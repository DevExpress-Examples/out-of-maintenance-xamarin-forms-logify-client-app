/*
               Copyright (c) 2015-2020 Developer Express Inc.
{*******************************************************************}
{                                                                   }
{       Developer Express Mobile UI for Xamarin.Forms               }
{                                                                   }
{                                                                   }
{       Copyright (c) 2015-2020 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING         }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Logify.Mobile.Models;
using Logify.Mobile.Services;
using Logify.Mobile.Services.Reports;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels {
    public class ReportListViewModel : NotificationObject {
        IReportRepository repository { get => DataProviderFactory.CreateReportListDataProvider(); }
        IGlobalSettings settings = GlobalSettings.Instance;

        readonly int pageSize = 20;

        bool isRefreshing = false;
        public bool IsRefreshing {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        bool isUpdateLocked = true;
        public bool IsUpdateLocked {
            get => isUpdateLocked;
            set => SetProperty(ref isUpdateLocked, value);
        }

        bool isLoadMoreEnabled = true;
        public bool IsLoadMoreEnabled {
            get => isLoadMoreEnabled;
            set => SetProperty(ref isLoadMoreEnabled, value);
        }

        public bool HasReports => !Reports.Any() && !IsUpdateLocked;

        public ObservableCollection<ReportViewModel> Reports { get; } = new ObservableCollection<ReportViewModel>();

        public ICommand LoadMoreCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public ICommand IgnoreCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public ReportListViewModel() {
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            RefreshCommand = new Command(ExecuteRefreshCommand);

            IgnoreCommand = new Command((report) => ((ReportViewModel)report).UpdateStatus(ReportStatus.IgnoredOnce));
            CloseCommand = new Command((report) => ((ReportViewModel)report).UpdateStatus(ReportStatus.ClosedOnce));
        }

        readonly object lockObj = new object();
        CancellationTokenSource cancellationTokenSource;

        void ExecuteLoadMoreCommand() => UpdateReportsRunner((token) => UpdateReportsCore(false, Reports.LastOrDefault()?.Report, token));
        void ExecuteRefreshCommand() => UpdateReportsRunner((token) => UpdateReportsCore(true, null, token));

        void UpdateReportsRunner(Func<CancellationToken, Task> updateReportsAction) {
            lock (lockObj) {
                if (cancellationTokenSource != null) {
                    cancellationTokenSource.Cancel();
                }
                cancellationTokenSource = new CancellationTokenSource();
            }
            CancellationToken token = cancellationTokenSource.Token;

            if (!token.IsCancellationRequested) {
                Task.Run(async () => await updateReportsAction(token), token);
            }
        }

        async Task UpdateReportsCore(bool clearList, Report lastReport, CancellationToken token) {
            IsUpdateLocked = true;
            IsLoadMoreEnabled = true;

            if (
                !token.IsCancellationRequested &&
                string.IsNullOrEmpty(SubscriptionsViewModel.Instance.SelectedItem?.Value as string)) {
                await SubscriptionsViewModel.Instance.UpdateItemsAsync();
            }

            if (
                !token.IsCancellationRequested &&
                string.IsNullOrEmpty(TeamsViewModel.Instance.SelectedItem?.Value as string)) {
                await TeamsViewModel.Instance.UpdateItemsAsync();
            }

            if (token.IsCancellationRequested) {
                return;
            }
            var reports = await repository.LoadReports(settings.FilterString, pageSize, lastReport);
            if (token.IsCancellationRequested) {
                return;
            }
            await Device.InvokeOnMainThreadAsync(() => {
                if (token.IsCancellationRequested) {
                    return;
                }
                if (clearList)
                    Reports.Clear();
                if (token.IsCancellationRequested) {
                    return;
                }
                AddToReports(reports);
            });
        }

        void AddToReports(IEnumerable<Report> items) {
            if (items != null && items.Count() > 0) {
                var rep = repository;
                foreach (var report in items) {
                    Reports.Add(new ReportViewModel(report, rep));
                }
            } else
                IsLoadMoreEnabled = false;
            IsRefreshing = false;
            IsUpdateLocked = false;
            OnPropertyChanged(nameof(HasReports));
        }
    }
}

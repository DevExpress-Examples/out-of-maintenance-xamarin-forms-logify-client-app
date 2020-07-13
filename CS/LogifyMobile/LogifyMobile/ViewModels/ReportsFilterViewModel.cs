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
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Logify.Mobile.Controls;
using Logify.Mobile.Models;
using Logify.Mobile.Services;
using Logify.Mobile.Services.Applications;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels {
    public class ReportsFilterViewModel : NotificationObject {
        IApplicationsDataProvider dataProvider { get => DataProviderFactory.CreateApplicationsDataProvider(); }
        FilterViewModel filterViewModel = FilterViewModel.Instance;
        bool separatorVisible;

        public IList<ApplicationInfo> Applications { get; private set; } = new List<ApplicationInfo>();

        public bool ApplicationSeparatorVisible { get { return separatorVisible; } }

        public IEnumerable<StatusInfo> Statuses { get; } = new ReportStatus[] {
            ReportStatus.Active,
            ReportStatus.ClosedInVersion,
            ReportStatus.ClosedOnce,
            ReportStatus.IgnoredByRule,
            ReportStatus.IgnoredOnce,
            ReportStatus.IgnoredPermanently,
        }.Select(status => new StatusInfo {
            Status = status,
            Selected = GlobalSettings.Instance.FilterString.Contains($"[Status] = {(int)status}")
        }).ToList();

        public ReportsFilterViewModel() {
            SelectedChangedCommand = new Command((sender) => {
                UpdateFilterString();
            });

            ApplicationTapCommand = new Command((sender) => {
                ApplicationInfo application = (ApplicationInfo)sender;
                application.Selected = !application.Selected;
                UpdateFilterString();
            });
            SubscriptionTapCommand = new Command((sender) => {
                PickerViewItem item = (PickerViewItem)sender;
                if(item != null) {
                    SubscriptionsViewModel.Instance.SelectedItem = item;
                }
            });
            TeamTapCommand = new Command((sender) => {
                PickerViewItem item = (PickerViewItem)sender;
                if (item != null) {
                    TeamsViewModel.Instance.SelectedItem = item;
                }
            });
            TeamsViewModel.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            separatorVisible = TeamsViewModel.Instance.IsMultiItems || SubscriptionsViewModel.Instance.IsMultiItems;
            OnPropertyChanged(nameof(ReportsFilterViewModel.ApplicationSeparatorVisible));
        }

        public void RefreshApplications() => Task.Run(async () => {
            IList<ApplicationInfo> apps = (await dataProvider.GetApplications()).Select(x => new ApplicationInfo() {
                Name = x.Value,
                Id = x.Key,
                Selected = filterViewModel.FilterString.Contains($"[ApiKey] = '{x.Key}'")
            }).ToList();
            Device.BeginInvokeOnMainThread(() => {
                Applications = apps;
                OnPropertyChanged(nameof(Applications));
            });
        });

        public ICommand SelectedChangedCommand { get; private set; }
        public ICommand ApplicationTapCommand { get; private set; }
        public ICommand SubscriptionTapCommand { get; private set; }
        public ICommand TeamTapCommand { get; private set; }

        void UpdateFilterString() {
            IEnumerable<string> statuses = Statuses.Where(status => status.Selected).Select(status => $"[Status] = {(int)status.Status}");
            IEnumerable<string> apiKeys = Applications.Where(app => app.Selected).Select(app => $"[ApiKey] = '{app.Id}'");
            filterViewModel.FilterString = string.Join(statuses.Any() && apiKeys.Any() ? " and " : "",
                string.Format(statuses.Count() > 1 && apiKeys.Any() ? "({0})" : "{0}", string.Join("||", statuses)),
                string.Format(statuses.Any() && apiKeys.Count() > 1 ? "({0})" : "{0}", string.Join("||", apiKeys)));
        }

    }

    public class StatusInfo {
        public ReportStatus Status { get; set; }
        public bool Selected { get; set; }
    }

    public class ApplicationInfo : NotificationObject {
        public string Name { get; set; }
        public string Id { get; set; }

        bool selected;
        public bool Selected {
            get => selected;
            set => SetProperty(ref selected, value);
        }
    }
}

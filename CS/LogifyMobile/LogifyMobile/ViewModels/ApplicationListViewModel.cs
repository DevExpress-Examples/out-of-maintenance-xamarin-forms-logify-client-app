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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Logify.Mobile.Services;
using Logify.Mobile.Services.ApplicationDetails;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels {
    public class ApplicationListViewModel : NotificationObject {
        IApplicationDetailsDataProvider dataProvider { get => DataProviderFactory.CreateApplicationDetailsDataProvider(); }
        IGlobalSettings settings = GlobalSettings.Instance;

        public List<ApplicationDetailsViewModel> Applications { get; private set; }

        bool isRefreshing = false;
        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { SetProperty(ref this.isRefreshing, value); }
        }

        bool isNotLoaded = false;
        public bool IsNotLoaded {
            get { return this.isNotLoaded; }
            set { SetProperty(ref this.isNotLoaded, value); }
        }

        CancellationTokenSource cancellationTokenSource;
        async Task ExecuteRefreshCommand() {
            this.cancellationTokenSource?.Cancel();
            this.cancellationTokenSource = new CancellationTokenSource();
            var token = this.cancellationTokenSource.Token;

            if (token.IsCancellationRequested) {
                return;
            }

            var apps = (await dataProvider.GetApplicationDetails());
            Applications = new List<ApplicationDetailsViewModel>();
            if (apps != null && apps.Count() > 0) {
                var maxAllReportsCount = apps.Max(app => app.AllReportsCount);
                Applications.AddRange(apps.Select(app => new ApplicationDetailsViewModel(app, maxAllReportsCount)));
            }
            OnPropertyChanged(nameof(Applications));
            IsRefreshing = false;
            IsNotLoaded = false;
            return;
        }

        public ICommand RefreshCommand => new Command(() => {
            _ = ExecuteRefreshCommand();
        });

    }
}

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
using System;
using Logify.Mobile.Models;
using Logify.Mobile.Services.Applications;
using Logify.Mobile.Services.ApplicationsDetail;
using Logify.Mobile.Services.ReportDetail;
using Logify.Mobile.Services.Reports;
using Logify.Mobile.Services.Statistic;
using Logify.Mobile.Services.Subscriptions;
using Logify.Mobile.Services.Teams;
using Logify.Mobile.ViewModels;

namespace Logify.Mobile.Services {
    public class LogifyDemoDataMode : ILogifyDataMode {
        string ILogifyDataMode.Name { get; } = "Demo";
        bool ILogifyDataMode.SkipLoginPage => false;

        public LoginButtonInfo GetButton() {
            LoginButtonInfo info = new LoginButtonInfo();
            info.ButtonIcon = "DemoMode.svg";
            info.ButtonText = "Demo Mode";
            info.Description = "Evaluate the capabilities of Logify's Mobile Client (sample data).";
            return info;
        }

        public void ProcessLogin(Action<ILogifyDataMode> onAuthenticated, Action<ILogifyDataMode> onCanceled) {
            onAuthenticated?.Invoke(this);
        }

        public void ProcessLogout() {
            GlobalSettings.Instance.CleanStoredData();
        }

        public UserInfo GetUserInfo() => new UserInfo("Demo User");

        public IApplicationsDataProvider GetApplicationsDataProvider() => new ApplicationsDemoDataProvider();
        public ITeamsDataProvider GetTeamsDataProvider() => new TeamsDemoDataProvider();

        public IApplicationsDetailDataProvider GetApplicationsDetailDataProvider() =>
            new ApplicationsDetailDemoDataProvider();

        public ISubscriptionsDataProvider GetSubscriptionsDataProvider() => new SubscriptionsDemoDataProvider();
        public IReportDetailDataProvider GetReportDetailDataProvider() => new ReportDetailDemoDataProvider();
        public IReportsRepository GetReportsDataProvider() => new ReportsDemoRepository();

        public IStatisticDataProvider GetStatisticDataProvider() => new StatisticDemoDataProvider();
    }
}

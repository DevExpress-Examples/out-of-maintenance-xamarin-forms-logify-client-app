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
using Logify.Mobile.Services.ApplicationDetails;
using Logify.Mobile.Services.ReportDetails;
using Logify.Mobile.Services.Reports;
using Logify.Mobile.Services.Statistics;
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

        public UserInfo GetUserInfo() {
            return new UserInfo("Demo User");
        }

        public IApplicationListDataProvider GetApplicationListDataProvider() {
            return new ApplicationListDemoDataProvider();
        }

        public ITeamListDataProvider GetTeamListDataProvider() {
            return new TeamListDemoDataProvider();
        }

        public IApplicationDetailsDataProvider GetApplicationDetailsDataProvider() {
            return new ApplicationDetailsDemoDataProvider();
        }

        public ISubscriptionListDataProvider GetSubscriptionListDataProvider() {
            return new SubscriptionListDemoDataProvider();
        }

        public IReportDetailsDataProvider GetReportDetailsDataProvider() {
            return new ReportDetailsDemoDataProvider();
        }

        public IReportRepository GetReportListDataProvider() {
            return new ReportDemoRepository();
        }

        public IStatisticsDataProvider GetStatisticsDataProvider() {
            return new StatisticsDemoDataProvider();
        }
    }
}

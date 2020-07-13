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
using Logify.Mobile.Services.Applications;
using Logify.Mobile.Services.ApplicationDetails;
using Logify.Mobile.Services.ReportDetails;
using Logify.Mobile.Services.Reports;
using Logify.Mobile.Services.Statistics;
using Logify.Mobile.Services.Subscriptions;
using Logify.Mobile.Services.Teams;

namespace Logify.Mobile.Services {
    public static class DataProviderFactory {
        static readonly Dictionary<string, object> dataProvidersCache = new Dictionary<string, object>();

        static TDataProvider GetCachedDataProvider<TDataProvider>(string providerName, Func<TDataProvider> providerInitializator) {
            if (!dataProvidersCache.TryGetValue(providerName, out var dataProvider)) {
                dataProvider = providerInitializator();
                dataProvidersCache[providerName] = dataProvider;
            }

            return (TDataProvider)dataProvider;
        }
        
        public static ISubscriptionListDataProvider CreateSubscriptionListDataProvider() {
            return GetCachedDataProvider("subscriptions", LogifyDataModeContext.SelectedMode.GetSubscriptionListDataProvider);
        }
        public static IApplicationListDataProvider CreateApplicationListDataProvider() {
            return GetCachedDataProvider("apps", LogifyDataModeContext.SelectedMode.GetApplicationListDataProvider);
        }
        
        public static IApplicationDetailsDataProvider CreateApplicationDetailsDataProvider() {
            return GetCachedDataProvider("appDetails", LogifyDataModeContext.SelectedMode.GetApplicationDetailsDataProvider);
        }
        public static IReportDetailsDataProvider CreateReportDetailsDataProvider() {
            return GetCachedDataProvider("reportDetails", LogifyDataModeContext.SelectedMode.GetReportDetailsDataProvider);
        }
        
        public static IStatisticsDataProvider CreateStatisticsDataProvider() {
            return GetCachedDataProvider("statistics", LogifyDataModeContext.SelectedMode.GetStatisticsDataProvider);
        }
        
        public static ITeamListDataProvider CreateTeamListDataProvider() {
            return GetCachedDataProvider("teams", LogifyDataModeContext.SelectedMode.GetTeamListDataProvider);
        }
        
        public static IReportRepository CreateReportListDataProvider() {
            return GetCachedDataProvider("reports",LogifyDataModeContext.SelectedMode.GetReportListDataProvider);
        }

        internal static void ClearCache() {
            dataProvidersCache.Clear();
        } 
    }
}

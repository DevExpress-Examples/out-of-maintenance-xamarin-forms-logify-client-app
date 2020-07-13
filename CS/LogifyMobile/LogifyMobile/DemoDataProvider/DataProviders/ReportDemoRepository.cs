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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Logify.Mobile.Models;
using Logify.Mobile.Services.Applications;

namespace Logify.Mobile.Services.Reports {
    public class ReportDemoRepository : IReportRepository {
        readonly IGlobalSettings settings = GlobalSettings.Instance;

        List<Report> reports;

        public ReportDemoRepository() {
            reports = Task.Run(() => MockData.GetObject<List<Report>>("DemoReports.json")).Result;
        }

        public async Task<IEnumerable<Report>> LoadReports(string filterString, int count, Report lastReport = null) {

            IEnumerable<string> allKeys = (await new ApplicationListDemoDataProvider().GetApplications()).Select(a => a.Key);
            IEnumerable<string> filterApiKeys = GetApiKeys(filterString);

            List<string> apiKeys = (filterApiKeys.Any() ? filterApiKeys.Intersect(allKeys) : allKeys).ToList();
            List<ReportStatus> statuses = GetStatuses(filterString).ToList();

            var totalAmount = 500;

            await Task.Delay(100);
            Random rnd = new Random((int)(lastReport?.DateTime.Ticks ?? 0));
            var result = new List<Report>();
            while (count > 0 && totalAmount-- > 0) {
                var report = reports[rnd.Next(reports.Count)];
                if ((!statuses.Any() || statuses.Any(s => s == report.Status)) &&
                    (!apiKeys.Any() || apiKeys.Any(k => k == report.ApiKey))) {
                    result.Add(report);
                    count--;
                }
            }
            return result;
        }

        public async Task<string> UpdateStatus(Report report, ReportStatus status, string reactivateCondition = "") {
            await Task.Delay(100);
            return "true";
        }

        IEnumerable<ReportStatus> GetStatuses(string filterString) {
            return Regex.Matches(filterString, @"\[Status\]\s*=\s*\d*").Cast<Match>()
                .Select(match => (ReportStatus)int.Parse(match.Value.Split('=')[1].Trim()));
        }

        IEnumerable<string> GetApiKeys(string filterString) {
            return Regex.Matches(filterString, @"\[ApiKey\]\s*=\s*'\w*'").Cast<Match>()
                .Select(match => match.Value.Split('=')[1].Trim().Trim('\''));
        }
    }
}

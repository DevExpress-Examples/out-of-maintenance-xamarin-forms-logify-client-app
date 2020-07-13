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
using System.Threading.Tasks;
using Logify.Mobile.Models;
using Logify.Mobile.Models.Statistic;

namespace Logify.Mobile.Services.Statistic {
    public class StatisticDemoDataProvider : IStatisticDataProvider {
        Random rnd = new Random(GlobalSettings.Instance.SubscriptionId.GetHashCode());

        public async Task<CountByDateStatistic> GetCountByDateStatistic(StatisticRequestType requestType, DateTime from, DateTime to) {
            var reportDaysCount = rnd.Next(12 * 30, 36 * 30);
            var today = DateTime.UtcNow.Date;

            IEnumerable<CountByDate> values = Enumerable.Range(-reportDaysCount + 1, reportDaysCount)
                .Select(x => new CountByDate {
                    Date = today.AddDays(x),
                    Count = rnd.Next(requestType == StatisticRequestType.All ? 100 : 10)
                })
                .Where(x => x.Date >= from && x.Date <= to)
                .GroupBy(GroupingFuncs[AggregationStepHelper.GetAggregationStep(from, to)])
                .Select(g => {
                    return new CountByDate {
                        Count = g.Sum(dv => dv.Count),
                        Date = g.Last().Date
                    };
                })
                .ToList();

            await Task.Delay(100);

            return new CountByDateStatistic {
                Total = values.Sum(dv => dv.Count),
                Values = values
            };
        }

        public async Task<IEnumerable<CountByStatus>> GetCountByStatusesStatistic() {
            await Task.Delay(100);
            return (Enum.GetValues(typeof(ReportStatus))).Cast<ReportStatus>().Select(s => new CountByStatus() { Status = s, Count = rnd.Next(0, 200) }).ToList();
        }

        static readonly IDictionary<AggregationStep, Func<CountByDate, int>> GroupingFuncs = new Dictionary<AggregationStep, Func<CountByDate, int>>() {
            { AggregationStep.Day, new Func<CountByDate, int>(dv => dv.Date.Year * dv.Date.DayOfYear) },
            { AggregationStep.Week, new Func<CountByDate, int>(dv => dv.Date.Year * (dv.Date.DayOfYear / 7)) },
            { AggregationStep.Month, new Func<CountByDate, int>(dv => dv.Date.Year * dv.Date.Month) },
            { AggregationStep.Year, new Func<CountByDate, int>(dv => dv.Date.Year) },
        };
    }
}

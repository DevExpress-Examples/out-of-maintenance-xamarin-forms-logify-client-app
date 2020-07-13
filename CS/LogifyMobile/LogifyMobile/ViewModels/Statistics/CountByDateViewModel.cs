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
using DevExpress.XamarinForms.Charts;
using Logify.Mobile.Models.Statistics;
using Logify.Mobile.Services;
using Logify.Mobile.Services.Statistics;

namespace Logify.Mobile.ViewModels.Statistics {
    public class CountByDateViewModel : NotificationObject {

        public IReadOnlyCollection<CountByDate> Data { get; private set; }
        public int TotalCount { get; private set; }

        public DateTimeMeasureUnit MeasureUnit { get; private set; }

        public void DataRangeUpdated(StatisticsRequestType requestType, DateTime fromDate, DateTime toDate) => Task.Run(async () => {
            CountByDateStatistics statistics = await DataProviderFactory.CreateStatisticsDataProvider().GetCountByDateStatistics(requestType, fromDate, toDate);

            Data = null;
            if (statistics != null) {
                if (statistics.Values != null)
                    Data = statistics.Values.Any() ? statistics.Values.ToList() : null;
                TotalCount = statistics.Total;   
            }
            MeasureUnit = AggregationStepHelper.MeasureUnits[AggregationStepHelper.GetAggregationStep(fromDate, toDate)];

            OnPropertyChanged(nameof(Data));
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(MeasureUnit));
        });
    }

}

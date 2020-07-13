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
using DevExpress.XamarinForms.Charts;
using Logify.Mobile.Models.Statistic;

namespace Logify.Mobile.Services.Statistic {
    public static class AggregationStepHelper {
        public static AggregationStep GetAggregationStep(DateTime from, DateTime to) {
            var rangeDays = (to - from).Days;
            if (rangeDays > 5 * 366) {
                return AggregationStep.Year;
            }
            if (rangeDays > 5 * 31) {
                return AggregationStep.Month;
            }
            if (rangeDays > 35) {
                return AggregationStep.Week;
            }
            return AggregationStep.Day;
        }
        public static IDictionary<AggregationStep, DateTimeMeasureUnit> MeasureUnits = new Dictionary<AggregationStep, DateTimeMeasureUnit> {
            {AggregationStep.Day,DateTimeMeasureUnit.Day },
            {AggregationStep.Week,DateTimeMeasureUnit.Week },
            {AggregationStep.Month,DateTimeMeasureUnit.Month },
            {AggregationStep.Year,DateTimeMeasureUnit.Year },
        };
    }
}

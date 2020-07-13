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
using Logify.Mobile.Models;
using Logify.Mobile.Models.Statistic;
using Logify.Mobile.Services;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels.Statistic {
    public class CountByStatusesViewModel : NotificationObject {
        public IReadOnlyCollection<StatusValueViewModel> Data { get; private set; }
        public Color[] CurrentPallete { get; private set; } = new Color[0];
        public int TotalCount { get; private set; }

        public void UpdateStatuses() => Task.Run(async () => {
            var statistic = await DataProviderFactory.CreateStatisticDataProvider().GetCountByStatusesStatistic();
            Data = statistic.Select(x=>new StatusValueViewModel(x)).OrderBy(s => s.Status).ToList();
            CurrentPallete = Data.Select(s => (Color)Logify.Mobile.Resources.Values[$"ReportStatus{s.Status}"]).ToArray();
            TotalCount = statistic.Sum(x => x.Count);
            OnPropertyChanged(nameof(Data));
            OnPropertyChanged(nameof(CurrentPallete));
            OnPropertyChanged(nameof(TotalCount));
        });
    }

    public class StatusValueViewModel : CountByStatus {
        public StatusValueViewModel(CountByStatus countByStatus) {
            this.Count = countByStatus.Count;
            this.Status = countByStatus.Status;
        }
        public string Name => ReportStatusNames.Name[Status];
    }
}

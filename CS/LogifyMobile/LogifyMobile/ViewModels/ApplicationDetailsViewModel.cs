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
using Logify.Mobile.Models;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels {
    public class ApplicationDetailsViewModel : NotificationObject {
        public ApplicationDetails ApplicationDetails { get; private set; }
        int maxAllReportsCount;

        public ApplicationDetailsViewModel(ApplicationDetails applicationDetails, int maxAllReportsCount) {
            ApplicationDetails = applicationDetails;
            this.maxAllReportsCount = maxAllReportsCount;

            Data = new List<ChartData> {
                new ChartData() { Name = "Total", Value = ApplicationDetails.AllReportsCount },
                new ChartData() { Name = "Active", Value = ApplicationDetails.ActiveReportsCount },
            };
        }

        public IEnumerable<ChartData> Data { get; private set; }

        public GridLength AllReportsCountPersentGridSize => new GridLength(Math.Round(AllReportsCountPersent, 3), GridUnitType.Star);
        public GridLength InvertedAllReportsCountPersentGridSize => new GridLength(Math.Round((1 - AllReportsCountPersent), 3), GridUnitType.Star);

        public Double AllReportsCountPersent => maxAllReportsCount > 0 ? ((double)ApplicationDetails.AllReportsCount / maxAllReportsCount) : 0;
        public Double AcitiveReportsCountPersent => maxAllReportsCount > 0 ? ((double)ApplicationDetails.ActiveReportsCount / maxAllReportsCount) : 0;

    }

    public class ChartData {
        public int Value { get; set; }
        public string Name { get; set; }
    }
}

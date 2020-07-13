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
﻿using System.Threading.Tasks;
using Logify.Mobile.Models;
using Logify.Mobile.Services;
using Logify.Mobile.Services.ReportDetail;

namespace Logify.Mobile.ViewModels.ReportDetail {
    public class ReportDetailViewModelCreator {
        string reportHeader = string.Empty;
        public async Task<ReportDetailViewModel> CreateModel(ReportViewModel reportViewModel) {
            IReportDetailDataProvider provider = DataProviderFactory.CreateReportDetailDataProvider();
            ReportDetailViewModel model = new ReportDetailViewModel(reportViewModel, provider);
            ExceptionReport exceptionReport = await provider.LoadReport(reportViewModel.Report.ApiKey, reportViewModel.Report.ReportId);
            foreach (ExceptionReportCard card in exceptionReport.Cards) {
                ReportDetailInfoContainerBase container = CreateContainer(card, provider, reportViewModel.Report);
                if (container != null)
                    model.Cards.Add(container);
            }
            model.Cards[0].IsSelected = true;
            return model;
        }
        ReportDetailInfoContainerBase CreateContainer(ExceptionReportCard card, IReportDetailDataProvider provider, Report report) {
            ReportDetailInfoContainerBase container = null;
            switch (card.Type) {
                case CardType.KeyValue:
                    container = new KeyValueReportDetailInfoContainer(card.Values);
                    break;
                case CardType.Stacked:
                    container = new StackedReportDetailInfoContainer(card.Values);
                    break;
                case CardType.SimpleList:
                    container = new SimpleListReportDetailInfoContainer(card.Values);
                    break;
                case CardType.Comments:
                    container = new CommentsDetailInfoContainer(card.Value, provider, report);
                    break;
                case CardType.Tabular:
                    container = new TabularDetailInfoContainer(card, provider);
                    break;
            }
            container.CardHeader = card.Title.ToUpper();
            return container;
        }
    }
}

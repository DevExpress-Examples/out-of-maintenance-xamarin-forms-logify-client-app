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
﻿using System.Windows.Input;
using Logify.Mobile.Models;
using Logify.Mobile.Services.ReportDetail;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels.ReportDetail {
    public class CommentsDetailInfoContainer : ReportDetailInfoContainerBase {
        public override CardType CardType => CardType.Comments;
        public string Comments { get; set; }

        public ICommand SaveCommentsCommand { get; private set; }

        public CommentsDetailInfoContainer(object value, IReportDetailDataProvider provider, Report report): base() {
            if (value == null)
                value = string.Empty;
            Comments = value.ToString();
            SaveCommentsCommand = new Command<string>(async (savingComments) => {
                if (!Comments.Equals(savingComments)) {
                    await provider.SaveComments(report.ApiKey, report.ReportId, savingComments);
                }
            });
        }
        
        protected override void ProcessValue(object item, bool isLastCard) {
        }
    }
}

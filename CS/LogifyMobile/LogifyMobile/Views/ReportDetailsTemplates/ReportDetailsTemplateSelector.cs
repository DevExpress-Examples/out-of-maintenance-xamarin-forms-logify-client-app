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
﻿using Logify.Mobile.Models;
using Logify.Mobile.ViewModels.ReportDetails;
using Xamarin.Forms;

namespace Logify.Mobile.Views.ReportDetailsTemplates {
    public class ReportDetailsTemplateSelector: DataTemplateSelector {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            if (!(item is ReportDetailInfoContainerBase infoContainer))
                return null;

            switch (infoContainer.CardType) {
                case CardType.KeyValue: return KeyValueTemplate;
                case CardType.Stacked: return StackedTemplate;
                case CardType.SimpleList: return SimpleListTemplate;
                case CardType.Comments: return CommentsTemplate;
                case CardType.Tabular: return TabularTemplate;
                default: return KeyValueTemplate;
            }
        }
        public DataTemplate KeyValueTemplate { get; set; }
        public DataTemplate StackedTemplate { get; set; }
        public DataTemplate SimpleListTemplate { get; set; }
        public DataTemplate CommentsTemplate { get; set; }
        public DataTemplate TabularTemplate { get; set; }
    }
}

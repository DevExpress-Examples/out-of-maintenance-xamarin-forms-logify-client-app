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
using System.Threading.Tasks;
using Logify.Mobile.Models;
using Logify.Mobile.Services.ReportDetails;
using Newtonsoft.Json.Linq;

namespace Logify.Mobile.ViewModels.ReportDetails {
    public class TabularDetailInfoContainer: ReportDetailInfoContainerBase {
        readonly List<TabularCard> cards = new List<TabularCard>() { };
        string refURL = string.Empty;
        bool cardsLoaded = false;
        IReportDetailsDataProvider provider = null;

        public CardValueType CardValueType { get; }
        public override CardType CardType => CardType.Tabular;
        public List<TabularCard> Values {
            get { return cards; }
        }

        public TabularDetailInfoContainer(ExceptionReportCard card, IReportDetailsDataProvider provider) {
            CardValueType = card.CardValueType;
            if (!card.Referrer) {
                ProcessValues(card.Values);
                cardsLoaded = true;
            } else {
                this.provider = provider;
                refURL = card.RefURL;
            }           
        }

        protected override void ProcessValue(object item, bool isLastCard) {
            if (item is JObject jObject) {
                TabularCard card = TabularCard.CreateInstance(CardValueType, jObject);
                card.IsLastCard = isLastCard;
                cards.Add(card);
            }
        }
        public async Task LoadCardsIfNeeded() {
            if (!cardsLoaded) {
                List<object> values = await provider.LoadByRefURL(refURL);
                ProcessValues(values);
                cardsLoaded = true;
            }
        }
    }
}

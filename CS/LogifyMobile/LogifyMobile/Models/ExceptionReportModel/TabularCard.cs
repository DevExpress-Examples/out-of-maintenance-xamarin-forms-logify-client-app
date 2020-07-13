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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Logify.Mobile.Models {
    public abstract class TabularCard {
        public static TabularCard CreateInstance(CardValueType type, JObject item) {
            switch (type) {
                case CardValueType.Breadcrumbs: return ((JObject)item).ToObject<BreadcrumbsCard>();
                case CardValueType.Audit: return ((JObject)item).ToObject<AuditCard>();
                case CardValueType.Input: return ((JObject)item).ToObject<InputCard>();
                case CardValueType.Cookies : return ((JObject)item).ToObject<CookiesCard>();
                default: return null;
            }
        }
        public abstract CardValueType CardValueType { get; }
        public bool IsLastCard { get; set; }
    }
    public class BreadcrumbsCard : TabularCard {
        public override CardValueType CardValueType { get { return CardValueType.Breadcrumbs; } }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
        public DateTime DateTime { get; set; }
        public string Level { get; set; }
        public string Line { get; set; }
        public string Thread { get; set; }
        public string Category { get; set; }
        public string Event { get; set; }
        public string Message { get; set; }
        public string MethodName { get; set; }

        public bool LevelVisible { get { return !string.IsNullOrEmpty(Level); } }
        public bool ThreadVisible { get { return !string.IsNullOrEmpty(Thread); } }
        public bool CategoryVisible { get { return !string.IsNullOrEmpty(Category); } }
        public bool EventVisible { get { return !string.IsNullOrEmpty(Event); } }
    }

    public class AuditCard : TabularCard {
        public override CardValueType CardValueType { get { return CardValueType.Audit; } }

        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
    }

    public class InputCard : TabularCard {
        public override CardValueType CardValueType { get { return CardValueType.Input; } }

        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool ValueVisible { get { return !string.IsNullOrEmpty(Value); } }
    }
    public class CookiesCard : TabularCard {
        public override CardValueType CardValueType { get { return CardValueType.Cookies; } }

        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime Expires { get; set; }
        public bool Secure { get; set; }
        public string Domain { get; set; }
        public bool ExpiresVisible { get { return Expires != DateTime.MinValue; } }
        public bool DomainVisible { get { return !string.IsNullOrEmpty(Domain); } }
    }
}

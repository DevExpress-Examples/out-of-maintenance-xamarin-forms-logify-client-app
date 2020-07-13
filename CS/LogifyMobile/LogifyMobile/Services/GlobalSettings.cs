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
using Xamarin.Forms;

namespace Logify.Mobile.Services {
    public class GlobalSettings : IGlobalSettings {
        static readonly Lazy<IGlobalSettings> instance = new Lazy<IGlobalSettings>(() => new GlobalSettings(), true);

        public static IGlobalSettings Instance => instance.Value;

        private GlobalSettings() { }
        
        public string SubscriptionId {
            get => GetValue(nameof(SubscriptionId), string.Empty);
            set => SetValue(nameof(SubscriptionId), value);
        }

        public string TeamId {
            get => GetValue(TeamIdPropertyName, string.Empty);
            set => SetValue(TeamIdPropertyName, value);
        }

        public string FilterString {
            get => GetValue(nameof(FilterString), string.Empty);
            set => SetValue(nameof(FilterString), value);
        }

        T GetValue<T>(string key, T defaultValue = default(T)) {
            return Application.Current.Properties.TryGetValue(key, out object result) ? (T)result : defaultValue;
        }

        void SetValue(string key, object value) {
            Application.Current.Properties[key] = value;
            Application.Current.SavePropertiesAsync();
        }

        void IGlobalSettings.CleanStoredData() {
            SubscriptionId = String.Empty;
            TeamId = String.Empty;
            FilterString = String.Empty;
        }

        string TeamIdPropertyName => $"{nameof(TeamId)} {SubscriptionId}";
    }
}

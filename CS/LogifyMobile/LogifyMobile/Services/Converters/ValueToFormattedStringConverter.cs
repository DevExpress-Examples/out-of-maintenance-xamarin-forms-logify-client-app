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
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Logify.Mobile.Services.Converters {
    public class ValueToFormattedStringConverter : IValueConverter {
        const string serverDateTimeFormat = "MM/dd/yyyy h:mm:ss tt";

        public Style HyperLinkStyle { get; set; }
        public Style DefaultStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string || value is DateTime) {
                Uri uri = null;
                string stringValue = value is string? (string)value: string.Empty;
                DateTime utcDateTime = value is DateTime ? (DateTime)value: DateTime.MinValue;
                if (utcDateTime != DateTime.MinValue || DateTime.TryParseExact(stringValue, serverDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal,  out utcDateTime)) {
                    return GetLocalDateTimeFormatString(utcDateTime);
                } else if (Uri.TryCreate(stringValue, UriKind.Absolute, out uri)) {
                    return CreateHyperLinkFormattedString(uri);
                }
                return CreateSimpleFormattedString(stringValue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        FormattedString GetLocalDateTimeFormatString(DateTime utcDateTime) {
            string localTime = utcDateTime.ToLocalTime().ToString();
            FormattedString result = new FormattedString();
            result.Spans.Add(new Span { Text = localTime, Style = DefaultStyle });
            return result;
        }

        Span CreateHyperLinkSpan(Uri uri) {
            Span result = new Span { Text = uri.AbsoluteUri, Style = HyperLinkStyle };
            result.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Launcher.OpenAsync(uri)) });
            return result;
        }
        FormattedString CreateHyperLinkFormattedString(Uri uri) {
            FormattedString result = new FormattedString();
            result.Spans.Add(CreateHyperLinkSpan(uri));
            return result;
        }
        FormattedString CreateSimpleFormattedString(string value) {
            FormattedString result = new FormattedString();
            result.Spans.Add(new Span { Text = value, Style = DefaultStyle });
            return result;
        }
    }
}

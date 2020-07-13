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
using Logify.Mobile.Models;
using Xamarin.Forms;

namespace Logify.Mobile.Services.Converters {
    public abstract class AuditStatusConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string text) {
                return GetConvertedValue(text, GetStatusName(text));
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        protected abstract object GetConvertedValue(string text, string statusName);
        protected Color GetStatusColor(string statusName) {
            if (string.IsNullOrEmpty(statusName))
                return Color.Transparent;
            return ((Color)Logify.Mobile.Resources.Values[$"ReportStatus{statusName}"]);
        }
        protected string GetTextBeforeStatus(string text, string statusName) {
            if (string.IsNullOrEmpty(statusName))
                return text;
            return text.Substring(0, text.IndexOf(statusName, StringComparison.Ordinal));
        }
        string GetStatusName(string text) {
            foreach (string status in Enum.GetNames(typeof(ReportStatus))) {
                if (text.Contains(status)) {
                    return status;
                }
            }
            return string.Empty;
        }
    }
    public class ColorStatusConverter: AuditStatusConverter {
        protected override object GetConvertedValue(string text, string statusName) {
            if (string.IsNullOrEmpty(statusName))
                return Color.Transparent;
            return GetStatusColor(statusName);
        }
    }
    public class TextHasStatusConverter : AuditStatusConverter {
        protected override object GetConvertedValue(string text, string statusName) {
            return !string.IsNullOrEmpty(statusName);
        }
    }
    public class TextBeforeStatusConverter : AuditStatusConverter {
        protected override object GetConvertedValue(string text, string statusName) {
            return GetTextBeforeStatus(text, statusName);
        }
    }
    public class TextStatusNameConverter : AuditStatusConverter {
        protected override object GetConvertedValue(string text, string statusName) {
            return $" {statusName} ";
        }
    }
}

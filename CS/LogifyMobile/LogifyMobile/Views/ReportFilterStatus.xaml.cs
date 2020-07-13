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
using System.Windows.Input;
using Logify.Mobile.Models;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class ReportFilterStatus : Grid {
        public ReportFilterStatus() {
            InitializeComponent();
        }

        public static readonly BindableProperty StatusProperty = BindableProperty.Create(
            nameof(Status), typeof(ReportStatus), typeof(ReportFilterStatus),
            null, BindingMode.OneTime, propertyChanged: StatusPropertyChanged);

        static void StatusPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            var control = (ReportFilterStatus)bindable;
            var status = (ReportStatus)newValue;
            control.label.Text = ReportStatusNames.Name[status];
            control.icon.ForegroundColor = (Color)Logify.Mobile.Resources.Values[$"ReportStatus{status}"];
        }

        public ReportStatus Status {
            get => (ReportStatus)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected), typeof(bool), typeof(ReportFilterStatus),
            false, BindingMode.TwoWay, propertyChanged: IsSelectedPropertyChanged);

        static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((ReportFilterStatus)bindable).label.TextColor =
                (Color)Logify.Mobile.Resources.Values[((bool)newValue) ?
                "FiltersTextMainColor" : "FiltersTextSecondaryColor"];

            ((ReportFilterStatus)bindable).backgroundFrame.BorderColor =
                (Color)Logify.Mobile.Resources.Values[((bool)newValue) ?
                "FiltersStatusItemBackgroundColor" : "FiltersStatusItemBorderColor"];

            ((ReportFilterStatus)bindable).backgroundFrame.BackgroundColor =
                (bool)newValue ? (Color)Logify.Mobile.Resources.Values["FiltersStatusItemBackgroundColor"] : Color.Transparent;                
        }

        public bool IsSelected {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        void Handle_TappedGesture(object sender, EventArgs args) {
            IsSelected = !IsSelected;
            SelectedChangedCommand?.Execute(this);
        }

        public static readonly BindableProperty SelectedChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedChangedCommand),typeof(ICommand),typeof(ReportFilterStatus));

        public ICommand SelectedChangedCommand {
            get => (ICommand)GetValue(SelectedChangedCommandProperty);
            set => SetValue(SelectedChangedCommandProperty, value);
        }
    }
}

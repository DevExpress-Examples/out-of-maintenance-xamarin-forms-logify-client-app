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

namespace Logify.Mobile.Controls {
    public partial class MenuButton : Grid {
        public MenuButton() {
            InitializeComponent();
        }

        public event EventHandler Tapped;

        public static BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource), typeof(string), typeof(MenuButton),
            propertyChanged: ImageSourcePropertyChanged);

        static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((MenuButton)bindable).icon.ImageSource = (string)newValue;
        }

        public string ImageSource {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text), typeof(string), typeof(MenuButton),
            propertyChanged: TextPropertyChanged);

        static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((MenuButton)bindable).label.Text = (string)newValue;
        }

        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool ShowRightIcon {
            get => this.rightIcon.IsVisible;
            set => this.rightIcon.IsVisible = value;
        }

        void Handle_Tapped(object sender, EventArgs args) {
            Tapped?.Invoke(this, EventArgs.Empty);
        }
    }
}

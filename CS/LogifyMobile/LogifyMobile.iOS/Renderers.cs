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
using System.ComponentModel;
using Logify.Mobile.Controls;
using Logify.Mobile.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(ExtNavigationRenderer))]
[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(PickerViewRenderer))]

namespace Logify.Mobile.iOS {
    public class ExtNavigationRenderer : NavigationRenderer {
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
            NavigationBar.Layer.ShadowOpacity = 0;
        }
    }

    public class PickerViewRenderer : PickerRenderer {
        string positiveButtonText = "OK";
        Color positiveButtonColor = Color.Default;
        string negativeButtonText = "Cancel";
        Color negativeButtonColor = Color.Default;

        public PickerViewRenderer() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);
            ExtendedPicker picker = Element as ExtendedPicker;
            if (picker != null) {
                this.positiveButtonText = picker.PositiveButtonText;
                this.positiveButtonColor = picker.PositiveButtonColor;
                this.negativeButtonText = picker.NegativeButtonText;
                this.negativeButtonColor = picker.NegativeButtonColor;
            }
            if (Control != null) {
                Control.BorderStyle = UITextBorderStyle.None;
                
                UIToolbar toolbar = (UIToolbar)Control.InputAccessoryView;
                UIBarButtonItem done = new UIBarButtonItem(this.positiveButtonText, UIBarButtonItemStyle.Done, (object sender, EventArgs click) => {});
                foreach(UIBarButtonItem button in toolbar.Items) {
                    if (button.Style == UIBarButtonItemStyle.Done)
                        done.Target = button.Target;
                }
                done.TintColor = this.positiveButtonColor.ToUIColor();
                UIBarButtonItem cancel = new UIBarButtonItem(this.negativeButtonText, UIBarButtonItemStyle.Done, (object sender, EventArgs click) => {
                    picker.Unfocus();
                });
                cancel.TintColor = this.negativeButtonColor.ToUIColor();
                UIBarButtonItem empty = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
                toolbar.Items = new UIBarButtonItem[] { cancel, empty, done };
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(ExtendedPicker.PositiveButtonText)) {
                this.positiveButtonText = ((ExtendedPicker)Element).PositiveButtonText;
            }
            if (e.PropertyName == nameof(ExtendedPicker.PositiveButtonColor)) {
                this.positiveButtonColor = ((ExtendedPicker)Element).PositiveButtonColor;
            }
            if (e.PropertyName == nameof(ExtendedPicker.PositiveButtonText)) {
                this.negativeButtonText = ((ExtendedPicker)Element).NegativeButtonText;
            }
            if (e.PropertyName == nameof(ExtendedPicker.PositiveButtonColor)) {
                this.negativeButtonColor = ((ExtendedPicker)Element).NegativeButtonColor;
            }
            if (e.PropertyName == nameof(ExtendedPicker.Show)) {
                Element.Focus();
            }
        }
    }
}

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
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using Logify.Mobile.Controls;
using Logify.Mobile.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(PickerViewRenderer))]
namespace Logify.Mobile.Droid {
    public class PickerViewRenderer : PickerRenderer {
        AlertDialog dialog;
        string positiveButtonText = "OK";
        Color positiveButtonColor = Color.Default;
        string negativeButtonText = "Cancel";
        Color negativeButtonColor = Color.Default;

        IElementController ElementController => Element as IElementController;
        public PickerViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);

            if (Element == null || e.NewElement == null || e.OldElement != null)
                return;
            Control.SetPadding(0, 0, 0, 0);
            if (Element is ExtendedPicker picker) {
                this.positiveButtonText = picker.PositiveButtonText;
                this.positiveButtonColor = picker.PositiveButtonColor;
                this.negativeButtonText = picker.NegativeButtonText;
                this.negativeButtonColor = picker.NegativeButtonColor;
            }
            Control.Background = new ColorDrawable(Element.BackgroundColor.ToAndroid());
            Control.Click += Control_Click;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(ExtendedPicker.BackgroundColor)) {
                Control.Background = new ColorDrawable(Element.BackgroundColor.ToAndroid());
            }
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
                Control_Click(sender, e);
            }
        }
        protected override void Dispose(bool disposing) {
            Control.Click -= Control_Click;
            base.Dispose(disposing);
        }

        private void Control_Click(object sender, EventArgs e) {
            Picker model = Element;

            var picker = new NumberPicker(Context);
            if (model.Items != null && model.Items.Any()) {
                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.Value = model.SelectedIndex;
            }
            var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
            layout.AddView(picker);
            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
            var builder = new AlertDialog.Builder(Context);
            builder.SetView(layout);
            builder.SetTitle(model.Title ?? "");
            builder.SetNegativeButton(this.negativeButtonText, (s, a) => { });
            builder.SetPositiveButton(this.positiveButtonText, (s, a) => {
                ElementController?.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                if (Element != null) {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController?.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                }
            });
            this.dialog = builder.Create();
            this.dialog.DismissEvent += (ssender, args) => {
                ElementController?.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
            };
            this.dialog.Show();
            Android.Widget.Button btnOk = this.dialog.GetButton((int)DialogButtonType.Positive);
            btnOk.SetTextColor(this.positiveButtonColor.ToAndroid());
            Android.Widget.Button btnCancel = this.dialog.GetButton((int)DialogButtonType.Negative);
            btnCancel.SetTextColor(this.negativeButtonColor.ToAndroid());

        }
    }    
}

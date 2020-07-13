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
using System.Threading.Tasks;
using Logify.Mobile.ViewModels;
using Xamarin.Forms;

namespace Logify.Mobile.Controls {
    public partial class FloatingButtonContainer : AbsoluteLayout {
        bool opened;
        public event EventHandler MakePublicLinkTapped;
        public event EventHandler CopyLinkTapped;
        public event EventHandler ScrollWrapTapped;
        public event EventHandler BeforeActionButtonsShow;

        public View ContentLayer { get { return contentLayer.Content; } set { contentLayer.Content = value; } }

        public FloatingButtonContainer() {
            InitializeComponent();
            this.buttonsLayer.Opacity = 0;
            this.actionButtons.Scale = 0;
            this.actionButtons.AnchorX = 0.9;
            this.actionButtons.AnchorY = 1;
        }

        async void MainButtonClicked(object sender, EventArgs e) {
            this.BeforeActionButtonsShow?.Invoke(this, e);
            await ToggleContainer(true);
        }
        async Task ToggleContainer(bool animate) {
            int scaleFadeValue = opened ? 0 : 1;
            if (!opened) {
                this.buttonsLayer.IsVisible = true;
            }
            if (animate) {
                await Task.WhenAll(
                     this.actionButtons.ScaleTo(scaleFadeValue, 100),
                     this.buttonsLayer.FadeTo(scaleFadeValue, 100)
                 );
            } else {
                this.buttonsLayer.Opacity = scaleFadeValue;
                this.actionButtons.Scale = scaleFadeValue;
            }
            this.buttonsLayer.IsVisible &= !opened;
            opened = !opened;
        }
        async void RaiseMakePublicLink(object sender, EventArgs e) {
            await ToggleContainer(true);
            MakePublicLinkTapped?.Invoke(this, e);
        }
        async void RaiseCopyLink(object sender, EventArgs e) {
            await ToggleContainer(true);
            CopyLinkTapped?.Invoke(this, e);
        }
        async void RaiseScrollWrap(object sender, EventArgs e) {
            await ToggleContainer(true);
            ScrollWrapTapped?.Invoke(this, e);
        }
        public void SetScrollWrapButtonText(string value) {
            if (scrollWrapButton != null && scrollWrapButton.BindingContext is TextIconButtonModel scrollWrapButtonContext)
                scrollWrapButtonContext.Text = value;
        }
        public void ToggleScrollWrapButton(bool visible) {
            if (opened || scrollWrapButton.IsVisible == visible)
                return;
            scrollWrapButton.IsVisible = visible;
        }
    }

    public class TextIconButtonModel : NotificationObject {
        string text;
        public string Text { get => text; set { if (text != value) SetProperty(ref text, value); } }
        public string Icon { get; set; }
    }
}

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
using DevExpress.XamarinForms.Navigation;
using Logify.Mobile.ViewModels;
using Logify.Mobile.ViewModels.ReportDetail;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class ReportDetailPage : ContentPage {

        ReportDetailViewModel model;
        public ReportDetailPage() {
            InitializeComponent();

            this.tabControl.PropertyChanging += TabControl_PropertyChanging;
            this.tabControl.PropertyChanged += TabControl_PropertyChanged;
        }
        public ReportDetailPage(ReportViewModel reportViewModel) : this() {
            Task.Run(async () => {
                var modelCreator = new ReportDetailViewModelCreator();
                this.model = await modelCreator.CreateModel(reportViewModel);
                Device.BeginInvokeOnMainThread(() => {
                    BindingContext = this.model;
                    ChangeSelectedIndexIfNeeded(nameof(TabView.SelectedItemIndex), true);
                    floatingButtonContainer.IsVisible = true;
                    picker.IsVisible = true;
                    activity.IsRunning = false;
                    activity.IsEnabled = false;
                    activity.IsVisible = false;
                });
            });
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            rawButton.IsEnabled = true;
            infoButton.IsEnabled = true;
        }
        void TabControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            ChangeSelectedIndexIfNeeded(e.PropertyName, true);
        }
        void TabControl_PropertyChanging(object sender, PropertyChangingEventArgs e) {
            ChangeSelectedIndexIfNeeded(e.PropertyName, false);
        }

        void ChangeSelectedIndexIfNeeded(string propertyName, bool select) {
            if (propertyName == nameof(TabView.SelectedItemIndex)) {
                this.model?.ChangeSelectedIndex(this.tabControl.SelectedItemIndex, select);
            }
        }
        void OpenRawReportPage(object sender, EventArgs e) {
            if (this.model == null)
                return;
            (sender as VisualElement).IsEnabled = false;
            infoButton.IsEnabled = false;
            if (this.model != null) {
                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PushAsync(new RawReportPage(model.ApiKey, model.ReportId));
                });
            }
        }
        void OpenDetailInfoPage(object sender, EventArgs e) {
            if (this.model == null)
                return;
            (sender as VisualElement).IsEnabled = false;
            rawButton.IsEnabled = false;
            Device.BeginInvokeOnMainThread(async () => {
                await Navigation.PushAsync(new DetailInfoPage(new DetailInfoModel(this.model.ApiKey)));
            });
        }
        void MakePublicLink(object sender, EventArgs e) {
            this.model?.MakePublicLink();
            DisplayAlert(String.Empty, "Public Link copied!", "Ok");
        }
        void CopyLink(object sender, EventArgs e) {
            this.model?.CopyLink();
            DisplayAlert(String.Empty, "Link copied!", "Ok");
        }
        void ScrollWrapInnerContent(object sender, EventArgs e) {
            this.model?.ChangeScrollWrapContentStatus();
            floatingButtonContainer.SetScrollWrapButtonText(this.model?.ScrollWrapButtonText);
        }
        void BeforeShowButtonsMenu(object sender, EventArgs e) {
            if (model != null) {
                floatingButtonContainer.ToggleScrollWrapButton(this.model.HasScrollWrapContent());
            }
        }
    }
}

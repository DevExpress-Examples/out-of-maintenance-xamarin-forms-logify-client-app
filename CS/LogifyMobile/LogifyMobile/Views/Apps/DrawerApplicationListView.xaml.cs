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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevExpress.XamarinForms.Navigation;
using Logify.Mobile.Services;
using Logify.Mobile.ViewModels;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class DrawerApplicationListView : DrawerPage, ISelectableTabItem {
        Thickness originalPaddings;
        bool originalPaddingsSaved = false;
        TeamsViewModel teamsViewModel = TeamsViewModel.Instance;
        bool showFilterPanel = false;
        bool areGesturesEnabled = false;

        public DrawerApplicationListView() {
            Task.Run(() => {
                InitializeComponent();
            });
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            AreGesturesEnabled = true;
            SetInsetsToPadding();
            MessagingCenter.Subscribe<Page, bool>(this, EventNames.SET_APPS_ARE_GESTURES_ENABLED, (sender, areGesturesEnabled) => {
                this.areGesturesEnabled = areGesturesEnabled;
                AreGesturesEnabled = this.showFilterPanel && areGesturesEnabled;
            });
            this.teamsViewModel.PropertyChanged += SelectedTeam_PropertyChanged;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            ChangeLoadMoreState(false);
            MessagingCenter.Unsubscribe<Page, bool>(this, EventNames.SET_APPS_ARE_GESTURES_ENABLED);
            this.teamsViewModel.PropertyChanged -= SelectedTeam_PropertyChanged;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SafeAreaInsetsProperty.PropertyName)
                SetInsetsToPadding();
        }

        void SelectedTeam_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(PickerViewModelBase.SelectedItem))
                ((ReportFilterViewModel)(this.applicationFilterView.BindingContext)).RefreshApplications();
            if (!this.showFilterPanel) {
                CheckViewModelsStatus();
                AreGesturesEnabled = this.showFilterPanel && this.areGesturesEnabled;
            }
        }

        void Handle_FilterButtonClicked(object sender, System.EventArgs e) {
            if (this.showFilterPanel) {
                IsDrawerOpened = !IsDrawerOpened;
            }
        }

        void SetInsetsToPadding() {
            Thickness insets = Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.GetSafeAreaInsets(this);
            if (!this.originalPaddingsSaved) {
                this.originalPaddings = this.applicationFilterView.Padding;
                this.originalPaddingsSaved = true;
            }
            this.applicationFilterView.Padding = new Thickness(
                this.originalPaddings.Left,
                Math.Max(this.originalPaddings.Top, insets.Top),
                Math.Max(this.originalPaddings.Right, insets.Right),
                this.originalPaddings.Bottom
            );
        }
        void CheckViewModelsStatus() {
            if (SubscriptionsViewModel.Instance.Items != null) {
                this.showFilterPanel = SubscriptionsViewModel.Instance.Items.Count > 1 || this.teamsViewModel.Items?.Count > 1;
                SetFilterButtonVisibility(this.showFilterPanel);
                this.applicationFilterView.SetSubscriptionListBlockHeight(SubscriptionsViewModel.Instance.Items.Count);
                this.applicationFilterView.SetTeamListBlockHeight(TeamsViewModel.Instance.Items.Count);
            }
        }

        public void SetFilterButtonVisibility(bool visible) {
            Device.BeginInvokeOnMainThread(() => {
                this.filterButton.IsVisible = visible;
            });
        }

        void ISelectableTabItem.OnTabSelected() {
            ((ReportFilterViewModel)(this.applicationFilterView.BindingContext)).RefreshApplications();
            ChangeLoadMoreState(true);
            CheckViewModelsStatus();
        }
        void ChangeLoadMoreState(bool state) {
            if (this.applicationListView.BindingContext is ApplicationListViewModel applicationsViewModel) {
                applicationsViewModel.IsNotLoaded = state;
            }
        }
    }
}

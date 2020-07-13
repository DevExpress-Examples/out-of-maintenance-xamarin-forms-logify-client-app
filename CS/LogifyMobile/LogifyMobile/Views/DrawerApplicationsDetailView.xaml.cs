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
    public partial class DrawerApplicationsDetailView : DrawerPage, ISelectableTabItem {
        Thickness originalPaddings;
        bool originalPaddingsSaved = false;
        TeamsViewModel teamsViewModel = TeamsViewModel.Instance;
        bool showFilterPanel = false;
        bool areGesturesEnabled = false;

        public DrawerApplicationsDetailView() {
            Task.Run(() => {
                InitializeComponent();
            });
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            this.AreGesturesEnabled = true;
            SetInsetsToPadding();
            MessagingCenter.Subscribe<Page, bool>(this, EventNames.SET_APPS_ARE_GESTURES_ENABLED, (sender, areGesturesEnabled) => {
                this.areGesturesEnabled = areGesturesEnabled;
                AreGesturesEnabled = showFilterPanel && areGesturesEnabled;
            });
            teamsViewModel.PropertyChanged += SelectedTeam_PropertyChanged;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            ChangeLoadMoreState(false);
            MessagingCenter.Unsubscribe<Page, bool>(this, EventNames.SET_APPS_ARE_GESTURES_ENABLED);
            teamsViewModel.PropertyChanged -= SelectedTeam_PropertyChanged;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SafeAreaInsetsProperty.PropertyName)
                SetInsetsToPadding();
        }

        void SelectedTeam_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(PickerViewModelBase.SelectedItem))
                ((ReportsFilterViewModel)(applicationsFilterView.BindingContext)).RefreshApplications();
            if (!showFilterPanel) {
                CheckViewModelsStatus();
                AreGesturesEnabled = showFilterPanel && areGesturesEnabled;
            }
        }

        void Handle_FilterButtonClicked(object sender, System.EventArgs e) {
            if (showFilterPanel) {
                IsDrawerOpened = !IsDrawerOpened;
            }
        }

        void SetInsetsToPadding() {
            Thickness insets = Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.GetSafeAreaInsets(this);
            if (!originalPaddingsSaved) {
                originalPaddings = this.applicationsFilterView.Padding;
                originalPaddingsSaved = true;
            }
            this.applicationsFilterView.Padding = new Thickness(
                originalPaddings.Left,
                Math.Max(originalPaddings.Top, insets.Top),
                Math.Max(originalPaddings.Right, insets.Right),
                originalPaddings.Bottom
            );
        }
        void CheckViewModelsStatus() {
            if (SubscriptionsViewModel.Instance.Items != null) {
                showFilterPanel = SubscriptionsViewModel.Instance.Items.Count > 1 || teamsViewModel.Items?.Count > 1;
                this.SetFilterButtonVisibility(showFilterPanel);
                applicationsFilterView.SetSubscriptionsBlockHeight(SubscriptionsViewModel.Instance.Items.Count);
                applicationsFilterView.SetTeamsBlockHeight(TeamsViewModel.Instance.Items.Count);
            }
        }

        public void SetFilterButtonVisibility(bool visible) {
            Device.BeginInvokeOnMainThread(() => {
                filterButton.IsVisible = visible;
            });
        }

        void ISelectableTabItem.OnTabSelected() {
            ((ReportsFilterViewModel)(applicationsFilterView.BindingContext)).RefreshApplications();
            ChangeLoadMoreState(true);
            CheckViewModelsStatus();
        }
        void ChangeLoadMoreState(bool state) {
            if (applicationsDetailView.BindingContext is ApplicationsDetailViewModel applicationsViewModel) {
                applicationsViewModel.IsNotLoaded = state;
            }
        }
    }
}

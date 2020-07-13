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
using DevExpress.XamarinForms.Navigation;
using Logify.Mobile.Services;
using Logify.Mobile.ViewModels;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class DrawerReportsFilterView : DrawerPage, ISelectableTabItem {
        Thickness originalPaddings;
        bool originalPaddingsSaved = false;
        bool isLandscape = false;
        TeamsViewModel teamsViewModel = TeamsViewModel.Instance;

        public DrawerReportsFilterView() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            SetInsetsToPadding();
            
            this.AreGesturesEnabled = true;

            MessagingCenter.Subscribe<Page, bool>(this, EventNames.SET_REPS_ARE_GESTURES_ENABLED, (sender, areGesturesEnabled) => {
                AreGesturesEnabled = areGesturesEnabled;
            });

            teamsViewModel.PropertyChanged += SelectedTeam_PropertyChanged;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Page, bool>(this, EventNames.SET_REPS_ARE_GESTURES_ENABLED);
            teamsViewModel.PropertyChanged -= SelectedTeam_PropertyChanged;
        }

        void Handle_FilterButtonClicked(object sender, System.EventArgs e) {
            IsDrawerOpened = !IsDrawerOpened;
            if (!IsDrawerOpened)
                reportsFilterView.UnfocusFilterPanel();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SafeAreaInsetsProperty.PropertyName)
                SetInsetsToPadding();
        }

        void SelectedTeam_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(PickerViewModelBase.SelectedItem))
                ((ReportsFilterViewModel)(reportsFilterView.BindingContext)).RefreshApplications();
        }

        void SetInsetsToPadding() {
            Thickness insets = Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.GetSafeAreaInsets(this);
            if (!originalPaddingsSaved) {
                originalPaddings = this.reportsFilterView.Padding;
                originalPaddingsSaved = true;
            }
            this.reportsFilterView.Padding = new Thickness(
                originalPaddings.Left,
                Math.Max(originalPaddings.Top, insets.Top),
                Math.Max(originalPaddings.Right, isLandscape ? 0 : insets.Right),
                originalPaddings.Bottom
            );
        }
        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if (width > height != isLandscape) {
                isLandscape = !isLandscape;
                reportsFilterView.ChangeLandscapeMode(isLandscape);
            }
        }

        void ISelectableTabItem.OnTabSelected() {
            ((ReportsFilterViewModel)(reportsFilterView.BindingContext)).RefreshApplications();
        }
    }
}

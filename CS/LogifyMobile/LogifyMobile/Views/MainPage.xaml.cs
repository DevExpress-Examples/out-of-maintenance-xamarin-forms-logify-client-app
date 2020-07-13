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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DevExpress.XamarinForms.Navigation;
using Logify.Mobile.Services;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class MainPage : TabPage {

        bool originalSizesSaved = false;
        TabHeaderLength originalPanelHeight;
        Thickness originalItemPaddings;

        public MainPage() {
            InitializeComponent();
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            SetInsetsToSizes();
        }
        private void MainPage_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e) {
            if (e.PropertyName == nameof(TabPage.SelectedItemIndex)
                && this.SelectedItemIndex >= 0
                && this.Items[this.SelectedItemIndex].Content is DrawerPage drawerPage) {
                drawerPage.IsDrawerOpened = false;
            }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SafeAreaInsetsProperty.PropertyName) {
                SetInsetsToSizes();
            }
        }
        void SetInsetsToSizes() {
            Thickness insets = Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.GetSafeAreaInsets(this);
            if (!originalSizesSaved) {
                originalPanelHeight = this.HeaderPanelHeight;
                originalItemPaddings = this.ItemHeaderPadding;
                originalSizesSaved = true;
            }
            this.HeaderPanelHeight = new TabHeaderLength(originalPanelHeight.Value + insets.Bottom, originalPanelHeight.TabHeaderUnitType);
            this.ItemHeaderPadding = new Thickness(
                originalItemPaddings.Left,
                originalItemPaddings.Top,
                originalItemPaddings.Right,
                originalItemPaddings.Bottom + (System.Math.Abs(insets.Bottom) > 0.01 ? insets.Bottom - 10 : 0));
        }
        void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(TabPage.SelectedItemIndex)
                            && this.SelectedItemIndex >= 0) {
                if (this.Items[this.SelectedItemIndex]?.Content is ISelectableTabItem tabItem) {
                    tabItem.OnTabSelected();
                }
            }
        }
    }
}

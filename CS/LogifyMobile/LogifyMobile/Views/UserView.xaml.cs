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
using Logify.Mobile.Services;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class UserView : ContentPage {
        bool isLandscape = false;

        double avatarTopMargin;
        double landscapeAvatarTopMargin = 10;
        double logoutBlockTopMargin;
        double landscapeLogoutBlockTopMargin = 10;
        double logoutButtonTopMargin;
        double landscapeLogoutButtonTopMargin = 55;
        LayoutOptions pageStackVerticalOptions;

        public UserView() {
            Task.Run(() => {
                InitializeComponent();
            });
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            
            BindingContext = LogifyDataModeContext.SelectedMode.GetUserInfo();
            avatarTopMargin = userAvatar.Margin.Top;
            logoutBlockTopMargin = logoutBlock.Margin.Top;
            logoutButtonTopMargin = logoutButton.Margin.Top;
            pageStackVerticalOptions = pageStack.VerticalOptions;
        }
        private void LogOut_Tapped(object sender, EventArgs e) {
            DataProviderFactory.ClearCache();
            LogifyDataModeContext.SetMode(null);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if (width > height != isLandscape) {
                ChangeLandscapeMode(width > height);
            }
        }
        void ChangeLandscapeMode(bool landscape) {
            isLandscape = landscape;
            userAvatar.Margin = new Thickness(0, isLandscape ? landscapeAvatarTopMargin : avatarTopMargin, 0, 0);
            logoutButton.Margin = new Thickness(0, isLandscape ? landscapeLogoutButtonTopMargin : logoutButtonTopMargin, 0, 0);
            logoutBlock.Margin = new Thickness(0, isLandscape ? landscapeLogoutBlockTopMargin : logoutBlockTopMargin, 0, 0);
            pageStack.Orientation = isLandscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
            pageStack.VerticalOptions = isLandscape ? LayoutOptions.CenterAndExpand : pageStackVerticalOptions;
            pageStack.Spacing = isLandscape ? 100 : 0;
        }
    }
}

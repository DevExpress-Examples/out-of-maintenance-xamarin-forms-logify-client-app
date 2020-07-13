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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logify.Mobile.Services;
using Logify.Mobile.ViewModels;
using Xamarin.Forms;

namespace Logify.Mobile.Views {
    public partial class LoginPage : ContentPage {
        bool isLoaded = false;
        bool isLandscape = false;

        double logoTopMargin;
        double landscapeLogoTopMargin = 10;
        double logoBottomMargin;

        double menuSpacing;
        double landscapeMenuSpacing = 10;

        readonly Page mainPage;

        private readonly LoginPageViewModel viewModel;

        public LoginPage() {
            viewModel = new LoginPageViewModel();
            this.BindingContext = viewModel;
            viewModel.PropertyChanged += LoginViewModel_PropertyChanged;

            InitializeComponent();

            logoTopMargin = logo.Margin.Top;
            logoBottomMargin = logo.Margin.Bottom;
            menuSpacing = menu.Spacing;
            mainPage = new MainPage();

            InitButtons();
        }

        void InitButtons() {
            List<ILogifyDataMode> dataModes = LogifyDataModeContext.GetAvailableModes();
            for (int i = 0; i < dataModes.Count; i++) {
                WeakReference dataMode = new WeakReference(dataModes[i]);

                LoginButton button = new LoginButton();
                button.BindingContext = CreateButtonInfo(dataModes[i], i);
                button.OnTapped += (sender, args) => {
                    viewModel.LoginInProcess = true;
                    (dataMode.Target as ILogifyDataMode)?.ProcessLogin(OnAuthenticated, OnCanceled);
                    if (Device.RuntimePlatform == Device.iOS) {
                        Task.Run(async () => {
                            await Task.Delay(200);
                            Device.BeginInvokeOnMainThread(() => { viewModel.LoginInProcess = false; });
                        });
                    }
                };
                button.Spacing = menu.Spacing;

                if (i == dataModes.Count - 1) {
                    Thickness marginStyle = button.Margin;
                    marginStyle.Bottom = 90;
                    button.Margin = marginStyle;
                }

                menu.Children.Add(button);
            }
        }

        private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(LoginPageViewModel.LoginInProcess)) {
                this.Opacity = viewModel.LoginInProcess ? 0.5 : 1;
            }
        }

        protected override async void OnAppearing() {
            if (!isLoaded) {
                if (LogifyDataModeContext.SkipLoginScreen) {
                    await RedirectToReports();
                    return;
                }

                isLoaded = true;
            }

            IsEnabled = true;
            IsBusy = false;
            base.OnAppearing();
        }

        LoginButtonInfo CreateButtonInfo(ILogifyDataMode dataMode, int index) {
            LoginButtonInfo result = new LoginButtonInfo();
            LoginButtonInfo data = dataMode.GetButton();

            StringBuilder title = new StringBuilder();
            if (index > 0)
                title.Append("OR");
            if (index > 0 && !String.IsNullOrEmpty(data.Title))
                title.Append(" ");
            if (!String.IsNullOrEmpty(data.Title))
                title.Append(data.Title);

            result.Title = title.ToString();
            result.ButtonIcon = data.ButtonIcon;
            result.ButtonText = data.ButtonText;
            result.Description = data.Description;

            return result;
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if (width > height != isLandscape) {
                ChangeLandscapeMode(width > height);
            }
        }

        void ChangeLandscapeMode(bool landscape) {
            isLandscape = landscape;
            logo.Margin = new Thickness(0, isLandscape ? landscapeLogoTopMargin : logoTopMargin, 0,
                isLandscape ? 0 : logoBottomMargin);
            for (int i = 0; i < menu.Children.Count; i++) {
                if (menu.Children[i] is StackLayout stackLayout)
                    stackLayout.Spacing = isLandscape ? landscapeMenuSpacing : menuSpacing;
                ;
            }

            menu.Spacing = isLandscape ? landscapeMenuSpacing : menuSpacing + landscapeMenuSpacing;
        }

        async void OnAuthenticated(ILogifyDataMode mode) {
            LogifyDataModeContext.SetMode(mode);
            GlobalSettings.Instance.CleanStoredData();

            Device.BeginInvokeOnMainThread(async () => {
                this.IsBusy = true;
                await RedirectToReports();
                viewModel.LoginInProcess = false;
            });
        }

        void OnCanceled(ILogifyDataMode mode) {
            this.IsBusy = false;
            viewModel.LoginInProcess = false;
        }

        async Task RedirectToReports() {
            if (!Navigation.NavigationStack.Contains(mainPage)) {
                await Navigation.PushAsync(mainPage, true);
                Navigation.RemovePage(this);
            }
        }
    }
}

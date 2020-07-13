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
﻿using DevExpress.XamarinForms.Charts;
using Logify.Mobile.ViewModels.Statistic;
using Xamarin.Forms;

namespace Logify.Mobile.Views.Statistic {
    public partial class CountByStatusesView : Grid, IRefreshProvider {
        LegendHorizontalPosition portraitHorizontalPosition;
        LegendHorizontalPosition landscapeHorizontalPosition = LegendHorizontalPosition.RightOutside;
        LegendVerticalPosition portraitVerticalPosition;
        LegendVerticalPosition landscapeVerticalPosition = LegendVerticalPosition.Top;

        Thickness portraitChartMargin;
        Thickness landscapeChartMargin = 5;

        int selectedIndex = -1;

        bool hintShowing = false;
        bool isLandscape = false;

        public CountByStatusesView() {
            InitializeComponent();
            portraitChartMargin = pieChartView.Margin;
            portraitHorizontalPosition = legend.HorizontalPosition;
            portraitVerticalPosition = legend.VerticalPosition;
        }

        public void Refresh() {
            ((CountByStatusesViewModel)BindingContext).UpdateStatuses();
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            bool landscape = width > height;
            if (isLandscape != landscape) {
                hintShowing = true;
                isLandscape = landscape;
                if (legend != null) {
                    legend.HorizontalPosition = isLandscape ? landscapeHorizontalPosition : portraitHorizontalPosition;
                    legend.VerticalPosition = isLandscape ? landscapeVerticalPosition : portraitVerticalPosition;
                }
                if (pieChartView != null) {
                    pieChartView.Margin = isLandscape ? landscapeChartMargin : portraitChartMargin;
                    SetHintTextStyle((TextStyle)this.Resources[isLandscape ? "landscapeHintTextStyle" : "portraitHintTextStyle"]);
                }
                if (centerLabel != null) {
                    SetCenterTextStyle((TextStyle)this.Resources[isLandscape ? "landscapeCenterTextStyle" : "portraitCenterTextStyle"]);
                }
            } else {
                if (selectedIndex >= 0 && pieChartView != null) {
                    pieChartView.SetSelected(0, selectedIndex, false);
                }
            }
        }
        private void SetCenterTextStyle(TextStyle style) {
            if (centerLabel.Style != null && style != null) {
                centerLabel.Style.TextStyle = style;
            }
        }
        private void SetHintTextStyle(TextStyle style) {
            if (pieChartView.Hint != null && pieChartView.Hint.Style != null && style != null) {
                pieChartView.Hint.Style.TextStyle = style;
            }
        }
    }
}

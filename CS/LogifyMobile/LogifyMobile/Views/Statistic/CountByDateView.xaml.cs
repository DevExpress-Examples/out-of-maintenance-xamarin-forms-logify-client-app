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
using DevExpress.XamarinForms.Charts;
using Logify.Mobile.Services.Statistic;
using Logify.Mobile.ViewModels.Statistic;
using Xamarin.Forms;

namespace Logify.Mobile.Views.Statistic {
    public partial class CountByDateView : StackLayout, IRefreshProvider {

        public StatisticRequestType RequestType { get; set; }

        ChartView chart;
        GridLength dividerColumnWidth;
        GridLength toDateColumnWidth;
        GridLength fromDateColumnWidth;

        public ChartView Chart {
            get => chart;
            set {
                BatchBegin();

                if (chart != null && chartGrid.Children.Contains(chart)) {
                    chartGrid.Children.Remove(chart);
                }
                if (value != null) {
                    chart = value;
                    chart.VerticalOptions = LayoutOptions.FillAndExpand;
                    chartGrid.Children.Add(chart);
                    Grid.SetRow(chart, 1);
                }

                BatchCommit();
            }
        }

        string description;
        public string Description {
            get => description;
            set {
                if (description != value) {
                    description = value;
                    descriptionLabel.Text = description;
                }
            }
        }
        public CountByDateView() {
            InitializeComponent();
            fromDate.Date = DateTime.Now.AddDays(-14);
            toDate.Date = toDate.MaximumDate = fromDate.MaximumDate = DateTime.Now;
            toDateColumnWidth = toDateColumn.Width;
            dividerColumnWidth = dividerColumn.Width;
            fromDateColumnWidth = fromeDateColumn.Width;
        }

        void UpdateChart() {
            if (RequestType != null) {
                BatchBegin();
                ((CountByDateViewModel)BindingContext).DataRangeUpdated(RequestType, fromDate.Date, toDate.Date);
                BatchCommit();
            }
        }

        void Handle_FromDateSelected(object sender, EventArgs e) {
            if (fromDate.Date > toDate.Date) {
                toDate.Date = fromDate.Date;
            }
            UpdateChart();
        }

        void Handle_ToDateSelected(object sender, EventArgs e) {
            if (toDate.Date < fromDate.Date) {
                fromDate.Date = toDate.Date;
            }
            UpdateChart();
        }

        public void Refresh() {
            UpdateChart();
        }
        public void ChangeLandscapeMode(bool landscape) {
            toDateColumn.Width = landscape ? 0: toDateColumnWidth;
            dividerColumn.Width = landscape ? 0 : dividerColumnWidth;
            fromeDateColumn.Width = landscape ? new GridLength(150) : fromDateColumnWidth;
            dateGrid.BatchBegin();
            dateDivider.IsVisible = !landscape;
            Grid.SetRow(toDate, landscape ? 1 : 0);
            Grid.SetColumn(toDate, landscape ? 0 : 2);
            dateGrid.BatchCommit();

            this.innerStack.Orientation = landscape ? StackOrientation.Horizontal : StackOrientation.Vertical;
            totalCountLabel.HorizontalOptions = landscape ? LayoutOptions.End : LayoutOptions.Start;
        }
    }
}

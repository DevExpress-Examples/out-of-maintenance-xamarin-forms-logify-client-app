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
using System.Threading.Tasks;
using Logify.Mobile.Controls;
using Logify.Mobile.Models;
using Logify.Mobile.Services.ReportDetail;
using Plugin.Clipboard;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels.ReportDetail {
    public class ReportDetailViewModel : NotificationObject {
        ReportViewModel reportViewModel;
        PickerViewItem selectedReportStatus;
        readonly List<ReportDetailInfoContainerBase> cards = new List<ReportDetailInfoContainerBase>() { };
        readonly List<PickerViewItem> statuses = new List<PickerViewItem>() { };
        string scrollWrapButtonText;
        bool hasScrollWrapContent = false;
        IReportDetailDataProvider provider;
        public List<ReportDetailInfoContainerBase> Cards => cards;
        public List<PickerViewItem> Statuses => statuses;
        public string AppName { get { return reportViewModel.Report.ApplicationName; } }
        public string ApiKey { get { return reportViewModel.Report.ApiKey; } }
        public string ReportId { get { return reportViewModel.Report.ReportId; } }
        public string ScrollWrapButtonText { get { return scrollWrapButtonText; } }
        public PickerViewItem SelectedReportStatus {
            get { return selectedReportStatus; }
            set {
                selectedReportStatus = value;
                UpdateReportStatus();
            }
        }

        public ReportDetailViewModel(ReportViewModel reportViewModel, IReportDetailDataProvider provider) {
            this.reportViewModel = reportViewModel;
            this.provider = provider;
            InitializeReportStatuses();
        }
        public void ChangeSelectedIndex(int index, bool select) {
            if(IndexIsValid(index)) {
                Cards[index].IsSelected = select;
                hasScrollWrapContent = Cards[index] is ScrolledContentDetailInfoContainer;
            }
        }
        public void ChangeScrollWrapContentStatus() {
            ScrollOrientation changedOrientation = ScrollOrientation.Both;
            foreach (ReportDetailInfoContainerBase card in cards) {
                if (card is ScrolledContentDetailInfoContainer container) {
                    container.ScrollWrapCommand.Execute(null);
                    if (changedOrientation == ScrollOrientation.Both)
                        changedOrientation = container.ScrollOrientation;
                }
            }
            if (changedOrientation != ScrollOrientation.Both) {
                scrollWrapButtonText = changedOrientation == ScrollOrientation.Vertical ? "Wrap text" : "Scroll text";
            }
        }
        public bool HasScrollWrapContent() {
            return hasScrollWrapContent;
        }
        public void MakePublicLink() {
            Task.Run(async () => {
                string link = await provider.MakePublicLink(ApiKey, ReportId);
                CrossClipboard.Current.SetText(link);
            });            
        }
        public void CopyLink() {
            Task.Run(async () => {
                string link = await provider.GetReportLink(ApiKey, ReportId);
                CrossClipboard.Current.SetText(link);
            });
        }
        bool IndexIsValid(int index) {
            return index < Cards.Count && index >= 0;
        }
        void InitializeReportStatuses() {
            foreach (ReportStatus status in Enum.GetValues(typeof(ReportStatus))) {
                PickerViewItem currentItem = new PickerViewItem() {
                    Text = ReportStatusNames.Name[status].ToUpper(),
                    TextColor = (Color)Logify.Mobile.Resources.Values[$"ReportStatus{status}"],
                    Value = status
                };
                statuses.Add(currentItem);
                if(reportViewModel.Report.Status == status) {
                    selectedReportStatus = currentItem;
                }
            }
        }
        void UpdateReportStatus() {
            if(selectedReportStatus.Value is ReportStatus status && reportViewModel.Report.Status != status) {
                reportViewModel.UpdateStatus(status);
            }
        }
    }
}

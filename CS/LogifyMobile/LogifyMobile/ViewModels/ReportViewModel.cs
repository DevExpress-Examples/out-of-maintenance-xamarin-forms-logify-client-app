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
using System.Text;
using System.Threading.Tasks;
using Logify.Mobile.Models;
using Logify.Mobile.Services.Reports;
using Xamarin.Forms;

namespace Logify.Mobile.ViewModels {
    public class ReportViewModel : NotificationObject {
        public Report Report { get; private set; }
        IReportRepository reportsRepository;

        public ReportViewModel(Report report, IReportRepository reportsRepository) {
            Report = report;
            this.reportsRepository = reportsRepository;
        }

        public int AffectedUsersCount => Report.AffectedUsersCount;
        public bool HasAffectedUsers => Report.AffectedUsersCount > 0;

        public int Counter => Report.Counter;
        public bool HasCounter => Report.Counter > 0;

        public string ApplicationName => Report.ApplicationName;
        public string Version => Report.Version;
        public string ShortInfo => Report.ShortInfo;

        public Color StatusColor => (Color)Resources.Values[$"ReportStatus{Report.Status.ToString()}"];

        public bool HasDateTimeLastReport => !string.IsNullOrEmpty(DateTimeLastReport);
        string dateTimeLastReport = null;
        public string DateTimeLastReport {
            get {
                if (dateTimeLastReport == null) {
                    var sb = new StringBuilder();
                    var nowDateTime = DateTime.UtcNow;
                    if (Report.DateTime > DateTime.MinValue) {
                        sb.Append(ElapsedString(Report.DateTime, nowDateTime));
                    }
                    if (Report.LastReport > DateTime.MinValue) {
                        if (sb.Length > 0) {
                            sb.Append(" / ");
                        }
                        sb.Append(ElapsedString(Report.LastReport, nowDateTime));
                    }
                    dateTimeLastReport = sb.ToString();
                }
                return dateTimeLastReport;
            }
        }

        public void UpdateStatus(ReportStatus status) {
            if(status != Report.Status) {
                Task.Run(() => reportsRepository.UpdateStatus(Report, status));
                Report.Status = status;
                OnPropertyChanged("StatusColor");
            }
        }

        string ElapsedString(DateTime fromDateTime, DateTime toDateTime) {
            var span = toDateTime - fromDateTime;
            if (span.Days > 365) {
                return span.Days / 365 + "Y";
            }
            var spanMonths = (12 * span.Days) / 365;
            if (spanMonths > 0) {
                return spanMonths + "M";
            }
            if (span.Days > 0) {
                return span.Days + "d";
            }
            if (span.Hours > 0) {
                return span.Hours + "h";
            }
            if (span.Minutes > 0) {
                return span.Minutes + "m";
            }
            return "a few seconds";
        }
    }
}

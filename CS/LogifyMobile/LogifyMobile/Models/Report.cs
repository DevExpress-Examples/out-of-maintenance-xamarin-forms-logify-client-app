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
using Newtonsoft.Json;

namespace Logify.Mobile.Models {

    public enum ReportStatus {
        Active = 0,
        ClosedOnce = 1, 
        ClosedInVersion = 2,
        IgnoredPermanently = 3, 
        IgnoredByRule = 4,
        IgnoredOnce = 5, 
    }

    public static class ReportStatusNames {
        public static readonly IDictionary<ReportStatus, string> Name = new Dictionary<ReportStatus, string>() {
            {ReportStatus.Active, "Active"},
            {ReportStatus.ClosedOnce, "Closed Once"},
            {ReportStatus.ClosedInVersion, "Closed in Version"},
            {ReportStatus.IgnoredPermanently, "Ignored Always"},
            {ReportStatus.IgnoredByRule, "Ignored by Rule"},
            {ReportStatus.IgnoredOnce, "Ignored Once"},
        };
    }

    public class Report {
        public string ReportId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime LastReport { get; set; }
        public string ApiKey { get; set; }
        public string ApplicationName { get; set; }
        [JsonProperty("ReportsListInfo")]
        public string ShortInfo { get; set; }
        public string Version { get; set; }
        public int Counter { get; set; }
        public int AffectedUsersCount { get; set; }
        public ReportStatus Status { get; set; }
    }
}

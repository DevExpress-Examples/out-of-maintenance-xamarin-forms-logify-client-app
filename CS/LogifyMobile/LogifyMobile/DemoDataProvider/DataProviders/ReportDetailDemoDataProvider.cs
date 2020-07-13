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
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Logify.Mobile.Models;

namespace Logify.Mobile.Services.ReportDetails {
    public class ReportDetailsDemoDataProvider : IReportDetailsDataProvider {
        public Task<ExceptionReport> LoadReport(string apiKey, string reportId) {
            return MockData.GetObject<ExceptionReport>("Reports." + reportId);
        }
        public Task<string> LoadRawReport(string apiKey, string reportId) {
            return MockData.GetRaw("RawReports." + reportId);
        }
        public async Task<string> SaveComments(string apiKey, string reportId, string comments) {
            await Task.Delay(100);
            return "true";
        }
        public Task<List<object>> LoadByRefURL(string refURL) {
            return MockData.GetObject<List<object>>(refURL);
        }

        public Task<string> MakePublicLink(string apiKey, string reportId) {
            return Task.FromResult($"logify/Report/Public/{apiKey}/{reportId}");
        }

        public Task<string> GetReportLink(string apiKey, string reportId) {
            return Task.FromResult($"logify/Report/{apiKey}/{reportId}");
        }
    }
}

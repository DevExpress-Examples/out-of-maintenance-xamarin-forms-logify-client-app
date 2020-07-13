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
using System.Linq;
using System.Threading.Tasks;
using Logify.Mobile.Models;

namespace Logify.Mobile.Services.ApplicationsDetail {
    public class ApplicationsDetailDemoDataProvider : IApplicationsDetailDataProvider {

        public async Task<IEnumerable<ApplicationDetail>> GetApplicationsDetail() {
            var settings = GlobalSettings.Instance;
            var apps = (await MockData.GetObject<IEnumerable<ApplicationDemoDetail>>("ApplicationsDetailView.json")).ToList();

            var result = new List<ApplicationDetail>();

            foreach(ApplicationDemoDetail currentApp in apps) {
                if (currentApp.SubscriptionId == settings.SubscriptionId &&
                    (string.IsNullOrEmpty(currentApp.TeamId) || currentApp.TeamId == settings.TeamId)) {
                    result.Add((ApplicationDetail)currentApp);
                }
            }
            return result.OrderBy(a => a.AppName);
        }
    }
}

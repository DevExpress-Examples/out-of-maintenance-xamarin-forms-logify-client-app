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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logify.Mobile.Services {
    public static class LogifyDataModeContext {
        readonly static List<ILogifyDataMode> dataModesInstances = new List<ILogifyDataMode>();
        static ILogifyDataMode selectedMode;

        static LogifyDataModeContext() {
            InitializeModes();
        }

        public static List<ILogifyDataMode> GetAvailableModes() => dataModesInstances;

        public static bool SkipLoginScreen => SelectedMode != null && SelectedMode.SkipLoginPage;
        public static ILogifyDataMode SelectedMode {
            get => selectedMode;
        }

        public static void SetMode(ILogifyDataMode mode) {
            if (selectedMode != null) {
                selectedMode.ProcessLogout();
            }

            selectedMode = mode;
        }

        static void InitializeModes() {
            List<Type> dataModes = GetAllDataModes();
            foreach (Type dataMode in dataModes) {
                dataModesInstances.Add(Activator.CreateInstance(dataMode) as ILogifyDataMode);
            }
        }

        static List<Type> GetAllDataModes() {
            Type derivedType = typeof(ILogifyDataMode);
            return Assembly
                .GetAssembly(typeof(App))
                .GetTypes()
                .Where(t => t != derivedType && typeof(ILogifyDataMode).IsAssignableFrom(t))
                .ToList();
        }
    }
}

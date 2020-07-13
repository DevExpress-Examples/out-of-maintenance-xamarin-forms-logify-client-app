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
using Logify.Mobile.ViewModels.ReportDetail;
using Xamarin.Forms;

namespace Logify.Mobile.Views.ReportDetailTemplates {
    public partial class CommentsDetailView : CardBaseReportDetailView {
        public bool SaveMode { get; set; }
        public CommentsDetailView() {
            InitializeComponent();
            this.BindingContextChanged += CommentsDetailView_BindingContextChanged;
            
        }

        private void CommentsDetailView_BindingContextChanged(object sender, EventArgs e) {
            if (this.BindingContext is CommentsDetailInfoContainer container) {
                container.PropertyChanged += CommentsDetailView_PropertyChanged;
            }
        }

        private void CommentsDetailView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(CommentsDetailInfoContainer.IsSelected)) {
                textEdit.Unfocus();
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e) {
            if (SaveMode) {
                textEdit.Unfocus();
            } else {
                textEdit.Focus();
            }
            ToggleSaveMode();
        }
        private void TextEdit_TextChanged(object sender, TextChangedEventArgs e) {
            if (editIcon.IsVisible && e.OldTextValue != null) {
                ToggleSaveMode();
            }
        }
        void ToggleSaveMode() {
            SaveMode = !SaveMode;
            editIcon.IsVisible = !SaveMode;
            saveIcon.IsVisible = SaveMode;
        }
    }
}

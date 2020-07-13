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
using Logify.Mobile.ViewModels.ReportDetails;
using Xamarin.Forms;

namespace Logify.Mobile.Views.ReportDetailsTemplates {
    public partial class CommentView : CardBaseReportDetailsView {
        public bool SaveMode { get; set; }
        public CommentView() {
            InitializeComponent();
            BindingContextChanged += CommentView_BindingContextChanged;
            
        }

        private void CommentView_BindingContextChanged(object sender, EventArgs e) {
            if (BindingContext is CommentsDetailInfoContainer container) {
                container.PropertyChanged += CommentView_PropertyChanged;
            }
        }

        private void CommentView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(CommentsDetailInfoContainer.IsSelected)) {
                this.textEdit.Unfocus();
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e) {
            if (SaveMode) {
                this.textEdit.Unfocus();
            } else {
                this.textEdit.Focus();
            }
            ToggleSaveMode();
        }
        private void TextEdit_TextChanged(object sender, TextChangedEventArgs e) {
            if (this.editIcon.IsVisible && e.OldTextValue != null) {
                ToggleSaveMode();
            }
        }
        void ToggleSaveMode() {
            SaveMode = !SaveMode;
            this.editIcon.IsVisible = !SaveMode;
            this.saveIcon.IsVisible = SaveMode;
        }
    }
}

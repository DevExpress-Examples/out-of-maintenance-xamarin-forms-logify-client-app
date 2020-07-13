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
using System.Linq;
using System.Threading.Tasks;
using Logify.Mobile.Controls;
using Logify.Mobile.Services;

namespace Logify.Mobile.ViewModels {
    public abstract class PickerViewModelBase : NotificationObject {
        protected IGlobalSettings settings = GlobalSettings.Instance;

        List<PickerViewItem> items;
        public List<PickerViewItem> Items {
            get => items;
            private set => SetProperty(ref items, value);
        }

        public PickerViewItem SelectedItem {
            get {
                var id = SelectedItemId;
                return Items?.FirstOrDefault(x => x.Value.Equals(id));
            }
            set {
                if (value != null && !value.Value.Equals(SelectedItemId)) {
                    SelectedItemId = (string)value.Value;
					UpdateSelectedItem(value);
					OnPropertyChanged();
                }
            }
        }
        void UpdateSelectedItem(PickerViewItem selectedItem) {
            foreach(PickerViewItem item in items) {
				item.Selected = item == selectedItem;
			}
		}

        public bool IsMultiItems {
            get => items != null && items.Count > 1;
        }

        Task updateItemsTask;
        public Task UpdateItemsAsync() {
            Console.WriteLine($"   --- start updateItemsAsync {this} ");
            if (updateItemsTask == null) {
                lock (typeof(PickerViewModelBase)) {
                    Console.WriteLine($"   ---  updateItemsAsync enter lock section {this} ");
                    if (updateItemsTask == null) {
                        Console.WriteLine($"   ---   updateItemsAsync start loaditems {this} ");
                        updateItemsTask = LoadItems()
                            .ContinueWith(_ => {
                                Console.WriteLine($"   ---  updateItemsAsync finish loaditems {this} ");
                                Items = _.Result.ToPickerViewItems();
                                if (items.Any() && SelectedItem == null) {
                                    SelectedItemId = (string)items.First().Value;
                                }
                                Console.WriteLine($"   ---  updateItemsAsync setSelected item {this} ");
                                OnPropertyChanged(nameof(SelectedItem));
                                OnPropertyChanged(nameof(IsMultiItems));
                                OnItemsUpdated().Wait();
                                updateItemsTask = null;
                                Console.WriteLine($"   ---  updateItemsAsync = null {this} ");
                            });
                    }
                }

            } else {
                Console.WriteLine($"   ---  updateItemsAsync TASK EXISTS {this} ");
            }
            return updateItemsTask;
        }

        protected abstract Task OnItemsUpdated();
        protected abstract string SelectedItemId { get; set; }
        protected abstract Task<IDictionary<string, string>> LoadItems();
    }

    public class TeamsViewModel : PickerViewModelBase {
        static readonly Lazy<TeamsViewModel> viewModel = new Lazy<TeamsViewModel>(() => new TeamsViewModel(), true);
        public static TeamsViewModel Instance => viewModel.Value;

        public TeamsViewModel() {
            SubscriptionsViewModel.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private async void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(SelectedItem)) {
                await UpdateItemsAsync();
            }
        }

        protected override string SelectedItemId {
            get => settings.TeamId;
            set => settings.TeamId = value;
        }

        protected override Task<IDictionary<string, string>> LoadItems() => DataProviderFactory.CreateTeamsDataProvider().GetTeams();

        protected override Task OnItemsUpdated() {
            return Task.CompletedTask;
        }
    }
}

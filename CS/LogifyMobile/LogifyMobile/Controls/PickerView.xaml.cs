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
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Logify.Mobile.Controls {
    public partial class PickerView : Grid {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource), typeof(IList), typeof(PickerView),
            null, propertyChanged: ItemsSourcePropertyChanged);
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex), typeof(int), typeof(PickerView),
            -1, propertyChanged: SelectedIndexPropertyChanged);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem), typeof(object), typeof(PickerView),
            null, propertyChanged: SelectedItemPropertyChanged);
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily), typeof(string), typeof(PickerView),
            string.Empty, propertyChanged: FontFamilyPropertyChanged);
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            nameof(FontAttributes), typeof(FontAttributes), typeof(PickerView),
            FontAttributes.None, propertyChanged: FontAttributesPropertyChanged);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(double), typeof(PickerView),
            propertyChanged: FontSizePropertyChanged);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(PickerView),
            string.Empty, propertyChanged: TitlePropertyChanged);
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            nameof(TitleColor), typeof(Color), typeof(PickerView),
            Color.Default, propertyChanged: TitleColorPropertyChanged);
        public static readonly BindableProperty PositiveButtonTextProperty = BindableProperty.Create(
            nameof(PositiveButtonText), typeof(string), typeof(PickerView),
            string.Empty, propertyChanged: PositiveButtonTextPropertyChanged);
        public static readonly BindableProperty PositiveButtonColorProperty = BindableProperty.Create(
            nameof(PositiveButtonColor), typeof(Color), typeof(PickerView),
            Color.Default, propertyChanged: PositiveButtonColorPropertyChanged);
        public static readonly BindableProperty NegativeButtonTextProperty = BindableProperty.Create(
            nameof(NegativeButtonText), typeof(string), typeof(PickerView),
            string.Empty, propertyChanged: NegativeButtonTextPropertyChanged);
        public static readonly BindableProperty NegativeButtonColorProperty = BindableProperty.Create(
            nameof(NegativeButtonColor), typeof(Color), typeof(PickerView),
            Color.Default, propertyChanged: NegativeButtonColorPropertyChanged);
        public static readonly BindableProperty SelectedItemChangeCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangeCommand), typeof(ICommand), typeof(PickerView));
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor), typeof(Color), typeof(PickerView));
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            nameof(BorderWidth), typeof(int), typeof(PickerView), 1);
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor), typeof(Color), typeof(PickerView),
            Color.Default, propertyChanged: PlaceholderColorPropertyChanged);

        static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetItemsSource((IList)newValue);
        }
        static void SelectedIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetSelectedIndex((int)newValue);
        }
        static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetSelectedItem(newValue);
		}
		static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetFontFamily((string)newValue);
        }
        static void FontAttributesPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetFontAttributes((FontAttributes)newValue);
        }
        static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetFontSize((double)newValue);
        }
        static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetTitle((string)newValue);
        }
        static void TitleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetTitleColor((Color)newValue);
        }
        static void PositiveButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetPositiveButtonText((string)newValue);
        }
        static void PositiveButtonColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetPositiveButtonColor((Color)newValue);
        }
        static void NegativeButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetNegativeButtonText((string)newValue);
        }
        static void NegativeButtonColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).SetNegativeButtonColor((Color)newValue);
        }
        static void PlaceholderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ((PickerView)bindable).picker.TextColor = ((Color)newValue);
        }

        public IList ItemsSource {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public int SelectedIndex {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        public object SelectedItem {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public String FontFamily {
            get => (String)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        public FontAttributes FontAttributes {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        public double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public string Title {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public Color TitleColor {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }
        public string PositiveButtonText {
            get => (string)GetValue(PositiveButtonTextProperty);
            set => SetValue(PositiveButtonTextProperty, value);
        }
        public Color PositiveButtonColor {
            get => (Color)GetValue(PositiveButtonColorProperty);
            set => SetValue(PositiveButtonColorProperty, value);
        }
        public string NegativeButtonText {
            get => (string)GetValue(NegativeButtonTextProperty);
            set => SetValue(NegativeButtonTextProperty, value);
        }
        public Color NegativeButtonColor {
            get => (Color)GetValue(NegativeButtonColorProperty);
            set => SetValue(NegativeButtonColorProperty, value);
        }
        public ICommand SelectedItemChangeCommand {
            get => (ICommand)GetValue(SelectedItemChangeCommandProperty);
            set => SetValue(SelectedItemChangeCommandProperty, value);
        }
        public Color BorderColor {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public int BorderWidth {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        public Color PlaceholderColor {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public event EventHandler SelectedIndexChanged;

        public PickerView() {
            InitializeComponent();
            picker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
        }
        void SetItemsSource(IList itemSource) {
            picker.ItemsSource = itemSource;
            IsEnabled = icon.IsVisible = itemSource != null ? (itemSource?.Count > 1) : false;
            
        }
        void SetSelectedIndex(int index) {
            picker.SelectedIndex = index;
        }
        void SetSelectedItem(object item) {
            if (picker.SelectedItem != item) {
				if (picker.SelectedItem is PickerViewItem oldItem)
					oldItem.Selected = false;
				if (item is PickerViewItem newItem)
					newItem.Selected = true;
				picker.SelectedItem = item;
                SetItemTextColor(item);
			}
		}
        void SetItemTextColor(object item) {
            if (item is PickerViewItem pickerViewItem && PlaceholderColor.Equals(Color.Default)) {
                picker.TextColor = pickerViewItem.TextColor;
            }
        }
        void SetFontFamily(string font) {
            picker.FontFamily = font;
        }
        void SetFontAttributes(FontAttributes fontAttributes) {
            picker.FontAttributes = fontAttributes;
        }
        void SetFontSize(double fontSize) {
            picker.FontSize = fontSize;
        }
        void SetTitle(string title) {
            picker.Title = title;
        }
        void SetTitleColor(Color titleColor) {
            picker.TitleColor = titleColor;
        }
        void SetPositiveButtonText(string positiveButtonText) {
            picker.PositiveButtonText = positiveButtonText;
        }
        void SetPositiveButtonColor(Color positiveButtonColor) {
            picker.PositiveButtonColor = positiveButtonColor;
        }
        void SetNegativeButtonText(string negativeButtonText) {
            picker.NegativeButtonText = negativeButtonText;
        }
        void SetNegativeButtonColor(Color negativeButtonColor) {
            picker.NegativeButtonColor = negativeButtonColor;
        }
        void Picker_SelectedIndexChanged(object sender, EventArgs e) {
            SetItemTextColor(picker.SelectedItem);
            SelectedItem = picker.SelectedItem;
            SelectedIndex = picker.SelectedIndex;
            SelectedIndexChanged?.Invoke(this, e);
            SelectedItemChangeCommand?.Execute(this);
        }
        void PickerView_Tapped(object sender, EventArgs e) {
            picker.ShowDialog();
        }
        public void UnfocusPicker() {
            picker.Unfocus();
        }
    }
}

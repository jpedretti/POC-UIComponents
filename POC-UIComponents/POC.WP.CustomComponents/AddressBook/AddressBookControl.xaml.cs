using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace POC.WP.CustomComponents.AddressBook
{
    public sealed partial class AddressBookControl : UserControl
    {
        #region Properties
        public static readonly DependencyProperty ListSourceProperty =
            DependencyProperty.Register("ListSource", typeof(object), typeof(AddressBookControl), new PropertyMetadata(null, SourceChanged));

        public static readonly DependencyProperty IndexSourceProperty =
            DependencyProperty.Register("IndexSource", typeof(object), typeof(AddressBookControl), new PropertyMetadata(null, SourceChanged));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(AddressBookControl), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(long), typeof(AddressBookControl), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(AddressBookControl), new PropertyMetadata(null));


        public object ListSource
        {
            get { return (object)GetValue(ListSourceProperty); }
            set { SetValue(ListSourceProperty, value); }
        }

        public object IndexSource
        {
            get { return (object)GetValue(IndexSourceProperty); }
            set { SetValue(IndexSourceProperty, value); }
        }


        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public long SelectedIndex
        {
            get { return (long)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }


        #endregion

        #region Constructors
        public AddressBookControl()
        {
            this.InitializeComponent();
            this.listAddressBook.SelectionChanged += SelectionChanged;
        }


        #endregion

        #region Event Handlers
        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as AddressBookControl;

            instance.listAddressBook.ItemsSource = instance.ListSource;
            instance.gridAddressBook.ItemsSource = instance.IndexSource;
        }

        void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (this.AddressBookSelectionChanged != null)
            //    this.AddressBookSelectionChanged(sender, e);
        }
        #endregion


        #region Methods

        #endregion

    }
}

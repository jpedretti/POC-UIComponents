using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Windows.Input;
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

        public static readonly DependencyProperty ShowCheckboxesProperty =
            DependencyProperty.Register("ShowCheckboxes", typeof(bool), typeof(AddressBookControl), new PropertyMetadata(null, ShowCheckboxesChanged));


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

        public bool ShowCheckboxes
        {
            get { return (bool)GetValue(ShowCheckboxesProperty); }
            set { SetValue(ShowCheckboxesProperty, value); }
        }



        public ObservableCollection<object> SelectedItems
        {
            get { return (ObservableCollection<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<object>), typeof(AddressBookControl), new PropertyMetadata(null));

        


        public Visibility CheckboxesVisibility
        {
            get { return (Visibility)GetValue(CheckboxesVisibilityProperty); }
            set { SetValue(CheckboxesVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckboxesVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckboxesVisibilityProperty =
            DependencyProperty.Register("CheckboxesVisibility", typeof(Visibility), typeof(AddressBookControl), new PropertyMetadata(Visibility.Collapsed));

        

        #endregion

        #region Constructors
        public AddressBookControl()
        {
            this.InitializeComponent();
            SelectedItems = new ObservableCollection<object>();
        }

        void chkSelected_Click(object sender, RoutedEventArgs e)
        {
            var chk = (sender as CheckBox);

            if (SelectedItems.Contains(chk.DataContext) && !chk.IsChecked.Value)
                SelectedItems.Remove(chk.DataContext);
            else
                if(chk.IsChecked.Value)
                    SelectedItems.Add(chk.DataContext);
        }


        #endregion

        #region Event Handlers
        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as AddressBookControl;

            instance.listAddressBook.ItemsSource = instance.ListSource;
            instance.gridAddressBook.ItemsSource = instance.IndexSource;


        }

        private static void ShowCheckboxesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as AddressBookControl;

            if (instance.ShowCheckboxes)
                instance.CheckboxesVisibility = Windows.UI.Xaml.Visibility.Visible;
            else
                instance.CheckboxesVisibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        #endregion


        #region Methods

        #endregion

    }
}

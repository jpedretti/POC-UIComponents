using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NavigationSearchBar.CustomControls
{
    public sealed partial class NavigationSearchBar : UserControl
    {
        public NavigationSearchBar()
        {
            this.InitializeComponent();
        }

        public bool HasBack
        {
            get { return (bool)GetValue(HasBackProperty); }
            set { SetValue(HasBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasBackProperty =
            DependencyProperty.Register("HasBack", typeof(bool), typeof(NavigationSearchBar), new PropertyMetadata(0));

        
    }
}

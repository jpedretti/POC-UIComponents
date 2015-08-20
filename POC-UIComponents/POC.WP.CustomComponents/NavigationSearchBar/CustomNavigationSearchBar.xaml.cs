using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace POC.WP.CustomComponents.NavigationSearchBar
{
    public sealed partial class CustomNavigationSearchBar : UserControl
    {
        public CustomNavigationSearchBar()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region XAML Properties

        /// <summary>
        /// Indica se a barra possui o botão de voltar
        /// </summary>
        public Visibility BackButtonVisibility
        {
            get { return (Visibility)GetValue(BackButtonVisibilityProperty); }
            set
            { 
                SetValue(BackButtonVisibilityProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for BackButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.Register("BackButtonVisibility", typeof(Visibility), typeof(CustomNavigationSearchBar), new PropertyMetadata(Visibility.Collapsed));


        /// <summary>
        /// Inidica se a barra possui o botão de busca/filtro
        /// </summary>
        public Visibility SearchButtonVisibility
        {
            get { return (Visibility)GetValue(SearchButtonVisibilityProperty); }
            set
            {
                SetValue(SearchButtonVisibilityProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for SearchButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchButtonVisibilityProperty =
            DependencyProperty.Register("SearchButtonVisibility", typeof(Visibility), typeof(CustomNavigationSearchBar), new PropertyMetadata(Visibility.Collapsed));


        /// <summary>
        /// Texto a ser exibido na barra
        /// </summary>
        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(String.Empty));


        /// <summary>
        /// Texto fornecido como busca
        /// </summary>
        public String SearchTerm
        {
            get { return (String)GetValue(SearchTermProperty); }
            set
            {
                SetValue(SearchTermProperty, value);
                RaisePropertyChanged("SearchTerm");
            }
        }

        // Using a DependencyProperty as the backing store for SearchTerm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTermProperty =
            DependencyProperty.Register("SearchTerm", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(String.Empty));


        /// <summary>
        /// Caminho para o ícone a ser usado no botão de busca
        /// </summary>
        public String SearchIconPath
        {
            get { return (String)GetValue(SearchIconPathProperty); }
            set { SetValue(SearchIconPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchIconPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchIconPathProperty =
            DependencyProperty.Register("SearchIconPath", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(String.Empty));


        /// <summary>
        /// Caminho para o ícone a ser usado no botãode voltar
        /// </summary>
        public String BackIconPath
        {
            get { return (String)GetValue(BackIconPathProperty); }
            set { SetValue(BackIconPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackIconPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackIconPathProperty =
            DependencyProperty.Register("BackIconPath", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(String.Empty));

        

        #endregion
    }
}

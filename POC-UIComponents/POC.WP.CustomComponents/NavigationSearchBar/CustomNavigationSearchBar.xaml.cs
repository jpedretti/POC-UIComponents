using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace POC.WP.CustomComponents.NavigationSearchBar
{
    /// <summary>
    /// Barra de navegação com botão de voltar configurável e barra de busca.
    /// </summary>
    public sealed partial class CustomNavigationSearchBar : UserControl
    {
        public CustomNavigationSearchBar()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;

            //registra os eventos default de click nos botões
            this.searchButton.Click += searchButton_Click_WhenSearchTextBoxCollapsed;
            this.backButton.Click += backButton_Click;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region XAML and State Controls

        private DispatcherTimer searchTextBoxVisibilityDispatcherTime = null;
        public bool HasSearchValue
        {
            get
            {
                return (this.SearchButtonVisibility == Visibility.Visible
                       && !string.IsNullOrWhiteSpace(this.SearchTerm));
            }
        }

        #endregion

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
        public static readonly DependencyProperty SearchTermProperty =
            DependencyProperty.Register("SearchTerm", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(String.Empty));


        /// <summary>
        /// Caminho para o ícone a ser usado no botão de busca
        /// </summary>
        public String SearchIconSource
        {
            get { return (String)GetValue(SearchIconSourceProperty); }
            set { SetValue(SearchIconSourceProperty, value); }
        }
        public static readonly DependencyProperty SearchIconSourceProperty =
            DependencyProperty.Register("SearchIconSource", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(null));


        /// <summary>
        /// Caminho para o ícone a ser usado no botãode voltar
        /// </summary>
        public String BackIconSource
        {
            get { return (String)GetValue(BackIconSourceProperty); }
            set { SetValue(BackIconSourceProperty, value); }
        }
        public static readonly DependencyProperty BackIconSourceProperty =
            DependencyProperty.Register("BackIconSource", typeof(String), typeof(CustomNavigationSearchBar), new PropertyMetadata(null));

        /// <summary>
        /// Command a ser executado quando o botão de voltar for clicado
        /// </summary>
        public ICommand BackButtonCommand
        {
            get { return (ICommand)GetValue(BackButtonCommandProperty); }
            set
            {
                SetValue(BackButtonCommandProperty, value);
                RaisePropertyChanged();
            }
        }
        private static readonly DependencyProperty BackButtonCommandProperty =
            DependencyProperty.Register("BackButtonCommand", typeof(ICommand), typeof(CustomNavigationSearchBar), new PropertyMetadata(null));

        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (BackButtonCommand != null && BackButtonCommand.CanExecute(null))
            {
                BackButtonCommand.Execute(null);
            }
        }

        private async void searchButton_Click_WhenSearchTextBoxCollapsed(object sender, RoutedEventArgs e)
        {
            if (HasSearchValue)
            {
                //se houver valor pesquisado, dá foco no campo
                searchTextBox.Focus(FocusState.Programmatic);
                return;
            }

            //faz a caixa de texto aparecer
            VisualStateManager.GoToState(this, "SearchActivated", true);

            //dá foco no controle
            await System.Threading.Tasks.Task.Delay(750);
            searchTextBox.Focus(FocusState.Programmatic);

            //altera os eventos
            searchButton.Click -= searchButton_Click_WhenSearchTextBoxCollapsed;
            searchButton.Click += searchButton_Click_WhenSearchTextBoxVisible;

            searchTextBox.LostFocus += searchTextBox_LostFocus;
        }

        private void searchButton_Click_WhenSearchTextBoxVisible(object sender, RoutedEventArgs e)
        {
            if (HasSearchValue)
            {
                //se houver valor pesquisado, dá foco no campo
                searchTextBox.Focus(FocusState.Programmatic);
            }
            else
            {
                //caso contrário reseta os eventos associados aos controles envolvidos
                if (searchTextBoxVisibilityDispatcherTime != null && searchTextBoxVisibilityDispatcherTime.IsEnabled)
                {
                    searchTextBoxVisibilityDispatcherTime.Stop();
                }

                //altera os eventos
                searchButton.Click += searchButton_Click_WhenSearchTextBoxCollapsed;
                searchButton.Click -= searchButton_Click_WhenSearchTextBoxVisible;

                searchTextBox.LostFocus -= searchTextBox_LostFocus;
            }
        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (HasSearchValue)
            {
                //se houver valor pesquisado apenas deixa o lost focus ocorrer
                return;
            }
            else
            {
                //se não há valor pesquisado, faz a caixa de busca sumir
                VisualStateManager.GoToState(this, "SearchHidden", true);

                //configura um dispatcher para resetar os eventos associados aos controles de busca
                searchTextBoxVisibilityDispatcherTime = new DispatcherTimer();
                searchTextBoxVisibilityDispatcherTime.Tick += (sdt, edt) =>
                {
                    searchButton_Click_WhenSearchTextBoxVisible(null, null);
                };
                searchTextBoxVisibilityDispatcherTime.Interval = TimeSpan.FromMilliseconds(1000);
                searchTextBoxVisibilityDispatcherTime.Start();
            }
        }

    }
}

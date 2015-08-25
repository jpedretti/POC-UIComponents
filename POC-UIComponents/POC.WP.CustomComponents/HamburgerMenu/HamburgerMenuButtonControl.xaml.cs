using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace POC.WP.CustomComponents.HamburgerMenu
{
    public sealed partial class HamburgerMenuButtonControl : UserControl
    {
        #region Properties

        public static readonly DependencyProperty MenuViewProperty =
            DependencyProperty.Register("MenuView", typeof(Panel), typeof(HamburgerMenuButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuOverlayProperty =
            DependencyProperty.Register("MenuOverlay", typeof(Panel), typeof(HamburgerMenuButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuViewWidthProperty =
            DependencyProperty.Register("MenuViewWidth", typeof(double), typeof(HamburgerMenuButtonControl), new PropertyMetadata(null));

        public static readonly DependencyProperty AnimateMenuProperty =
            DependencyProperty.Register("AnimateMenu", typeof(bool), typeof(HamburgerMenuButtonControl), new PropertyMetadata(true));

        public Panel MenuView
        {
            get { return (Panel)GetValue(MenuViewProperty); }
            set { SetValue(MenuViewProperty, value); }
        }

        public Panel MenuOverlay
        {
            get { return (Panel)GetValue(MenuOverlayProperty); }
            set { SetValue(MenuOverlayProperty, value); }
        }

        public bool AnimateMenu
        {
            get { return (bool)GetValue(AnimateMenuProperty); }
            set { SetValue(AnimateMenuProperty, value); }
        }

        public double MenuViewWidth
        {
            get { return (double)GetValue(MenuViewWidthProperty); }
            set { SetValue(MenuViewWidthProperty, value); }
        }


        public bool IsPanelOpen { get; set; }

        private bool _hasMenuOverlayLoaded = false;

        #endregion

        #region Constructor
        public HamburgerMenuButtonControl()
        {
            this.InitializeComponent();
            this.btnHambugerMenu.Click += btnHambugerMenu_Click;

            this.LayoutUpdated += HamburgerMenuButtonControl_LayoutUpdated;
        }
        #endregion

        #region Event Handlers

        void HamburgerMenuButtonControl_LayoutUpdated(object sender, object e)
        {
            Setup();
        }

        void btnHambugerMenu_Click(object sender, RoutedEventArgs e)
        {
            ChangePanelState();
        }
        #endregion

        #region Methods

        private void Setup()
        {
            if (!_hasMenuOverlayLoaded && MenuOverlay != null)
            {
                _hasMenuOverlayLoaded = true;

                MenuOverlay.Tapped += (s, e) => { ClosePanel(); };
            }
        }

        private void ChangePanelState()
        {
            if (MenuView.Margin.Left > 0)
                ClosePanel();
            else
                OpenPanel();
        }



        private async Task OpenPanel(bool cancelAnimation = false)
        {
            MenuView.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MenuOverlay.Visibility = Windows.UI.Xaml.Visibility.Visible;

            if (AnimateMenu && !cancelAnimation)
            {
                // Animate opening the panel
                ShowOverlay();

                var margin = MenuView.Margin;

                while (MenuView.Margin.Left < 0)
                {
                    margin.Left += (MenuViewWidth / 10);
                    MenuView.Margin = margin;
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                }
            }
            else
            {
                MenuView.Width = MenuViewWidth;
            }

            IsPanelOpen = true;
        }

        private async Task ClosePanel(bool cancelAnimation = false)
        {
            if (AnimateMenu && !cancelAnimation)
            {
                var margin = MenuView.Margin;

                // Animate opening the panel
                while (MenuView.Margin.Left > (MenuViewWidth * -1))
                {
                    margin.Left -= (MenuViewWidth / 10);
                    MenuView.Margin = margin;
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                }

                HideOverlay();
            }
            else
            {
                MenuView.Width = 0;
            }

            IsPanelOpen = false;

            MenuView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MenuOverlay.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }

        private async Task HideOverlay()
        {
            while (MenuOverlay.Opacity > 0)
            {
                MenuOverlay.Opacity -= 0.1;
                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }

        private async Task ShowOverlay()
        {
            while (MenuOverlay.Opacity < 0.7)
            {
                MenuOverlay.Opacity += 0.1;
                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }
        #endregion

    }
}

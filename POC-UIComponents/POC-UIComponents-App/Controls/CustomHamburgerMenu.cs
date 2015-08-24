﻿using Microsoft.Practices.Prism.Commands;
using POC.WP.CustomComponents;
using POC_UIComponents_App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using POC_UIComponents_App.Views;
using Windows.Foundation;
using Windows.UI.Xaml.Input;

namespace POC_UIComponents_App.Controls
{
    public class CustomHamburgerMenu : HamburgerMenu
    {

        private double _initialX;

        public CustomHamburgerMenu()
            : base()
        {
            this.DefaultStyleKey = typeof(CustomHamburgerMenu);
            this.SizeChanged += CustomHamburgerMenu_SizeChanged;
            this.ManipulationMode = ManipulationModes.TranslateX;
            this.ManipulationStarted += CustomHamburgerMenu_ManipulationStarted;
            this.ManipulationCompleted += CustomHamburgerMenu_ManipulationCompleted;
        }

        void CustomHamburgerMenu_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Position.X - _initialX <= Window.Current.Bounds.Width * 0.2)
            {
                IsLeftPaneOpen = false;
            }
        }

        void CustomHamburgerMenu_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            _initialX = e.Position.X;
        }

        void CustomHamburgerMenu_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LeftPaneWidth = e.NewSize.Width * LeftPaneWidthScreenRatio;
        }

        public double LeftPaneWidthScreenRatio
        {
            get { return (double)GetValue(LeftPaneWidthScreenRatioProperty); }
            set { SetValue(LeftPaneWidthScreenRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftPaneScre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftPaneWidthScreenRatioProperty =
            DependencyProperty.Register("LeftPaneWidthScreenRatio", typeof(double), typeof(CustomHamburgerMenu), new PropertyMetadata(0.0));


    }
}

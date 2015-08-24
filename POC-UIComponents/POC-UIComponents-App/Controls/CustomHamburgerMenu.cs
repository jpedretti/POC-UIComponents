using Microsoft.Practices.Prism.Commands;
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

namespace POC_UIComponents_App.Controls
{
    public class CustomHamburgerMenu : HamburgerMenu
    {
        public CustomHamburgerMenu()
            : base()
        {
            this.DefaultStyleKey = typeof(CustomHamburgerMenu);
            this.SizeChanged += CustomHamburgerMenu_SizeChanged;
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

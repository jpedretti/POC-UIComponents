using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace POC.WP.CustomComponents
{
    public class HamburgerMenu : ContentControl
    {
        private double _initialX;

        private ContentPresenter LeftPanePresenter { get; set; }
        private Rectangle MainPaneRectangle { get; set; }

        public HamburgerMenu()
        {
            this.DefaultStyleKey = typeof(HamburgerMenu);
        }

        public UIElement LeftPane
        {
            get { return (UIElement)GetValue(LeftPaneProperty); }
            set { SetValue(LeftPaneProperty, value); }
        }
        public static readonly DependencyProperty LeftPaneProperty = DependencyProperty.Register("LeftPane", typeof(UIElement), typeof(HamburgerMenu), new PropertyMetadata(null));

        public bool IsLeftPaneOpen
        {
            get { return (bool)GetValue(IsLeftPaneOpenProperty); }
            set { SetValue(IsLeftPaneOpenProperty, value); }
        }
        public static readonly DependencyProperty IsLeftPaneOpenProperty = DependencyProperty.Register("IsLeftPaneOpen", typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(false, new PropertyChangedCallback(OnIsLeftPaneOpenChanged)));

        public double LeftPaneWidth
        {
            get { return (double)GetValue(LeftPaneWidthProperty); }
            protected set { SetValue(LeftPaneWidthProperty, value); }
        }
        public static readonly DependencyProperty LeftPaneWidthProperty = DependencyProperty.Register("LeftPaneWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(300.0, new PropertyChangedCallback(OnLeftPaneWidthChanged)));
        
        protected override void OnApplyTemplate()
        {
            // Find the left pane in the control template and store a reference
            this.LeftPanePresenter = this.GetTemplateChild("leftPanePresenter") as ContentPresenter;
            this.MainPaneRectangle = this.GetTemplateChild("mainPaneRectangle") as Rectangle;

            if (this.MainPaneRectangle != null)
                this.MainPaneRectangle.Tapped += (sender, e) => { this.IsLeftPaneOpen = false; };

            // Ensure that the TranslateX on the RenderTransform of the left pane is set to the negative value of the left pa
            this.SetLeftPanePresenterX();

            base.OnApplyTemplate();
        }

        private static void OnIsLeftPaneOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var ctrl = sender as HamburgerMenu;
            if (ctrl != null)
            {
                if (ctrl.LeftPane != null)
                {
                    // Change to appropriate view state when the the IsLeftPaneOpen is toggled
                    var value = (bool)args.NewValue;
                    if (value)
                    {
                        VisualStateManager.GoToState(ctrl, "OpenLeftPane", true);

                        ctrl.ManipulationMode = ManipulationModes.TranslateX;
                        ctrl.ManipulationStarted += CustomHamburgerMenu_ManipulationStarted;
                        ctrl.ManipulationCompleted += CustomHamburgerMenu_ManipulationCompleted;
                    }
                    else
                    {
                        ctrl.ManipulationMode = ManipulationModes.System;
                        ctrl.ManipulationStarted -= CustomHamburgerMenu_ManipulationStarted;
                        ctrl.ManipulationCompleted -= CustomHamburgerMenu_ManipulationCompleted;
                        
                        VisualStateManager.GoToState(ctrl, "CloseLeftPane", true);
                    }
                }
            }
        }

        private void SetLeftPanePresenterX()
        {
            // Set the X position of the left pane content presenter to the negative of the pane so that it's off to the left of the screen when closed
            if (this.LeftPanePresenter != null)
            {
                this.LeftPanePresenter.RenderTransform = new CompositeTransform()
                {
                    TranslateX = -this.LeftPaneWidth
                };
            }
        }

        private static void OnLeftPaneWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            // Update the starting X position of the left pane if the width is updated
            var ctrl = sender as HamburgerMenu;
            if (ctrl != null && args.NewValue is double)
                ctrl.SetLeftPanePresenterX();
        }

        private static void CustomHamburgerMenu_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var ctrl = sender as HamburgerMenu;
            if (ctrl != null)
            {
                if (ctrl.LeftPane != null)
                {
                    if (isPanMovementLargerThen(e, ctrl, Window.Current.Bounds.Width * 0.2))
                    {
                        ctrl.IsLeftPaneOpen = false;
                    }
                }
            }
        }

        private static bool isPanMovementLargerThen(ManipulationCompletedRoutedEventArgs e, HamburgerMenu ctrl, double size)
        {
            return e.Position.X - ctrl._initialX <= -(size);
        }

        private static void CustomHamburgerMenu_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            var ctrl = sender as HamburgerMenu;
            if (ctrl != null)
            {
                if (ctrl.LeftPane != null)
                {
                    ctrl._initialX = e.Position.X;
                }
            }
        }
    }
}

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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace POC.WP.CustomComponents.ExpanderView
{
    public sealed partial class ExpanderViewControl : UserControl
    {
       
        #region Properties
        public static readonly DependencyProperty ContentPanelProperty =
            DependencyProperty.Register("ContentPanel", typeof(Grid), typeof(ExpanderViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(ExpanderViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(String), typeof(ExpanderViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsPanelOpenProperty =
            DependencyProperty.Register("IsPanelOpen", typeof(bool), typeof(ExpanderViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsOpenOnStartProperty = 
            DependencyProperty.Register("IsOpenOnStart", typeof(bool), typeof(ExpanderViewControl), new PropertyMetadata(true));

        public static readonly DependencyProperty ContentPanelHeightProperty =
            DependencyProperty.Register("ContentPanelHeight", typeof(double), typeof(ExpanderViewControl), new PropertyMetadata(250));

        
        public double ContentPanelHeight
        {
            get { return (double)GetValue(ContentPanelHeightProperty); }
            set { SetValue(ContentPanelHeightProperty, value); }
        }

        public string Title
        {
            get { return base.GetValue(TitleProperty) as String; }
            set { base.SetValue(TitleProperty, value); }
        }

        public ImageSource IconSource
        {
            get { return base.GetValue(IconSourceProperty) as ImageSource; }
            set { base.SetValue(IconSourceProperty, value); }
        }

        public Grid ContentPanel
        {
            get { return (Grid)GetValue(ContentPanelProperty); }
            set { SetValue(ContentPanelProperty, value); }
        }

        public bool IsPanelOpen
        {
            get { return (bool)GetValue(IsPanelOpenProperty); }
            set { SetValue(IsPanelOpenProperty, value); }
        }

        public bool IsOpenOnStart
        {
            get { return (bool)GetValue(IsOpenOnStartProperty); }
            set { SetValue(IsOpenOnStartProperty, value); }
        }

        #endregion

        public ExpanderViewControl()
        {
            this.InitializeComponent();
            this.btnOpenImage.Tapped += btnOpenImage_Tapped;
            //this.Loaded += ExpanderViewControl_Loaded;
            this.LayoutUpdated += ExpanderViewControl_LayoutUpdated;
        }

        void ExpanderViewControl_LayoutUpdated(object sender, object e)
        {
            Setup();
        }

        private bool _hasContentPanelLoaded = false;

        public void Setup()
        {
            if (!_hasContentPanelLoaded && ContentPanel != null)
            {
                _hasContentPanelLoaded = true;
                
                ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;

                if (IsOpenOnStart)
                {
                    IsPanelOpen = true;
                    //ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    ContentPanel.Height = ContentPanelHeight;
                }
                else
                {
                    IsPanelOpen = false;
                    //ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    ContentPanel.Height = 0;
                }
            }
        }

        #region Handlers
        void btnOpenImage_Tapped(object sender, RoutedEventArgs e)
        {
            ChangePanelState();
        }

        public async void ChangePanelState()
        {
            if (!IsPanelOpen)
            {
                // Animate opening the panel
                while (ContentPanel.Height < ContentPanelHeight)
                {
                    ContentPanel.Height += (ContentPanelHeight/10);
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                }

                //ContentPanel.Height = 250;

                //Storyboard s = new Storyboard();
                //DoubleAnimation ani = new DoubleAnimation();
                //ani.To = 250;
                //ani.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                //Storyboard.SetTarget(ani, ContentPanel);
                //Storyboard.SetTargetProperty(ani, "Height");
                //s.Children.Add(ani);
                //s.Begin();
                
                //ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;

                //VisualStateManager.GoToState(this, "OpenPane", true);

                IsPanelOpen = true;
                btnOpenImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/arrow_up.png", UriKind.Absolute));
            }
            else
            {
                // Animate opening the panel
                while (ContentPanel.Height > 0)
                {
                    ContentPanel.Height -= (ContentPanelHeight / 10);
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                }

                //ContentPanel.Height = 0;

                //Storyboard s = new Storyboard();
                //DoubleAnimation ani = new DoubleAnimation();
                //ani.To = 0;
                //ani.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
                //Storyboard.SetTarget(ani, ContentPanel);
                //Storyboard.SetTargetProperty(ani, "Height");
                //s.Children.Add(ani);
                //s.Begin();

                //ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                
                //VisualStateManager.GoToState(this, "ClosePane", true);

                IsPanelOpen = false;
                btnOpenImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/arrow_down.png", UriKind.Absolute));
            }

        }
        #endregion


    }
}

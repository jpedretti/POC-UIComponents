using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace POC.WP.CustomComponents.ExpanderView
{
    public class CustomExpanderView : ContentControl
    {
        #region Properties

        public static readonly DependencyProperty ViewContentsProperty =
            DependencyProperty.Register("ViewContents", typeof(UIElement), typeof(CustomExpanderView), new PropertyMetadata(null));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(CustomExpanderView), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(String), typeof(CustomExpanderView), new PropertyMetadata(null));

        public static readonly DependencyProperty IsPanelOpenProperty =
            DependencyProperty.Register("IsPanelOpen", typeof(bool), typeof(CustomExpanderView), new PropertyMetadata(null));

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

        public UIElement ViewContents
        {
            get { return (UIElement)GetValue(ViewContentsProperty); }
            set { SetValue(ViewContentsProperty, value); }
        }

        public bool IsPanelOpen
        {
            get { return (bool)GetValue(IsPanelOpenProperty); }
            set { SetValue(IsPanelOpenProperty, value); }
        }

        private ContentPresenter ViewContentsPresenter { get; set; }
        private Grid ContentsGrid { get; set; }
        private Image btnOpenImage { get; set; }

        #endregion

        #region Constructor
        public CustomExpanderView()
        {
            this.DefaultStyleKey = typeof(CustomExpanderView);
        }
        #endregion

        #region Overrides
        protected override void OnApplyTemplate()
        {
            this.ViewContentsPresenter = this.GetTemplateChild("viewContentsPresenter") as ContentPresenter;
            this.ContentsGrid = this.GetTemplateChild("gridContent") as Grid;
            this.btnOpenImage = this.GetTemplateChild("btnOpenImage") as Image;

            this.btnOpenImage.Tapped += btnOpenImage_Tapped;

            base.OnApplyTemplate();
        }
        #endregion

        #region Handlers
        void btnOpenImage_Tapped(object sender, RoutedEventArgs e)
        {
            ChangePanelState();
        }

        public void ChangePanelState()
        {
            if (ContentsGrid.Height == 0)
            {
                // Animate opening the panel
                //for (double i = 1; i < 50; i += 5)
                //{
                //    ContentsGrid.Height += i;
                //    await Task.Delay(TimeSpan.FromMilliseconds(15));
                //}
                //ContentsGrid.Height = double.NaN;

                VisualStateManager.GoToState(this, "OpenPane", true);

                IsPanelOpen = true;
                btnOpenImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/arrow_up.png", UriKind.Absolute));
            }
            else
            {

                //// Animate closing the panel
                //for (double i = ContentsGrid.ActualHeight / 2; i > 0; i -= 25)
                //{
                //    ContentsGrid.Height = i;
                //    await Task.Delay(TimeSpan.FromMilliseconds(15));
                //}
                //ContentsGrid.Height = 0;

                VisualStateManager.GoToState(this, "ClosePane", true);

                IsPanelOpen = false;
                btnOpenImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/arrow_down.png", UriKind.Absolute));
            }
        }
        #endregion

    }
}

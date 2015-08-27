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

namespace POC.WP.CustomComponents.ProfilePhoto
{
    public sealed partial class ProfilePhotoControl : UserControl
    {
        #region Properties
        public static readonly DependencyProperty SourcePhotoProperty =
            DependencyProperty.Register("SourcePhoto", typeof(ImageSource), typeof(ProfilePhotoControl), new PropertyMetadata(null, PhotoSourceChanged));

        public static readonly DependencyProperty InitialsProperty =
            DependencyProperty.Register("Initials", typeof(string), typeof(ProfilePhotoControl), new PropertyMetadata(string.Empty));

        public Visibility HasPhoto { get; set; }

        public ImageSource SourcePhoto
        {
            get { return (ImageSource)GetValue(SourcePhotoProperty); }
            set { SetValue(SourcePhotoProperty, value); }
        }

        public string Initials
        {
            get { return (string)GetValue(InitialsProperty); }
            set { SetValue(InitialsProperty, value); }
        }
        #endregion

        #region Constructor
        public ProfilePhotoControl()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private static void PhotoSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as ProfilePhotoControl;

            if (instance.SourcePhoto != null)
                instance.HasPhoto = Visibility.Visible;
            else
                instance.HasPhoto = Visibility.Collapsed;
        }
        #endregion

    }
}

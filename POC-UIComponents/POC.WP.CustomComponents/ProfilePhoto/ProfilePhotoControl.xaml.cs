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

namespace POC.WP.CustomComponents.ProfilePhoto
{
    public sealed partial class ProfilePhotoControl : UserControl
    {
        public ProfilePhotoControl()
        {
            this.InitializeComponent();
        }

        public ImageSource SourcePhoto
        {
            get { return (ImageSource)GetValue(SourcePhotoProperty); }
            set { SetValue(SourcePhotoProperty, value); }
        }

        public static readonly DependencyProperty SourcePhotoProperty =
            DependencyProperty.Register("SourcePhoto", typeof(ImageSource), typeof(ProfilePhotoControl), new PropertyMetadata(null));

    }
}

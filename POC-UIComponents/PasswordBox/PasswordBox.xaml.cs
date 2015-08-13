using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PasswordBox
{
    public sealed partial class PasswordBox : UserControl
    {
        public PasswordBox()
        {
            this.InitializeComponent();

            //caracteresViewBox.Margin = new Thickness(0, cardImage.Margin.Top * 1.2, 0, cardImage.Margin.Bottom * 1.2);
        }



        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FillColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Color), typeof(PasswordBox), new PropertyMetadata(0));



        public int CharacterCount
        {
            get { return (int)GetValue(CharacterCountProperty); }
            set { SetValue(CharacterCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.Register("CharacterCount", typeof(int), typeof(PasswordBox), new PropertyMetadata(0));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBox), new PropertyMetadata(0));



        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set
            {
                SetValue(ImageSourceProperty, value);
                image.Source = value;
            }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(PasswordBox), new PropertyMetadata(0));








    }
}

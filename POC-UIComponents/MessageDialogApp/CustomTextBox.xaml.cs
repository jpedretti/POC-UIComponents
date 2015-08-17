using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MessageDialogApp
{
    public sealed partial class CustomTextBox : UserControl
    {
        public CustomTextBox()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;

            SetBorderWidth();
        }

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set
            {
                SetValue(BorderWidthProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty BorderWidthProperty =
            DependencyProperty.Register("BorderWidth", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustomTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}

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

namespace POC.WP.CustomComponents
{
    public sealed partial class CustomTextBox : UserControl
    {
        private double _headerScaleFactor = 1.5;

        public CustomTextBox()
        {
            this.InitializeComponent();
            this.SizeChanged += CustomTextBox_SizeChanged;
            this.Tapped += CustomTextBox_Tapped;
        }



        public double Center
        {
            get { return (double)GetValue(CenterProperty); }
            private set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

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

        public double ReducedFontSize
        {
            get { return (double)GetValue(FontSizeProperty) / _headerScaleFactor; }
        }

        public double ReducedHeight
        {
            get { return (double)GetValue(HeightProperty) / _headerScaleFactor; }
        }




        public string PlaceHolderText
        {
            get { return (string)GetValue(PlaceHolderTextProperty); }
            set
            {
                SetValue(PlaceHolderTextProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for PlaceHolderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderTextProperty =
            DependencyProperty.Register("PlaceHolderText", typeof(string), typeof(CustomTextBox), new PropertyMetadata(""));

        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }



        public Duration PlaceHolderAnimationTime
        {
            get { return (Duration)GetValue(PlaceHolderAnimationTimeProperty); }
            set
            {
                SetValue(PlaceHolderAnimationTimeProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for AnimationTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderAnimationTimeProperty =
            DependencyProperty.Register("PlaceHolderAnimationTime", typeof(Duration), typeof(CustomTextBox), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5))));




        public Duration BorderAnimationTime
        {
            get { return (Duration)GetValue(BorderAnimationTimeProperty); }
            set { SetValue(BorderAnimationTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderAnimationTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderAnimationTimeProperty =
            DependencyProperty.Register("BorderAnimationTime", typeof(Duration), typeof(CustomTextBox), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.7))));




        public double SelectedBorderThickness
        {
            get { return (double)GetValue(SelectedBorderThicknessProperty); }
            set
            {
                SetValue(SelectedBorderThicknessProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for SelectedBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderThicknessProperty =
            DependencyProperty.Register("SelectedBorderThickness", typeof(double), typeof(CustomTextBox), new PropertyMetadata(3));




        public SolidColorBrush PlaceHolderForeground
        {
            get { return (SolidColorBrush)GetValue(PlaceHolderForegroundProperty); }
            set
            {
                SetValue(PlaceHolderForegroundProperty, value);
                RaisePropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for PlaceHolderForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderForegroundProperty =
            DependencyProperty.Register("PlaceHolderForeground", typeof(SolidColorBrush), typeof(CustomTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));





        private void SetBorderWidth(double width)
        {
            effectiveTextBox.Width = width;
            placeHolderTextBox.Width = width;
            bottonBorderFront.X2 = width;
            Center = width / 2.0;
        }

        private void effectiveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (effectiveTextBox.Text.Length > 0)
                VisualStateManager.GoToState(this, "HasValue", true);
            else
                VisualStateManager.GoToState(this, "NotHasValue", true);
        }

        void CustomTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorderWidth(e.NewSize.Width);
        }

        private void CustomTextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (effectiveTextBox.FocusState == Windows.UI.Xaml.FocusState.Unfocused)
                effectiveTextBox.Focus(FocusState.Programmatic);
        }
    }
}

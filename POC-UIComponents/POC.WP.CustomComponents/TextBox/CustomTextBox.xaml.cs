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
        public event PropertyChangedEventHandler PropertyChanged;

        public CustomTextBox()
        {
            this.InitializeComponent();
            this.SizeChanged += CustomTextBox_SizeChanged;
            this.Tapped += CustomTextBox_Tapped;
            this.FontFamily = new FontFamily("Arial");
        }

        #region Dependency Properties
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

        public SolidColorBrush PlaceHolderForeground
        {
            get { return (SolidColorBrush)GetValue(PlaceHolderForegroundProperty); }
            set
            {
                SetValue(PlaceHolderForegroundProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty PlaceHolderForegroundProperty =
            DependencyProperty.Register("PlaceHolderForeground", typeof(SolidColorBrush), typeof(CustomTextBox), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public double ReducedFontSize
        {
            get { return (double)GetValue(ReducedFontSizeProperty); }
            private set
            {
                SetValue(ReducedFontSizeProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty ReducedFontSizeProperty =
            DependencyProperty.Register("ReducedFontSize", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

        new public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            private set
            {
                SetValue(FontSizeProperty, value);
                RaisePropertyChanged();
            }
        }
        new public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            private set
            {
                SetValue(HeaderHeightProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

        public double Center
        {
            get { return (double)GetValue(CenterProperty); }
            private set
            {
                SetValue(CenterProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0));

        public double SelectedBorderThickness
        {
            get { return (double)GetValue(SelectedBorderThicknessProperty); }
            set
            {
                SetValue(SelectedBorderThicknessProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty SelectedBorderThicknessProperty =
            DependencyProperty.Register("SelectedBorderThickness", typeof(double), typeof(CustomTextBox), new PropertyMetadata(3));

        public string PlaceHolderText
        {
            get { return (string)GetValue(PlaceHolderTextProperty); }
            set
            {
                SetValue(PlaceHolderTextProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty PlaceHolderTextProperty =
            DependencyProperty.Register("PlaceHolderText", typeof(string), typeof(CustomTextBox), new PropertyMetadata(""));

        public Duration PlaceHolderAnimationTime
        {
            get { return (Duration)GetValue(PlaceHolderAnimationTimeProperty); }
            set
            {
                SetValue(PlaceHolderAnimationTimeProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty PlaceHolderAnimationTimeProperty =
            DependencyProperty.Register("PlaceHolderAnimationTime", typeof(Duration), typeof(CustomTextBox), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.3))));

        public Duration BorderAnimationTime
        {
            get { return (Duration)GetValue(BorderAnimationTimeProperty); }
            set
            {
                SetValue(BorderAnimationTimeProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty BorderAnimationTimeProperty =
            DependencyProperty.Register("BorderAnimationTime", typeof(Duration), typeof(CustomTextBox), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.7))));

        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            private set
            {
                SetValue(TextMarginProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(CustomTextBox), new PropertyMetadata(0));

        public InputScopeNameValue InputScope
        {
            get
            {
                return (GetValue(InputScopeProperty) as InputScope).Names.FirstOrDefault().NameValue;
            }
            set
            {
                var newInputScope = new InputScope { Names = { { new InputScopeName(value) } } };
                SetValue(InputScopeProperty, newInputScope);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register("InputScope", typeof(InputScope), typeof(CustomTextBox),
            new PropertyMetadata(new InputScope { Names = { { new InputScopeName(InputScopeNameValue.AlphanumericFullWidth) } } }));
        #endregion

        #region Private Methods
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

        private void CustomTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorderWidth(e.NewSize.Width);
            var componentSizeWithoutBottonBorder = (e.NewSize.Height - SelectedBorderThickness);
            var textBottonMarginValue = SelectedBorderThickness;
            TextMargin = new Thickness(0, 0, 0, textBottonMarginValue);

            FontSize = (componentSizeWithoutBottonBorder - textBottonMarginValue) * 2 / 3;
            ReducedFontSize = FontSize / 2;
            HeaderHeight = -(FontSize + textBottonMarginValue);
        }

        private void CustomTextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (effectiveTextBox.FocusState == Windows.UI.Xaml.FocusState.Unfocused)
                effectiveTextBox.Focus(FocusState.Programmatic);
        }

        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}

﻿using System;
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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PasswordBox
{
    public sealed partial class CustonPasswordBox : UserControl
    {
        public CustonPasswordBox()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;

            Window.Current.SizeChanged += Current_SizeChanged;

            SetBorderWidth();
        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetBorderWidth();
        }

        private static SolidColorBrush _notTypedDefaultColor = new SolidColorBrush(Colors.Gray);
        private static SolidColorBrush _typedDefaultColor = new SolidColorBrush(Colors.Orange);
        private static SolidColorBrush _backgroundDefaultColor = new SolidColorBrush(Colors.LightGray);
        private static InputScope _inputScope = new InputScope { Names = { { new InputScopeName(InputScopeNameValue.Number) } } };

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
            DependencyProperty.Register("BorderWidth", typeof(double), typeof(CustonPasswordBox), new PropertyMetadata(0));

        public SolidColorBrush TypedColor
        {
            get { return (SolidColorBrush)GetValue(TypedColorProperty); }
            set
            {
                SetValue(TypedColorProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty TypedColorProperty =
            DependencyProperty.Register("TypedColor", typeof(SolidColorBrush), typeof(CustonPasswordBox), new PropertyMetadata(_typedDefaultColor));

        public SolidColorBrush NotTypedColor
        {
            get { return (SolidColorBrush)GetValue(NotTypedColorProperty); }
            set
            {
                SetValue(NotTypedColorProperty, value);
                FillCreatedEllipses(value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty NotTypedColorProperty =
            DependencyProperty.Register("NotTypedColor", typeof(SolidColorBrush), typeof(CustonPasswordBox), new PropertyMetadata(_notTypedDefaultColor));

        public SolidColorBrush CustonBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(CustonBackgroundColorProperty); }
            set
            {
                SetValue(CustonBackgroundColorProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty CustonBackgroundColorProperty =
            DependencyProperty.Register("CustonBackgroundColor", typeof(SolidColorBrush), typeof(CustonPasswordBox), new PropertyMetadata(_backgroundDefaultColor));

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
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustonPasswordBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public int CharacterCount
        {
            get { return (int)GetValue(CharacterCountProperty); }
            set
            {
                SetValue(CharacterCountProperty, value);
                CharacterCountChanged(value);
            }
        }

        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.Register("CharacterCount", typeof(int), typeof(CustonPasswordBox), new PropertyMetadata(0));

        public string TypedPassword
        {
            get { return (string)GetValue(TypedPasswordProperty); }
            set
            {
                SetValue(TypedPasswordProperty, value);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty TypedPasswordProperty =
            DependencyProperty.Register("TypedPassword", typeof(string), typeof(CustonPasswordBox), new PropertyMetadata(null));


        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set
            {
                SetValue(ImageSourceProperty, value);
                image.Source = value;
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CustonPasswordBox), null);

        public InputScopeNameValue CustonInputScope
        {
            get
            {
                return (GetValue(CustonInputScopeProperty) as InputScope).Names.FirstOrDefault().NameValue;
            }
            set
            {
                var newInputScope = new InputScope { Names = { { new InputScopeName(value) } } };
                SetValue(CustonInputScopeProperty, newInputScope);
                RaisePropertyChanged();
            }
        }

        public static readonly DependencyProperty CustonInputScopeProperty =
            DependencyProperty.Register("CustonInputScope", typeof(InputScope), typeof(CustonPasswordBox), new PropertyMetadata(_inputScope));

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        private void FillEllipses(string password)
        {
            var passwordLength = password.Length;

            var ellipses = ellipsesStackPanel.Children.OfType<Ellipse>().ToList();

            int count = 0;
            foreach (var item in ellipses)
            {
                if (count < passwordLength)
                    item.Fill = TypedColor;
                else
                    item.Fill = NotTypedColor;

                count++;
            }

        }

        private void CharacterCountChanged(int value)
        {
            for (int i = 0; i < value; i++)
            {
                ellipsesStackPanel.Children.Insert(
                    i,
                    new Ellipse
                    {
                        Width = 50,
                        Height = 50,
                        Margin = new Thickness(5, 0, 5, 0),
                        Stretch = Stretch.UniformToFill,
                        Opacity = 1,
                        Fill = NotTypedColor
                    }
                );
            }
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            base.OnTapped(e);
            passwordContainer.Focus(FocusState.Programmatic);
        }

        private void FillCreatedEllipses(SolidColorBrush value)
        {
            var ellipses = ellipsesStackPanel.Children.OfType<Ellipse>().ToList();
            foreach (var item in ellipses)
            {
                item.Fill = value;
            }
        }

        private void passwordContainer_TextChanged(object sender, TextChangedEventArgs e)
        {
            var password = (sender as TextBox).Text;
            FillEllipses(password);
        }

        private void SetBorderWidth()
        {
            BorderWidth = Math.Max(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
        }


        private void IsFucused()
        {
            var hasFocus = true;
            if (hasFocus)
                VisualStateManager.GoToState(this, "Focused", true);
            else
                VisualStateManager.GoToState(this, "NotFocused", true);

        }
    }
}

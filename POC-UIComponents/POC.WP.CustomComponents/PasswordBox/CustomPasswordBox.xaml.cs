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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace POC.WP.CustomComponents
{
    public sealed partial class CustomPasswordBox : UserControl
    {
        private static SolidColorBrush _notTypedDefaultColor = new SolidColorBrush(Colors.Gray);
        private static SolidColorBrush _typedDefaultColor = new SolidColorBrush(Colors.Orange);
        private static SolidColorBrush _backgroundDefaultColor = new SolidColorBrush(Colors.LightGray);
        private static InputScope _inputScope = new InputScope { Names = { { new InputScopeName(InputScopeNameValue.Number) } } };

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomPasswordBox()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            this.SizeChanged += CustomPasswordBox_SizeChanged;
        }

        #region Dependency Properties
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
            DependencyProperty.Register("BorderWidth", typeof(double), typeof(CustomPasswordBox), new PropertyMetadata(0.0));

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
            DependencyProperty.Register("TypedColor", typeof(SolidColorBrush), typeof(CustomPasswordBox), new PropertyMetadata(_typedDefaultColor));

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
            DependencyProperty.Register("NotTypedColor", typeof(SolidColorBrush), typeof(CustomPasswordBox), new PropertyMetadata(_notTypedDefaultColor));

        public SolidColorBrush CustomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(CustomBackgroundColorProperty); }
            set
            {
                SetValue(CustomBackgroundColorProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty CustomBackgroundColorProperty =
            DependencyProperty.Register("CustomBackgroundColor", typeof(SolidColorBrush), typeof(CustomPasswordBox), new PropertyMetadata(_backgroundDefaultColor));

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
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustomPasswordBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

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
            DependencyProperty.Register("CharacterCount", typeof(int), typeof(CustomPasswordBox), new PropertyMetadata(4));

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
            DependencyProperty.Register("TypedPassword", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata(""));

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
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CustomPasswordBox), null);

        public InputScopeNameValue CustomInputScope
        {
            get
            {
                return (GetValue(CustomInputScopeProperty) as InputScope).Names.FirstOrDefault().NameValue;
            }
            set
            {
                var newInputScope = new InputScope { Names = { { new InputScopeName(value) } } };
                SetValue(CustomInputScopeProperty, newInputScope);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty CustomInputScopeProperty =
            DependencyProperty.Register("CustomInputScope", typeof(InputScope), typeof(CustomPasswordBox), new PropertyMetadata(_inputScope));
        #endregion

        #region Private Methods
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

        private void SetBorderWidth(double width)
        {
            BorderWidth = width;
        }

        private void CustomPasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorderWidth(e.NewSize.Width);
        }
        #endregion
    }
}

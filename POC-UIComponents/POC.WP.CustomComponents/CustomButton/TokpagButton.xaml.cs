using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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

namespace POC.WP.CustomComponents.CustomButton
{
    public sealed partial class TokpagButton : UserControl
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public TokpagButton()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        #region Dependency Properties

        public double Radius
        {
            get { return (double)GetValue(HeightProperty); }
            set
            {
                Width = value;
                Height = value;
                BorderRadius = new CornerRadius(value);
                ImageSize = value * 0.50;
                RaisePropertyChanged();
            }
        }

        //Utilizado para sobrescrever a propriedade Width padrão, impedindo sua utilização pelo usuario ao utilizar o TokpagButton
        new public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            private set
            {
                SetValue(WidthProperty, value);
                RaisePropertyChanged();
            }
        }
        //Utilizado para sobrescrever a propriedade Heigth padrão, impedindo sua utilização pelo usuario ao utilizar o TokpagButton
        new public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            private set
            {
                SetValue(HeightProperty, value);
                RaisePropertyChanged();
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set
            {
                SetValue(CommandProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(TokpagButton), new PropertyMetadata(null));

        public SolidColorBrush DisabledBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(DisabledBackgroundColorProperty); }
            set
            {
                SetValue(DisabledBackgroundColorProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty DisabledBackgroundColorProperty =
            DependencyProperty.Register("DisabledBackgroundColor", typeof(SolidColorBrush), typeof(TokpagButton), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadiusProperty); }
            private set
            {
                SetValue(BorderRadiusProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(TokpagButton), new PropertyMetadata(new CornerRadius(0)));

        public double ImageSize
        {
            get { return (double)GetValue(ImageSizeProperty); }
            private set
            {
                SetValue(ImageSizeProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof(double), typeof(TokpagButton), new PropertyMetadata(0.0));


        #endregion

        #region Private Methods
        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

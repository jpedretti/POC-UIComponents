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
    public sealed partial class RoundBorderButton : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RoundBorderButton()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        #region Dependency Properties
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
            DependencyProperty.Register("Command", typeof(ICommand), typeof(RoundBorderButton), new PropertyMetadata(null));

        public SolidColorBrush PressedForegroundColor
        {
            get { return (SolidColorBrush)GetValue(PressedForegroundColorProperty); }
            set
            {
                SetValue(PressedForegroundColorProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty PressedForegroundColorProperty =
            DependencyProperty.Register("PressedForegroundColor", typeof(SolidColorBrush), typeof(RoundBorderButton), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public SolidColorBrush PressedBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(PressedBackgroundColorProperty); }
            set
            {
                SetValue(PressedBackgroundColorProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty PressedBackgroundColorProperty =
            DependencyProperty.Register("PressedBackgroundColor", typeof(SolidColorBrush), typeof(RoundBorderButton), new PropertyMetadata(new SolidColorBrush(Colors.DarkOrange)));

        public SolidColorBrush DisabledForegroundColor
        {
            get { return (SolidColorBrush)GetValue(DisabledForegroundColorProperty); }
            set
            {
                SetValue(DisabledForegroundColorProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty DisabledForegroundColorProperty =
            DependencyProperty.Register("DisabledForegroundColor", typeof(SolidColorBrush), typeof(RoundBorderButton), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public SolidColorBrush DisabledBorderBrush
        {
            get { return (SolidColorBrush)GetValue(DisabledBorderBrushProperty); }
            set
            {
                SetValue(DisabledBorderBrushProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.Register("DisabledBorderBrush", typeof(SolidColorBrush), typeof(RoundBorderButton), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

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
            DependencyProperty.Register("DisabledBackgroundColor", typeof(SolidColorBrush), typeof(RoundBorderButton), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set
            {
                SetValue(ButtonContentProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(RoundBorderButton), new PropertyMetadata(null));

        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadiusProperty); }
            set
            {
                SetValue(BorderRadiusProperty, value);
                RaisePropertyChanged();
            }
        }
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(RoundBorderButton), new PropertyMetadata(new CornerRadius(6)));
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

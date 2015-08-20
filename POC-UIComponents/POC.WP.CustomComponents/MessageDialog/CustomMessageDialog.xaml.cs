using MessageDialog.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace POC.WP.CustomComponents
{
    public sealed partial class CustomMessageDialog : ContentDialog
    {
        #region Dependency Properties

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(CustomMessageDialog), new PropertyMetadata(null));



        public SolidColorBrush MessageForeground
        {
            get { return (SolidColorBrush)GetValue(MessageForegroundProperty); }
            set { SetValue(MessageForegroundProperty, value); }
        }

        public static readonly DependencyProperty MessageForegroundProperty =
            DependencyProperty.Register("MessageForeground", typeof(SolidColorBrush), typeof(CustomMessageDialog), new PropertyMetadata(
                new SolidColorBrush(Color.FromArgb(255, 140, 140, 140))
            ));

        public SolidColorBrush TitleForeground
        {
            get { return (SolidColorBrush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(SolidColorBrush), typeof(CustomMessageDialog), new PropertyMetadata(
                new SolidColorBrush(Color.FromArgb(255, 244, 121, 32))
            ));



        public SolidColorBrush ButtonForeground
        {
            get { return (SolidColorBrush)GetValue(ButtonForegroundProperty); }
            set { SetValue(ButtonForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonForegroundProperty =
            DependencyProperty.Register("ButtonForeground", typeof(SolidColorBrush), typeof(CustomMessageDialog), new PropertyMetadata(
                new SolidColorBrush(Color.FromArgb(255, 244, 121, 32))
            ));



        public SolidColorBrush ButtonBorderBrush
        {
            get { return (SolidColorBrush)GetValue(ButtonBorderBrushProperty); }
            set { SetValue(ButtonBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonBorderBrushProperty =
            DependencyProperty.Register("ButtonBorderBrush", typeof(SolidColorBrush), typeof(CustomMessageDialog), new PropertyMetadata(
                new SolidColorBrush(Color.FromArgb(255, 244, 121, 32))
        ));



        public string Button1Text
        {
            get { return (string)GetValue(Button1TextProperty); }
            set { SetButton1Text(value); }
        }

        private static readonly DependencyProperty Button1TextProperty =
            DependencyProperty.Register("Button1Text", typeof(string), typeof(CustomMessageDialog), new PropertyMetadata(null));

        public string Button2Text
        {
            get { return (string)GetValue(Button2TextProperty); }
            set { SetButton2Text(value); }
        }

        private static readonly DependencyProperty Button2TextProperty =
            DependencyProperty.Register("Button2Text", typeof(string), typeof(CustomMessageDialog), new PropertyMetadata(null));

        public ICommand Button1Command
        {
            get { return (ICommand)GetValue(Button1CommandProperty); }
            set { SetValue(Button1CommandProperty, value); }
        }

        private static readonly DependencyProperty Button1CommandProperty =
            DependencyProperty.Register("Button1Command", typeof(ICommand), typeof(CustomMessageDialog), new PropertyMetadata(null));

        public ICommand Button2Command
        {
            get { return (ICommand)GetValue(Button2CommandProperty); }
            set { SetValue(Button2CommandProperty, value); }
        }

        private static readonly DependencyProperty Button2CommandProperty =
            DependencyProperty.Register("Button2Command", typeof(ICommand), typeof(CustomMessageDialog), new PropertyMetadata(null));

        private static readonly DependencyProperty Button1EnabledProperty =
            DependencyProperty.Register("Button1Enabled", typeof(Visibility), typeof(CustomMessageDialog), new PropertyMetadata(Visibility.Collapsed));

        private Visibility Button2Enabled
        {
            get { return (Visibility)GetValue(Button2EnabledProperty); }
            set { SetValue(Button2EnabledProperty, value); }
        }

        private static readonly DependencyProperty Button2EnabledProperty =
            DependencyProperty.Register("Button2Enabled", typeof(Visibility), typeof(CustomMessageDialog), new PropertyMetadata(Visibility.Collapsed));

        private object _button1Params;

        public object Button1Params
        {
            get { return _button1Params; }
            set { _button1Params = value; }
        }

        private object _button2Params;

        public object Button2Params
        {
            get { return _button2Params; }
            set { _button2Params = value; }
        }

        private Visibility Button1Enabled
        {
            get { return (Visibility)GetValue(Button1EnabledProperty); }
            set { SetValue(Button1EnabledProperty, value); }
        }

        #endregion

        public CustomMessageDialog()
        {
            this.InitializeComponent();
            FontFamily = new FontFamily("Arial");
        }


        private void SetButton2Text(string value)
        {
            SetValue(Button2TextProperty, value);
            Button2Enabled = !string.IsNullOrEmpty(Button2Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetButton1Text(string value)
        {
            SetValue(Button1TextProperty, value);
            Button1Enabled = !string.IsNullOrEmpty(Button1Text) ? Visibility.Visible : Visibility.Collapsed;
        }
        public ICommand Button1Clicked
        {
            get
            {
                return new RelayCommand(
                    (e) =>
                    {
                        if (Button1Command != null && Button1Command.CanExecute(Button1Params))
                        {
                            Button1Command.Execute(Button1Params);
                        }
                        this.Hide();
                    },
                    (e) => { return true; }
                    );

            }
        }

        public ICommand Button2Clicked
        {
            get
            {
                return new RelayCommand(
                    (e) =>
                    {
                        if (Button2Command != null && Button2Command.CanExecute(SecondaryButtonCommandParameter))
                        {
                            Button2Command.Execute(SecondaryButtonCommandParameter);
                        }
                        this.Hide();
                    },
                    (e) => { return true; }
                    );

            }
        }
    }
}

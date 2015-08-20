using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using POC.WP.CustomComponents;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;
using System.Diagnostics;

namespace POC_UIComponents_App.ViewModels
{
    class MessageDialogPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        private DelegateCommand _button1Command;
        public DelegateCommand Button1Command
        {
            get
            {
                if (_button1Command == null)
                {
                    _button1Command = new DelegateCommand
                        (
                            async () => { await Button1CommandAction(); },
                            () => { return true; }
                        );
                }
                return _button1Command;
            }
        }

        private DelegateCommand _button2Command;
        public DelegateCommand Button2Command
        {
            get
            {
                if (_button2Command == null)
                {
                    _button2Command = new DelegateCommand
                        (
                            async () => { await Button2CommandAction(); },
                            () => { return true; }
                        );
                }
                return _button2Command;
            }
        }

        public MessageDialogPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private async Task Button1CommandAction()
        {
            var dialog = new CustomMessageDialog
            {
                Title = this.Title,
                Message = this.Message,
                Button1Text = "Click Me Arial",
                FontFamily = new FontFamily("Arial"),
                FontWeight = FontWeights.Black,
                Button1Command = new DelegateCommand<string>
                (
                    (s) => { Debug.WriteLine(s); },
                    (s) => { return true; }
                ),
                Button1Params = "HELLO WORLD"
            };

            await dialog.ShowAsync();
        }

        private async Task Button2CommandAction()
        {
            var dialog = new CustomMessageDialog
            {
                Title = this.Title,
                Message = this.Message,
                Button1Text = "Click Me",
                Button2Text = "Or Me!",
                FontFamily = new FontFamily("Arial")
            };

            await dialog.ShowAsync();
        }
    }
}

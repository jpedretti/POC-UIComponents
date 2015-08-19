using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace POC_UIComponents_App.ViewModels
{
    class MainPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        private DelegateCommand _goToTextbox;
        public DelegateCommand GoToTextBox
        {
            get
            {
                if (_goToTextbox == null)
                {
                    _goToTextbox = new DelegateCommand
                        (
                            () => { GoToTextBoxAction(); },
                            () => { return true; }
                        );
                }
                return _goToTextbox;
            }
        }

        private DelegateCommand _goToPasswordbox;
        public DelegateCommand GoToPasswordbox
        {
            get
            {
                if (_goToPasswordbox == null)
                {
                    _goToPasswordbox = new DelegateCommand
                        (
                            () => { GoToPasswordboxAction(); },
                            () => { return true; }
                        );
                }
                return _goToPasswordbox;
            }
        }



        private DelegateCommand _goToMessageDialog;
        public DelegateCommand GoToMessageDialog
        {
            get
            {
                if (_goToMessageDialog == null)
                {
                    _goToMessageDialog = new DelegateCommand
                        (
                            () => { GoToMessageDialogAction(); },
                            () => { return true; }
                        );
                }
                return _goToMessageDialog;
            }
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void GoToTextBoxAction()
        {
            _navigationService.Navigate("Textbox", null);
        }

        private void GoToPasswordboxAction()
        {
            _navigationService.Navigate("PasswordBox", null);

        }

        private void GoToMessageDialogAction()
        {
            _navigationService.Navigate("MessageDialog", null);
        }
    }
}

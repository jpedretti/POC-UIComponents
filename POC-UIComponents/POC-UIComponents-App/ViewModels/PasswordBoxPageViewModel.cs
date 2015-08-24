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
    public class PasswordBoxPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public PasswordBoxPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private string _password = default(string);

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private string _password2 = default(string);

        public string Password2
        {
            get
            {
                return _password2;
            }
            set
            {
                SetProperty(ref _password2, value);
            }
        }
    }
}

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
    class TextboxPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public TextboxPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private string _myText = string.Empty;

        public string Nome
        {
            get
            {
                return _myText;
            }
            set
            {
                SetProperty(ref _myText, value);
            }
        }
    }
}

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
    class TextboxViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public TextboxViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}

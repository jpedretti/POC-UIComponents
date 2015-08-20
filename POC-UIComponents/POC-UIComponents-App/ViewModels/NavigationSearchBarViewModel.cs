using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System.Diagnostics;

namespace POC_UIComponents_App.ViewModels
{
    class NavigationSearchBarPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public NavigationSearchBarPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private DelegateCommand _myCommand;
        public DelegateCommand MyCommand
        {
            get
            {
                if (_myCommand == null)
                {
                    _myCommand = new DelegateCommand
                        (
                            () => { Debug.WriteLine("HELLO WORLD FROM NavigationSearchBar"); },
                            () => { return true; }
                        );
                }
                return _myCommand;
            }
        }
    }
}

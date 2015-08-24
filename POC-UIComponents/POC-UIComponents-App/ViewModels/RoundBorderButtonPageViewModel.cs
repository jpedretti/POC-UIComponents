using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace POC_UIComponents_App.ViewModels
{
    public class RoundBorderButtonPageViewModel : ViewModel
    {
        DelegateCommand execute = null;
        public DelegateCommand Execute
        {
            get
            {
                if (execute != null)
                    return execute;
                execute = new DelegateCommand
                (
                    async () => { await new Windows.UI.Popups.MessageDialog("Sucesso", "Sucesso").ShowAsync(); },
                    () => { return true; }
                );
                this.PropertyChanged += (s, e) => execute.RaiseCanExecuteChanged();
                return execute;
            }
        }

        DelegateCommand execute1 = null;
        public DelegateCommand Execute1
        {
            get
            {
                if (execute1 != null)
                    return execute1;
                execute1 = new DelegateCommand
                (
                    async () => { await new Windows.UI.Popups.MessageDialog("Sucesso", "Sucesso").ShowAsync(); },
                    () => { return true; }
                );
                this.PropertyChanged += (s, e) => execute1.RaiseCanExecuteChanged();
                return execute1;
            }
        }

        DelegateCommand execute2 = null;
        public DelegateCommand Execute2
        {
            get
            {
                if (execute2 != null)
                    return execute2;
                execute2 = new DelegateCommand
                (
                    async () => { await new Windows.UI.Popups.MessageDialog("Sucesso", "Sucesso").ShowAsync(); },
                    () => { return true; }
                );
                this.PropertyChanged += (s, e) => execute2.RaiseCanExecuteChanged();
                return execute2;
            }
        }
        DelegateCommand execute3 = null;
        public DelegateCommand Execute3
        {
            get
            {
                if (execute3 != null)
                    return execute3;
                execute3 = new DelegateCommand
                (
                    async () => { await new Windows.UI.Popups.MessageDialog("Sucesso", "Sucesso").ShowAsync(); },
                    () => { return false; }
                );
                this.PropertyChanged += (s, e) => execute3.RaiseCanExecuteChanged();
                return execute3;
            }
        }
    }
}

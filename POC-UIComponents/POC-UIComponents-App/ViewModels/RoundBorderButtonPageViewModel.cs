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
    }
}

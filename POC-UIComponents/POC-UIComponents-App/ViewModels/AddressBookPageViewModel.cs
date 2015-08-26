using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using POC_UIComponents_App.Model;
using POC_UIComponents_App.Infrastructure;
using System.Globalization;
using Windows.UI.Xaml.Data;
using System.Collections.ObjectModel;

namespace POC_UIComponents_App.ViewModels
{
    public class AddressBookPageViewModel : ViewModel
    {
        private INavigationService _navigationService;

        public object CurrentSelectedItem { get; set; }

        private List<AlphaKeyGroup<AddressBookEntryModel>> _addressGroups = new List<AlphaKeyGroup<AddressBookEntryModel>>();

        public List<AlphaKeyGroup<AddressBookEntryModel>> AddressGroups
        {
            get
            {
                return _addressGroups;
            }
            set
            {
                SetProperty(ref _addressGroups, value);
            }
        }



        public AddressBookPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }


        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            GenerateDummyData();

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }

        private void GenerateDummyData()
        {
            List<AddressBookEntryModel> source = new List<AddressBookEntryModel>();
            source.Add(new AddressBookEntryModel("Ringo Pumpkin", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", ""));
            source.Add(new AddressBookEntryModel("Vincent Vega", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "", true));
            source.Add(new AddressBookEntryModel("Yolanda Honey Bunny", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", ""));
            source.Add(new AddressBookEntryModel("Jules Winnfield", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", ""));
            source.Add(new AddressBookEntryModel("Marvin", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "", true));
            source.Add(new AddressBookEntryModel("Brett", null, null, "99999-0011", ""));
            source.Add(new AddressBookEntryModel("Roger", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "", true));
            source.Add(new AddressBookEntryModel("Butch Coolidge", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", ""));
            source.Add(new AddressBookEntryModel("Marsellus Wallace", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Paul English Bob", null, null, "99999-0011", "/Assets/contact_photo.jpg"));
            source.Add(new AddressBookEntryModel("Trudi", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Jody", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Lance", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Mia Wallace", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg"));
            source.Add(new AddressBookEntryModel("Bob Bogle", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", "/Assets/contact_photo.jpg"));
            source.Add(new AddressBookEntryModel("Quentin", "Citi", "Ag. 1234 | CC. 1231231", "99999-0011", null, true));
            source.Add(new AddressBookEntryModel("Jeferson", null, null, "99999-0011", "/Assets/contact_photo.jpg", true));

            AddressGroups = AlphaKeyGroup<AddressBookEntryModel>.CreateGroups(source, CultureInfo.CurrentUICulture, s => s.FullName, true);
        }





    }
}

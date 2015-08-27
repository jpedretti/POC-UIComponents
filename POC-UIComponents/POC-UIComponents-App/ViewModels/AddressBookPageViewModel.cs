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
            source.Add(new AddressBookEntryModel("Ringo Pumpkin", "Citi", "Ag. 1234 | CC. 1", "1-0011", ""));
            source.Add(new AddressBookEntryModel("Vincent Vega", "Citi", "Ag. 1234 | CC. 2", "2-0011", "", true));
            source.Add(new AddressBookEntryModel("Yolanda Honey Bunny", "Citi", "Ag. 1234 | CC. 3", "3-0011", "/Assets/contact_photo_2.jpg"));
            source.Add(new AddressBookEntryModel("Jules Winnfield", "Citi", "Ag. 1234 | CC. 4", "4-0011", ""));
            source.Add(new AddressBookEntryModel("Marvin", "Citi", "Ag. 1234 | CC. 5", "5-0011", "", true));
            source.Add(new AddressBookEntryModel("Brett", null, null, "6-0011", "/Assets/contact_photo_2.jpg"));
            source.Add(new AddressBookEntryModel("Roger", "Citi", "Ag. 1234 | CC. 6", "7-0011", "", true));
            source.Add(new AddressBookEntryModel("Butch Coolidge", "Citi", "Ag. 1234 | CC. 7", "8-0011", ""));
            source.Add(new AddressBookEntryModel("Marsellus Wallace", "Citi", "Ag. 1234 | CC. 8", "9-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Paul English Bob", null, null, "10-0011", "/Assets/contact_photo.jpg"));
            source.Add(new AddressBookEntryModel("Trudi", "Citi", "Ag. 1234 | CC. 9", "11-0011", "/Assets/contact_photo_2.jpg", true));
            source.Add(new AddressBookEntryModel("Jody", "Citi", "Ag. 1234 | CC. 11", "12-0011", "/Assets/contact_photo_2.jpg", true));
            source.Add(new AddressBookEntryModel("Lance", "Citi", "Ag. 1234 | CC. 12", "13-0011", "/Assets/contact_photo.jpg", true));
            source.Add(new AddressBookEntryModel("Mia Wallace", "Citi", "Ag. 1234 | CC. 13", "14-0011", "/Assets/contact_photo_2.jpg"));
            source.Add(new AddressBookEntryModel("Bob Bogle", "Citi", "Ag. 1234 | CC. 14", "15-0011", "/Assets/contact_photo.jpg"));
            source.Add(new AddressBookEntryModel("Quentin", "Citi", "Ag. 1234 | CC. 15", "16-0011", null, true));
            source.Add(new AddressBookEntryModel("Jeferson", null, null, "17-0011", "/Assets/contact_photo.jpg", true));

            AddressGroups = AlphaKeyGroup<AddressBookEntryModel>.CreateGroups(source, CultureInfo.CurrentUICulture, s => s.FullName, true);
        }





    }
}

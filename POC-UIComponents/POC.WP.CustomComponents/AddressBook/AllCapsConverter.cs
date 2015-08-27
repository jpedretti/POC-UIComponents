using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace POC.WP.CustomComponents.AddressBook
{
    public class AllCapsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(string))
                return ((string)value).ToUpper();

            return value;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.GetType() == typeof(string))
                return ((string)value).ToUpper();

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.GetType() == typeof(string))
                return ((string)value).ToLower();

            return value;
        }
    }
}

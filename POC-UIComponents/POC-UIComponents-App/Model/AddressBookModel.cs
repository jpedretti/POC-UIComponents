using POC_UIComponents_App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Globalization.Collation;

namespace POC_UIComponents_App.Model
{
    public class AlphaKeyGroup<T> : List<T>
    {
        /// <summary>
        /// The delegate that is used to get the key information.
        /// </summary>
        /// <param name="item">An object of type T</param>
        /// <returns>The key value to use for this object</returns>
        public delegate string GetKeyDelegate(T item);

        /// <summary>
        /// The Key of this group.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="key">The key for this group.</param>
        public AlphaKeyGroup(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Create a list of AlphaGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="slg">The </param>
        /// <returns>Theitems source for a LongListSelector</returns>
        private static List<AlphaKeyGroup<T>> CreateGroups(CharacterGroupings slg)
        {
            List<AlphaKeyGroup<T>> list = new List<AlphaKeyGroup<T>>();

            foreach (CharacterGrouping key in slg)
            {
                if (string.IsNullOrWhiteSpace(key.Label) == false)
                    list.Add(new AlphaKeyGroup<T>(key.Label));
            }

            return list;
        }

        /// <summary>
        /// Create a list of AlphaGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="items">The items to place in the groups.</param>
        /// <param name="ci">The CultureInfo to group and sort by.</param>
        /// <param name="getKey">A delegate to get the key from an item.</param>
        /// <param name="sort">Will sort the data if true.</param>
        /// <returns>An items source for a LongListSelector</returns>
        public static List<AlphaKeyGroup<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey, bool sort)
        {
            CharacterGroupings slg = new CharacterGroupings();
            List<AlphaKeyGroup<T>> list = CreateGroups(slg);

            foreach (T item in items)
            {
                string index = "";
                index = slg.Lookup(getKey(item));
                if (string.IsNullOrEmpty(index) == false)
                {
                    list.Find(a => a.Key == index).Add(item);
                }
            }

            if (sort)
            {
                foreach (AlphaKeyGroup<T> group in list)
                {
                    group.Sort((c0, c1) => { return ci.CompareInfo.Compare(getKey(c0), getKey(c1)); });
                }
            }

            return list;
        }

    }

    public class AddressBookEntryModel
    {
        public string FullName { get; set; }

        public string BankName { get; set; }

        public string Account { get; set; }

        public string Phone { get; set; }

        public string PhotoPath { get; set; }

        public string Initials { get; set; }

        public Windows.UI.Xaml.Visibility Flag { get; set; }

        public AddressBookEntryModel(string fullName, string bankName, string account, string phone, string photoPath, bool flag = false)
        {
            this.FullName = fullName;
            this.BankName = bankName;
            this.Account = account;
            this.Phone = phone;
            this.PhotoPath = photoPath;
            this.Initials = ExtractNameInitials(fullName);
            this.Flag = flag ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FullName, Account, Phone);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            var q = (obj as AddressBookEntryModel);
            
            if (q.FullName != this.FullName && q.Account != this.Account && q.Phone != this.Phone)
                return false;
            
            return true;
        }

        private string ExtractNameInitials(string fullname)
        {
            Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");

            string result = initials.Replace(fullname, "$1");

            if (!string.IsNullOrEmpty(result))
                if (result.Length > 1)
                    result = result.First().ToString() + result.Last().ToString();
                else
                    result = result.First().ToString();

            return result;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Utils
    {
        public Boolean checkForDuplicates(ObservableCollection<ListItem> collection, ListItem newItem)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection.ElementAt(i).Name == newItem.Name) return false;
                if (collection.ElementAt(i).Path == newItem.Path) return false;
            }
            return true;
        }
    }
}

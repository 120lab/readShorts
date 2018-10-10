using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace readShorts.ManageBackoffice
{
    public static class Switcher
    {
        public class SwitcherParams
        {
            public Int64 UserRecordKey { get; set; }
            public Int64 ShortRecordKey { get; set; }
        }

        public static SwitcherParams param = new SwitcherParams();
        public static MainWindow pageSwitcher;
        public static readShorts_DB_DEVEntities DBContext = new readShorts_DB_DEVEntities();
        public static void Switch(UserControl newPage)
        {
            pageSwitcher.pageContent.Content = newPage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace readShorts.ManageBackoffice
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : UserControl
    {
        public Page2()
        {
            InitializeComponent();
            if (Switcher.param.UserRecordKey != 0)
            {
                Int64? val = Switcher.DBContext.Short.FirstOrDefault(x => x.WriterUserKey == Switcher.param.UserRecordKey).RecordKey;
                if (val.HasValue)
                {
                    Switcher.param.ShortRecordKey = val.Value;
                    UserAccountPanel.DataContext = Switcher.DBContext.Short.FirstOrDefault(x => x.WriterUserKey == Switcher.param.UserRecordKey);
                }
                      
            }
        }
    }
}

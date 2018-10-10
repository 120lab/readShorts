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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : UserControl
    {
        public Page4()
        {
            InitializeComponent();
            InitGrid();
        }
        private void InitGrid()
        {
            GridDataChannels.ItemsSource = Switcher.DBContext.ShortChannel.Where(x=>x.IsRowDeleted == false && x.ShortKey == Switcher.param.ShortRecordKey).ToList();
        }
    }
}

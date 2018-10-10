using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Logica di interazione per Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        public Page1()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            var users = (from u in Switcher.DBContext.UserAccount
                         select new
                         {
                             u.RecordKey,
                             u.UserName,
                             u.FirstName,
                             u.LastName,
                             u.LUSubscriptionTypeKey,
                             u.LUSysInterfacelanguageKey,
                             u.ClientIP                             
                         }).ToList();
            UserGrid.ItemsSource = users;            
        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = UserGrid.SelectedItem;
            Switcher.param.UserRecordKey = item.RecordKey;
        }
    }
}

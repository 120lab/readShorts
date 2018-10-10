using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : UserControl
    {
        public Page3()
        {
            InitializeComponent();
            InitGrid();
        }
        private void InitGrid()
        {
            GetUsersList();
        }

        private void UsersGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            UserAccount ua = (e.Row.DataContext as UserAccount);
            Switcher.DBContext.UserAccount.Add(ua);
            SaveChanges();
            GetUsersList();
        }

        private void GetUsersList()
        {
            UsersGrid.ItemsSource = Switcher.DBContext.UserAccount.ToList();
        }

        private void SaveChanges()
        {
            try
            {
                Switcher.DBContext.SaveChanges();
                MessageBox.Show("Save Done", "Save Done");
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                string errorMessages = string.Empty;
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in validationResult.ValidationErrors)
                    {
                        errorMessages += validationError.ErrorMessage;
                    }
                }

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                MessageBox.Show(exceptionMessage, "Save Validation fail - Check for invalid data "); 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Save Validation fail - Check for invalid data ");
            }
        }
    }
}

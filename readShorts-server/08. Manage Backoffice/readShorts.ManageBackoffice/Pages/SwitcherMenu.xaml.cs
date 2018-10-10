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
    /// Logica di interazione per SwitcherMenu.xaml
    /// </summary>
    public partial class SwitcherMenu : UserControl
    {
        public SwitcherMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (sender as Button);
            switch (btn.Content.ToString())
            {
                case "Users":                    
                    Switcher.Switch(new Page3());
                    break;
                case "Choose user":
                    Switcher.Switch(new Page1());
                    break;
                case "Shorts":
                    Switcher.Switch(new Page2());
                    break;
                case "Channels":
                    Switcher.Switch(new Page4());
                    break;
                case "Exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }            
        }
    }
}

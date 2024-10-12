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

namespace DemoProb.Pages
{
    /// <summary>
    /// Логика взаимодействия для CommonPage.xaml
    /// </summary>
    public partial class CommonPage : Page
    {
        public CommonPage()
        {
            InitializeComponent();
            UpdatePage();
        }

        public void UpdatePage()
        {
            ServiceWpar.Children.Clear();
            foreach (var item in App.db.Service)
            {
                ServiceWpar.Children.Add(new Controls.CommonUserControl(item));
            }
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

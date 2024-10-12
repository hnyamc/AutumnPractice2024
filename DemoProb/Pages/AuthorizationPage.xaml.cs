using DemoProb.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Client> client { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            string name = LastNameTB.Text.Trim();
            string firstName = FirstNameTB.Text.Trim();

            client = new List<Client>(App.db.Client.ToList());
            Client currentClient = client.FirstOrDefault(x => x.FirstName == name && x.LastName == firstName);
            if (currentClient != null)
            {
                LastNameTB.Text = "";
                FirstNameTB.Text = "";
                NavigationService.Navigate(new CommonPage());
            }
            else if (LastNameTB.Text == "0000" && FirstNameTB.Text == "0000")
                NavigationService.Navigate(new EnterPage());
            else
            {
                MessageBox.Show("Мы не нашли ваши данные в системе, попробуйте зайти снова");
                LastNameTB.Text = "";
                FirstNameTB.Text = "";
            }
        }
    }
}

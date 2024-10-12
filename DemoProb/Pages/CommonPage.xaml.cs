using DemoProb.Controls;
using DemoProb.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private List<Service> service = new List<Service>();
        public CommonPage()
        {
            InitializeComponent();
            CostMaxMinCB.SelectedIndex = 1;
            DiscountCB.SelectedIndex = 0;
            UpdatePage();
        }

        public void UpdatePage()
        {
            ServiceWpar.Children.Clear();
            service = App.db.Service.ToList();
            SortingCost();
            SortingSearch();
            SortingDiscount();
            foreach (var item in service)
            {
                ServiceWpar.Children.Add(new CommonUserControl(item));
            }
        }

        public void SortingCost()
        {
            switch (CostMaxMinCB.SelectedIndex)
            {
                case 0: // Сортировка по возрастанию
                    service = service
                        .OrderBy(x => ((double?)x.Cost) - ((double?)x.Cost) * (x.Discount ?? 0) / 100)
                        .ToList();
                    break;

                case 1: // Без сортировки (оригинальный список из базы данных)
                    service = App.db.Service.ToList();
                    break;

                case 2: // Сортировка по убыванию
                    service = service
                        .OrderByDescending(x => ((double?)x.Cost) - ((double?)x.Cost) * (x.Discount ?? 0) / 100)
                        .ToList();
                    break;
            }
        }

        public void SortingSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchTB.Text))
                service = service.Where(x => x.Title.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
        }

        public void SortingDiscount()
        {
            switch (DiscountCB.SelectedIndex)
            {
                case 0: // Сортировка по возрастанию
                    service = App.db.Service.ToList();
                    break;

                case 1: // Без сортировки (оригинальный список из базы данных)
                    service = service
                    .Where(x => x.Discount == null)
                        .ToList();
                    break;

                case 2: // Сортировка по убыванию
                    service = service
                        .Where(x => x.Discount >= 0 && x.Discount < 5f)
                        .ToList();
                    break;
                case 3: // Сортировка по убыванию
                    service = service
                        .Where(x => x.Discount >= 5 && x.Discount < 15f)
                        .ToList();
                    break;
                case 4: // Сортировка по убыванию
                    service = service
                        .Where(x => x.Discount >= 15 && x.Discount < 30f)
                        .ToList();
                    break;
                case 5: // Сортировка по убыванию
                    service = service
                        .Where(x => x.Discount >= 30 && x.Discount < 70f)
                        .ToList();
                    break;
                case 6: // Сортировка по убыванию
                    service = service
                        .Where(x => x.Discount >= 70 && x.Discount < 100f)
                        .ToList();
                    break;
            }
        }


        private void CostMaxMinCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePage();
        }
        private void DiscountCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePage();
        }

        private void Button_Click_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

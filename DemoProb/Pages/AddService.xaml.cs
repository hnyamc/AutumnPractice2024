using DemoProb.DB;
using Microsoft.Win32;
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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        private string selectedImagePath;
        public string folderName = "Услуги школы";
        int charactersToRemove = 61; // Количество символов для удаления в начале строки
        public AddService()
        {
            InitializeComponent();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.EnterPage());
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Service ser = new Service();
            try
            {
                var service = App.db.Service.FirstOrDefault(s => s.Title == TitleServiceTBox.Text);
                if (service != null)
                {
                    throw new FormatException("Такое название уже есть");
                }
                if (TitleServiceTBox.Text == "" || (Convert.ToDecimal(CostTBox.Text) <= 0 || CostTBox.Text == ""))
                {
                    throw new FormatException("Название или цена не указаны");
                }
                else
                {
                    ser.Title = TitleServiceTBox.Text;
                    ser.Cost = Convert.ToDecimal(CostTBox.Text);

                }
                if (TimeTBox.Text == "" || Convert.ToInt32(TimeTBox.Text) >= 240 || Convert.ToInt32(TimeTBox.Text) <= 0)
                {
                    throw new Exception("Время введено не корректно, сеанс должен быть меньше 240 минут и больше 0");
                }
                else
                    ser.DurationInMinutes = Convert.ToInt32(TimeTBox.Text);
                if (DiscountTBox.Text == "")
                    ser.Discount = null;
                else if (Convert.ToInt32(DiscountTBox.Text) < 0 || Convert.ToInt32(DiscountTBox.Text) >= 100)
                    throw new Exception("Скидка введена не корректно или она не может быть меньше 0 или больше 100 ");
                else
                    ser.Discount = Convert.ToInt32(DiscountTBox.Text);
                if (selectedImagePath == "" || selectedImagePath == null)
                    throw new Exception("вы не выбрали фото, повторите попытку");
                else
                    ser.ServicePhotoID = App.db.ServicePhoto.FirstOrDefault(x => x.PhotoPath == selectedImagePath).ID;
                App.db.Service.Add(ser);
                App.db.SaveChanges();
                NavigationService.Navigate(new Pages.EnterPage());
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка формата: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void Button_Click_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {

                selectedImagePath = openFileDialog.FileName;

                // Ищем индекс папки "Услуги школы" и обрезаем строку
                int index = selectedImagePath.IndexOf(folderName);

                if (index != -1)
                {
                    string result = selectedImagePath.Substring(index);
                    selectedImagePath = result;
                }
                ImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
        }

    }
}

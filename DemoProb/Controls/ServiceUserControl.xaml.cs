using DemoProb.DB;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoProb.Controls
{
    /// <summary>
    /// Логика взаимодействия для ServiceUserControl.xaml
    /// </summary>
    public partial class ServiceUserControl : UserControl
    {
        private NavigationService _navigationService;
        private Service ser;
        private Action _isRemove;
        public ServiceUserControl(Service service, Action isRemove)
        {
            InitializeComponent();
            ser = service;
            _isRemove = isRemove;
            TitleServiceTB.Text = ser.Title.ToString();

            // Получить путь к папке "ресурс" относительно папки, в которой находится исполняемый файл
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "StudPracticeAutumn2024/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            //Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));

            if (ser.Discount != null)
            {

                //Зачёркнутый текст
                textDecorate.Text = $"{ser.Cost.Value.ToString("0.#")}";
                textDecorate.TextDecorations = TextDecorations.Strikethrough;

                CostAndTimeTB.Text = $"{(((double?)ser.Cost) - ((double?)ser.Cost) * ser.Discount / 100)} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = $"* скидка {ser.Discount.ToString()}%";
            }
            else
            {
                myGrid.Background = new SolidColorBrush(Colors.LightBlue);
                CostAndTimeTB.Text = $"{ser.Cost.Value.ToString("0.#")} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = "";
            }

        }
        private void NavigateTo(object content)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
                return;
            Frame mainFrame = LogicalTreeHelper.FindLogicalNode(window, "MainFrame") as Frame;
            mainFrame?.Navigate(content);
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            App.db.Service.Remove(ser);
            App.db.SaveChanges();
            _isRemove.Invoke();
            MessageBox.Show("Успешно удалено");

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Pages.EditService(ser));
        }

    }
}

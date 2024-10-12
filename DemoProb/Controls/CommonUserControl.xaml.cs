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
    /// Логика взаимодействия для CommonUserControl.xaml
    /// </summary>
    public partial class CommonUserControl : UserControl
    {
        private Service ser;
        public CommonUserControl(Service service)
        {
            InitializeComponent();
            ser = service;
            TitleServiceTB.Text = ser.Title.ToString();

            // Получить путь к папке "ресурс" относительно папки, в которой находится исполняемый файл
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "DemoProb/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            //Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));

            if (ser.Discount != null)
            {

                //Зачёркнутый текст
                textDecorate.Text = $"{ser.Cost.Value.ToString("0.#")}";
                textDecorate.TextDecorations = TextDecorations.Strikethrough;
                CostAndTimeTB.Text = $"{((((double?)ser.Cost) - ((double?)ser.Cost) * ser.Discount / 100)).ToString()} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = $"* скидка {ser.Discount.ToString()}%";
            }
            else
            {
                myGrid.Background = new SolidColorBrush(Colors.LightBlue);
                CostAndTimeTB.Text = $"{ser.Cost.Value.ToString("0.#")} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = "";
            }

        }
    }
}

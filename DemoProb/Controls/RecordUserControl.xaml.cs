using DemoProb.DB;
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

namespace DemoProb.Controls
{
    /// <summary>
    /// Логика взаимодействия для RecordUserControl.xaml
    /// </summary>
    public partial class RecordUserControl : UserControl
    {

        private string fullName;
        private ClientService clientSer;
        private bool _active;
        public RecordUserControl(ClientService clientService, bool active)
        {
            InitializeComponent();
            _active = active;
            clientSer = clientService;
            DataClient();
        }


        public void DataClient()
        {
            DateTime eventTime = clientSer.StartTime; // Дата из базы данных
            DateTime currentTime = DateTime.Now;      // Текущее время
            TimeSpan timeDifference = eventTime - currentTime;
            if (timeDifference.TotalSeconds > 0)
            {
                if (_active)
                    TimeBeforeStartTB.Foreground = new SolidColorBrush(Colors.Red);
                // Форматируем результат
                string timeLeft = $"{timeDifference.Days} дн. {timeDifference.Hours} ч. {timeDifference.Minutes} мин.";

                // Выводим результат в TextBox (или другой элемент интерфейса)
                TimeBeforeStartTB.Text = timeLeft;
            }
            else
            {
                // Если событие уже прошло
                myGrid.Background = new SolidColorBrush(Colors.OrangeRed);
                TimeBeforeStartTB.Text = "Событие уже прошло";
            }
            fullName = $"{clientSer.Client.FirstName} {clientSer.Client.LastName} {clientSer.Client.Patronymic}";
            TitleServiceTB.Text = clientSer.Service.Title.ToString();
            FIOTB.Text = fullName;
            PhoneTB.Text = clientSer.Client.Phone.ToString();
            EmailTB.Text = clientSer.Client.Email.ToString(); ;
            DataTimeTB.Text = clientSer.StartTime.ToString();
        }
    }
}

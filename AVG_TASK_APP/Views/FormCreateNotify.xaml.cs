using AVG_TASK_APP.DataAccess;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.ViewModels;
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
using System.Windows.Shapes;
using System.Diagnostics;
namespace AVG_TASK_APP.Views
{
    /// <summary>
    /// Interaction logic for FormCreateNotify.xaml
    /// </summary>
    public partial class FormCreateNotify : Window
    {
        private bool isOrange = true; // Initial state
        public FormCreateNotify()
        {
            InitializeComponent();
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOrange)
            {
                Pin.Foreground = Brushes.Orange;
            }
            else
            {
                Pin.Foreground = (Brush)FindResource("color5");
            }
            isOrange = !isOrange;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NotifyRepository notifyRepository = new NotifyRepository();
            Notify notify = new Notify();
            List<Notify> notifyData = (List<Notify>)notifyRepository.GetAll();
            int count = 0;
            for (int i = 0; i < notifyData.Count; i++)
            {
                count++;
            }
            string text = new TextRange(ContentNotifyTextBlock.Document.ContentStart, ContentNotifyTextBlock.Document.ContentEnd).Text;
            notify.Id_User = count + 1;
            notify.Content = text;
            if (Pin.Foreground == Brushes.Orange)
            {
                notify.Pin = 1;
            }
            else
            {
                notify.Pin = 0;
            }
            notify.Id_User = int.Parse(IdUserTextBlock.Text.ToString());
            notifyRepository.Add(notify);
        }
    }
}

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

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for NotifiesUserControl.xaml
    /// </summary>
    public partial class NotifiesUserControl : UserControl
    {
        public  NotifiesUserControl()
        {
            InitializeComponent();
            DataContext = new NotifyViewModel();
            NotifyItem notifyItem1 = new NotifyItem();
            NotifyItem notifyItem2 = new NotifyItem();
            NotifyItem notifyItem3 = new NotifyItem();
            ListNotifies.Children.Add(notifyItem1);
            ListNotifies.Children.Add(notifyItem2);
            ListNotifies.Children.Add(notifyItem3);
        }
        

    }
}

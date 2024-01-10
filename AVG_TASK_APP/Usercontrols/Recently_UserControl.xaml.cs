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

namespace AVG_TASK_APP.Usercontrols
{
    /// <summary>
    /// Interaction logic for RecentlyUserControl.xaml
    /// </summary>
    public partial class Recently_UserControl : UserControl
    {
        public Recently_UserControl()
        {
            InitializeComponent();
        }

        private void Recently_Loaded(object sender, RoutedEventArgs e)
        {
            recentLyPanel.Children.Add(new BtnBoard_UserControl(1, "11"));

        }


    }
}

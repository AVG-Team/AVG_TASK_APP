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

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for RecentlyUserControl.xaml
    /// </summary>
    public partial class RecentlyUserControl : UserControl
    {
        public RecentlyUserControl()
        {
            InitializeComponent();
        }

        private void Recently_Loaded(object sender, RoutedEventArgs e)
        {
            recentLyPanel.Children.Add(new btnBoard(1, "11"));
            
        }

        
    }
}

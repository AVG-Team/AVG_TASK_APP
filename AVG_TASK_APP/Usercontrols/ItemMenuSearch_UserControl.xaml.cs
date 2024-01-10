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
    /// Interaction logic for ItemMenuSearch.xaml
    /// </summary>
    public partial class ItemMenuSearch_UserControl : UserControl
    {
        public string Value { get { return txtValue.Text; } }

        public ItemMenuSearch_UserControl(string value, string content, int levelUser)
        {
            InitializeComponent();
            txtContent.Text = content;
            txtValue.Text = value;

            ImageSource imageSource;
            string relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
            if (levelUser > 0)
                relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
            imageSource = new BitmapImage(new Uri(relativePath, UriKind.RelativeOrAbsolute));
            imageUser.ImageSource = imageSource;
        }
    }
}

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
    /// Interaction logic for ItemMenuSearch.xaml
    /// </summary>
    public partial class ItemMenuSearch : UserControl
    {
        public string Email { get { return txtEmailUser.Text; } }
        public ItemMenuSearch(string email, string nameUser, int levelUser)
        {
            InitializeComponent();
            txtNameUser.Text = nameUser;
            txtEmailUser.Text = email;

            ImageSource imageSource;
            string relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
            if (levelUser != 0)
                relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
            imageSource = new BitmapImage(new Uri(relativePath, UriKind.RelativeOrAbsolute));
            imageUser.ImageSource = imageSource;
        }
    }
}

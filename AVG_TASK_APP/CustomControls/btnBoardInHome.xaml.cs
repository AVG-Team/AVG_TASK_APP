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
    /// Interaction logic for btnBoardInHome.xaml
    /// </summary>
    public partial class btnBoardInHome : UserControl
    {
        public btnBoardInHome(int idTable, string nameTable, string nameWorkspace, int role)
        {
            InitializeComponent();
            this.idTable.Text = idTable.ToString();

            if(nameTable.Length > 22)
            {
                nameTable = nameTable.Substring(15)+"...";
            }
            this.nameTable.Text = nameTable;

            if (nameWorkspace.Length > 22)
            {
                nameWorkspace = nameWorkspace.Substring(15) + "...";
            }
            this.nameWorkspace.Text = nameWorkspace;

            ImageSource imageSource;
            string relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/OIP.jpg";
            if (role != 0)
                relativePath = "pack://application:,,,/AVG_TASK_APP;component/Resources/Images/ADMIN.png";
            imageSource = new BitmapImage(new Uri(relativePath, UriKind.RelativeOrAbsolute));
            imageBtn.ImageSource = imageSource;
        }
    }
}

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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace AVG_TASK_APP.Views.Forms
{
    /// <summary>
    /// Interaction logic for UserInformationUi.xaml
    /// </summary>
    public partial class InfoAccount_View : Window
    {
        public InfoAccount_View()
        {
            InitializeComponent();
        }

        private void btnChangeAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChangeAvatar_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a new avatar image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                // Load and display the selected image as the new avatar
                BitmapImage bitmapImage = new BitmapImage(new Uri(selectedImagePath));
                AvatarImage.ImageSource = bitmapImage;
            }
        }
    }
}

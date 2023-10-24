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
    /// Interaction logic for BoardUserControl.xaml
    /// </summary>
    public partial class BoardUserControl : System.Windows.Controls.UserControl
    {
        List<btnBoard> boards = new List<btnBoard>();
        public BoardUserControl()
        {
            InitializeComponent();         
            btnBoard board1 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            board1.Content.Text = "Add";
            board1.MouseDown += BtnBoard1_Click;
            btnBoard board2 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board3 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board4 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
/*            btnBoard board5 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board6 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board7 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board8 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board9 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };*/
            boards.Add(board1);
            boards.Add(board2);
            boards.Add(board3);
            boards.Add(board4);
/*            boards.Add(board5);
            boards.Add(board6);
            boards.Add(board7);
            boards.Add(board8);
            boards.Add(board9);*/
        }

        private void txtFind_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnBoard1_Click(object sender, MouseButtonEventArgs e)
        {
            btnBoard newBoard = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            boards.Add(newBoard);
        }
        private void txtFind_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFind.Text = ""; // Xóa nội dung mặc định khi bấm vào
            txtFind.Width = 300; // Kích thước mới khi TextBox nhận focus
        }

        private void txtFind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text))
            {
                txtFind.Text = "Search..."; // Đặt lại nội dung mặc định nếu không có gì được nhập
            }
            txtFind.Width = 150;  // Thu hẹp TextBox khi nó mất focus
        }
    }
}

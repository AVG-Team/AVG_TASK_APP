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
            btnBoard board2 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board3 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board4 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board5 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board6 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board7 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board8 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            btnBoard board9 = new btnBoard() { Margin = new Thickness(10, 0, 0, 0), };
            boards.Add(board1);
            boards.Add(board2);
            boards.Add(board3);
            boards.Add(board4);
            boards.Add(board5);
            boards.Add(board6);
            boards.Add(board7);
            boards.Add(board8);
            boards.Add(board9);
            for (int i = 0; i < boards.Count(); i++)
            {
                btnBoard board = new btnBoard();

                // Set the margin for each button
                board.Margin = new Thickness(5); // Adjust as needed

                if (i % 4 == 0)
                {
                    // Create a new horizontal StackPanel for each row of buttons with margin
                    StackPanel rowPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(0, 10, 0, 0), // Adjust margin values as needed
                    };

                    areaBoard.Children.Add(rowPanel);
                }

                // Find the last added StackPanel and add the button to it
                if (areaBoard.Children.Count > 0 && areaBoard.Children[areaBoard.Children.Count - 1] is StackPanel rowStackPanel)
                {
                    rowStackPanel.Children.Add(boards[i]);
                }
            }


        }


        private void AddNewButton()
        {
            btnBoard newBoard = new btnBoard() { Margin = new Thickness(10, 0, 0, 0) };
            newBoard.Content.Text = "New"; // Set content for the new button
            areaBoard.Children.Add(newBoard);
        }
        private void txtFind_MouseDown(object sender, MouseButtonEventArgs e)
        {

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

        private void areaBoard_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

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
    public partial class NotifiesUserControl : Window
    {
        public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(
            "Content", typeof(object), typeof(NotifiesUserControl), new PropertyMetadata(null));

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public NotifiesUserControl()
        {
            InitializeComponent();
        }
        private void CreateUIElements()
        {
            Grid myGrid = new Grid();
            // Create a StackPanel
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.VerticalAlignment = VerticalAlignment.Center;

            // Create a Border
            Border border = new Border();
            border.Margin = new Thickness(10, 10, 0, 0);
            border.CornerRadius = new CornerRadius(10);
            border.Background = new SolidColorBrush(Color.FromRgb(0xF1, 0xF2, 0xF4));
            border.BorderThickness = new Thickness(4);

            // Create a Grid within the Border
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            // Create an Ellipse
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 40;
            ellipse.Height = 40;
            ellipse.Margin = new Thickness(10, 0, 0, 0);
            ellipse.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/Images/OIP.jpg")));

            // Create TextBlocks and other content
            TextBlock textBlock1 = new TextBlock();
            textBlock1.Margin = new Thickness(10, 10, 0, 0);
            textBlock1.Text = "Nguyen Mai Bao Huy";

            // Create additional content as needed

            // Add the UI elements to the layout hierarchy
            stackPanel.Children.Add(border);
            border.Child = grid;
            grid.Children.Add(ellipse);
            grid.Children.Add(textBlock1);
            // Add other content to the grid as needed.

            // Finally, add the StackPanel to the main window's content
            this.Content = stackPanel;
        }
    }
}

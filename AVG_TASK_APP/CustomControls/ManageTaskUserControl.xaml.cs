using AVG_TASK_APP.Models;
using C1.WPF.Core;
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
    /// Interaction logic for ManageTaskUserControl.xaml
    /// </summary>
    public partial class ManageTaskUserControl : UserControl
    {
        private List<UserControl> _listCard = new List<UserControl>();
        private List<ListBox> listBoxes = new List<ListBox>();
        private C1DragDropManager _dd;

        public ManageTaskUserControl()
        {
            InitializeComponent();

            _dd = new C1DragDropManager();

            for (int i = 1; i <= 5; i++)
            {
                CardUserControl cardUserControl = new CardUserControl();
                areaCard.Children.Add(cardUserControl);
                _listCard.Add(cardUserControl);
            }
            foreach (CardUserControl card in _listCard)
            {
                _dd.RegisterDropTarget(card.lb, true);
                listBoxes.Add(card.lb);
            }

            foreach (ListBox lb in listBoxes)
            {
                foreach (Person p in Person.Generate(5))
                {
                    Button button = new Button();


                    var personElement = new ContentPresenter();
                    personElement.Content = p.Name;
                    personElement.MouseLeftButtonUp += personElement_MouseEnter;
                    personElement.ContentTemplate = (DataTemplate)Resources["listBoxItem"];
                    lb.Items.Add(personElement);

                    _dd.RegisterDragSource(personElement, DragDropEffect.Move, ModifierKeys.None);
                    _dd.RegisterDragSource(button, DragDropEffect.Move, ModifierKeys.None);
                    _dd.DragThreshold = 5;

                    personElement.MouseDown += (s, e) =>
                    {
                        e.Handled = true;
                    };
                }
            }

            _dd.DragDrop += _dd_DragDrop;



        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _dd_DragDrop(object source, DragDropEventArgs e)
        {
            // get object being dragged
            UIElement sourceElement = e.DragSource;
            // get source parent, target listboxes
            foreach (ListBox lb in listBoxes)
            {
                if (lb.Items.Contains(sourceElement))
                {
                    ListBox sourceParent = lb;
                    ListBox target = e.DropTarget as ListBox;
                    // get index into target
                    int index = GetDropIndex(e, target);
                    // adjust index
                    if (sourceParent == target)
                    {
                        int sourceIndex = sourceParent.Items.IndexOf(sourceElement);
                        if (index > sourceParent.Items.IndexOf(sourceElement))
                        {
                            index--;
                        }
                        if (index == sourceIndex)
                            return;
                    }
                    // remove from original position, insert into new position
                    sourceParent.Items.Remove(sourceElement);
                    target.Items.Insert(index, sourceElement);
                }
            }
        }

        private int GetDropIndex(DragDropEventArgs e, ListBox? target)
        {
            int index = 0;
            foreach (UIElement child in target.Items)
            {
                Point p = e.GetPosition(child);
                if (p.Y - child.DesiredSize.Height / 2 < 0) break;
                index++;
            }
            return index;
        }

        private void personElement_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


    }
}

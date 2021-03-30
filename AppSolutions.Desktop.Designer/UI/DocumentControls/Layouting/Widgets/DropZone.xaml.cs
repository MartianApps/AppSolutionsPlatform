using AppSolutions.Desktop.Designer.Services;
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

namespace AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting.Widgets
{
    /// <summary>
    /// Interaktionslogik für DropZone.xaml
    /// </summary>
    public partial class DropZone : UserControl
    {
        private bool _draggingBehaviourIsActive;

        public DropZone()
        {
            InitializeComponent();
        }

        public void DraggingIsActive()
        {
            var output = BootStrapper.Resolve<IConsoleOutputService>();
            output.PushInfo("DropZone activated");
            _draggingBehaviourIsActive = true;
            Border.Visibility = Visibility.Visible;
            InnerCanvas.Background = new SolidColorBrush(Colors.LightGray);
        }

        public void DraggingIsInactive()
        {
            _draggingBehaviourIsActive = false;
            Border.Visibility = Visibility.Collapsed;
            InnerCanvas.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_draggingBehaviourIsActive)
            {
                Border.Visibility = Visibility.Visible;
                InnerCanvas.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_draggingBehaviourIsActive)
            {
                Border.Visibility = Visibility.Collapsed;
                InnerCanvas.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void DropZone_Drop(object sender, DragEventArgs e)
        {
            var output = BootStrapper.Resolve<IConsoleOutputService>();
            output.PushInfo("Drop");
        }
    }
}

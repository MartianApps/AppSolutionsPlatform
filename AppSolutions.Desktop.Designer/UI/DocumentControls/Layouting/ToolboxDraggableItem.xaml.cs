using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting
{
    /// <summary>
    /// Interaktionslogik für ToolboxDraggableItem.xaml
    /// </summary>
    public partial class ToolboxDraggableItem : UserControl
    {
        public ToolboxDraggableItem()
        {
            InitializeComponent();
        }

        private ILayoutingToolboxItemViewModel ViewModel => (ILayoutingToolboxItemViewModel)DataContext;

        #region Adornments

        [DllImport("user32.dll")]
        static extern void GetCursorPos(ref PInPoint p);

        private FrameworkElementAdorner myAdornment;
        private PInPoint pointRef = new PInPoint();

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ViewModel.OnDragStart();

                var obj = new DataObject("COLOR", Colors.Red);
                myAdornment = new FrameworkElementAdorner(this)
                {
                    Child = new ToolboxDraggableItem
                    {
                        DataContext = ViewModel
                    }
                };

                var adLayer = AdornerLayer.GetAdornerLayer(ViewModel.DocumentBaseVisual);
                adLayer.Add(myAdornment);
                DragDrop.DoDragDrop(this, obj, DragDropEffects.Copy);
                adLayer.Remove(myAdornment);

                ViewModel.OnDragStop();
            }
        }

        private void UserControl_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            GetCursorPos(ref pointRef);
            Point relPos = this.PointFromScreen(pointRef.GetPoint());
            myAdornment.Arrange(new Rect(relPos, myAdornment.DesiredSize));
        }

        #endregion
    }
}

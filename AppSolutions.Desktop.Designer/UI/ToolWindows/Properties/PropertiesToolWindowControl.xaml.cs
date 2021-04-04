using AppSolutions.Desktop.Designer.ViewModels.ToolWindows.Properties;
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

namespace AppSolutions.Desktop.Designer.UI.ToolWindows
{
    /// <summary>
    /// Interaktionslogik für PropertiesToolWindowControl.xaml
    /// </summary>
    public partial class PropertiesToolWindowControl : UserControl, IView
    {
        public PropertiesToolWindowControl()
        {
            InitializeComponent();
        }

        public PropertiesToolWindowControl(IPropertiesToolWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        public IPropertiesToolWindowViewModel ViewModel => (IPropertiesToolWindowViewModel)DataContext;
    }
}

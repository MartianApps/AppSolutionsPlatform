using AppSolutions.Desktop.Designer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaktionslogik für OutputToolWindowControl.xaml
    /// </summary>
    public partial class OutputToolWindowControl : UserControl, IView
    {
        public OutputToolWindowControl()
        {
            InitializeComponent();
        }

        public OutputToolWindowControl(IConsoleOutputViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

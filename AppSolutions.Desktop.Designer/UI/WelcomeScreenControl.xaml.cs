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

namespace AppSolutions.Desktop.Designer.UI
{
    /// <summary>
    /// Interaktionslogik für WelcomeScreenControl.xaml
    /// </summary>
    public partial class WelcomeScreenControl : UserControl, IView
    {
        public WelcomeScreenControl()
        {
            InitializeComponent();
        }

        public WelcomeScreenControl(IWelcomeScreenViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        public IWelcomeScreenViewModel ViewModel => (IWelcomeScreenViewModel)DataContext;
    }
}

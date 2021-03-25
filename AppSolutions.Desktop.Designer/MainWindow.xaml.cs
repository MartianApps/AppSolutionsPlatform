using AppSolutions.Desktop.Designer.UI;
using AppSolutions.Desktop.Designer.UI.ToolWindows;
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
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IMainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            var welcomeScreen = BootStrapper.Resolve<WelcomeScreenControl>();
            WelcomeControlContainer.Content = welcomeScreen;

            // LeftContainer
            var group = new RadPaneGroup();
            var projectExplorerPane = new RadPane();
            projectExplorerPane.Header = "Project Explorer";
            projectExplorerPane.Content = BootStrapper.Resolve<ProjectExplorerToolWindowControl>();
            group.Items.Add(projectExplorerPane);
            LeftContainer.Items.Add(group);

            // BottomContainer
            group = new RadPaneGroup();
            var outputPane = new RadPane();
            outputPane.Header = "Output";
            outputPane.Content = BootStrapper.Resolve<OutputToolWindowControl>();
            group.Items.Add(outputPane);

            var errorPane = new RadPane();
            errorPane.Header = "Error List";
            group.Items.Add(errorPane);

            BottomContainer.Items.Add(group);
        }
    }
}

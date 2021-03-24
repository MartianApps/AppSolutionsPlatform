using AppSolutions.Desktop.Designer.ViewModels;
using NLog;
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

namespace AppSolutions.Desktop.Designer.UI.ToolWindows
{
    /// <summary>
    /// Interaktionslogik für ProjectExplorerToolWindowControl.xaml
    /// </summary>
    public partial class ProjectExplorerToolWindowControl : UserControl, IView
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ProjectExplorerToolWindowControl()
        {
            InitializeComponent();
        }

        public ProjectExplorerToolWindowControl(IProjectExplorerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        public ProjectExplorerViewModel ViewModel => (ProjectExplorerViewModel)DataContext;

        private void CreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
                openFolderDialog.ShowDialog();
                var folderName = openFolderDialog.FileName;

                ViewModel.CreateNewProject(tbProjectName.Text, folderName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}

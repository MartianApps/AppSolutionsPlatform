using AppSolutions.Desktop.Designer.ViewModels;
using AppSolutions.Platform.Models.Projects;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private RadTreeViewItem ClickedTreeViewItem
        {
            get
            {
                return this.ContextMenu.GetClickedElement<RadTreeViewItem>();
            }
        }

        private void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedTreeViewItem != null)
            {
                IProjectItemViewModel dataItem = this.ClickedTreeViewItem.DataContext as IProjectItemViewModel;
                if (dataItem != null)
                {
                    var items = this.ContextMenu.Items.Cast<RadMenuItem>();

                    var isProject = dataItem.Type == ProjectItemType.Project;
                    var isDocument = (dataItem.Type != ProjectItemType.Project && dataItem.Type != ProjectItemType.Module && dataItem.Type != ProjectItemType.Folder);

                    // Keinen  Rename und auch kein Dokumente erstellen auf Projekt erlauben
                    (items.FirstOrDefault(i => i.Tag?.ToString() == "Rename")).Visibility = isProject ? Visibility.Collapsed : Visibility.Visible;
                    (items.FirstOrDefault(i => i.Tag?.ToString() == "AddFolder")).Visibility = isProject || isDocument ? Visibility.Collapsed : Visibility.Visible;
                    (items.FirstOrDefault(i => i.Tag?.ToString() == "AddPage")).Visibility = isProject || isDocument ? Visibility.Collapsed : Visibility.Visible;
                    (items.FirstOrDefault(i => i.Tag?.ToString() == "AddWorkflow")).Visibility = isProject || isDocument ? Visibility.Collapsed : Visibility.Visible;

                    this.ProjectExplorerTreeView.SelectedItem = dataItem;//Funktioniert nicht. Warum?
                    ViewModel.SelectedItem = dataItem;
                }
                else
                {
                    this.ProjectExplorerTreeView.SelectedItem = null;
                    ViewModel.SelectedItem = null;
                }
            }
        }

        private void ContextMenuClick(object sender, RoutedEventArgs e)
        {
            //if (this.ClickedTreeViewItem == null) return;

            //DataItem item = this.ClickedTreeViewItem.DataContext as DataItem;

            //if (item == null) return;

            //string header = (e.OriginalSource as RadMenuItem).Header as string;
            //switch (header)
            //{
            //    case "New Child":
            //        DataItem newChild = new DataItem();
            //        newChild.Text = "New Child";
            //        item.Items.Add(newChild);
            //        item.IsExpanded = true; // Ensure that the new child is visible
            //        break;
            //    case "New Sibling":
            //        DataItem newSibling = new DataItem();
            //        newSibling.Text = "New Sibling";
            //        item.Parent.Items.Add(newSibling);
            //        break;
            //    case "Delete":
            //        item.Parent.Items.Remove(item);
            //        break;
            //    case "Edit":
            //        this.ClickedTreeViewItem.IsInEditMode = true;
            //        break;
            //    case "Select":
            //        this.ClickedTreeViewItem.IsSelected = true;
            //        break;
            //}
        }
    }
}

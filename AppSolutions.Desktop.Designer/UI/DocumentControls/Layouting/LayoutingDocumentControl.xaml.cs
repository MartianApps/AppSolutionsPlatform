using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using AppSolutions.Platform.Models.Projects;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppSolutions.Desktop.Designer.UI.DocumentControls
{
    /// <summary>
    /// Interaktionslogik für LayoutingDocumentControl.xaml
    /// </summary>
    public partial class LayoutingDocumentControl : UserControl, IView
    {
        public LayoutingDocumentControl()
        {
            InitializeComponent();
        }

        public LayoutingDocumentControl(ILayoutingDocumentViewModel viewModel) : this(ProjectItemType.Unknown, null, viewModel)
        {

        }

        public LayoutingDocumentControl(ProjectItemType type, string documentPath, ILayoutingDocumentViewModel viewModel)
        {
            InitializeComponent();

            this.Loaded += LayoutingDocumentControl_Loaded;
            DataContext = viewModel;

            if (!string.IsNullOrEmpty(documentPath))
            {
                viewModel.LoadDocument(type, documentPath);
            }
        }

        public ILayoutingDocumentViewModel ViewModel => (ILayoutingDocumentViewModel)DataContext;

        private void LayoutingDocumentControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateToolItems(this);

            Storyboard sb = this.FindResource("TransformToolbox") as Storyboard;
            Storyboard.SetTarget(sb, this.Toolbox);
            sb.Begin();
        }
    }
}

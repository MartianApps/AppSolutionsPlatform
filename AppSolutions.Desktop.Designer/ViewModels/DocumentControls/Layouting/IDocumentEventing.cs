using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting
{
    public delegate void ViewModelMouseDownDelegate(IViewModel viewModel);

    public interface IDocumentEventing
    {
        void OnMouseDown(IViewModel viewModel);

        event ViewModelMouseDownDelegate ViewModelMouseDown;
    }
}

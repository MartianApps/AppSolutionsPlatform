using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets
{
    public interface IDropZoneViewModel: IViewModel
    {
        int InsertRow { get; set; }
    }

    public class DropZoneViewModel : AbstractBaseViewModel, IDropZoneViewModel
    {
        public DropZoneViewModel()
        {
        }

        public int InsertRow { get; set; }
    }
}

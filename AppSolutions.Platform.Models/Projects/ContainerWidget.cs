using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    public class ContainerWidget : AbstractWidget
    {
        [XmlElement(ElementName = "Item")]
        public List<ContainerItem> Items { get; set; }

        public int GetTypeCount(Type widgetType)
        {
            if (Items == null || Items.Count == 0)
            {
                return 0;
            }
            else
            {
                var count = 0;
                foreach (var item in Items.Where(o => o.Widget != null))
                {
                    if (item.Widget.GetType().Equals(widgetType))
                    {
                        count++;
                    }
                    if (item.Widget is ContainerWidget)
                    {
                        count += ((ContainerWidget)item.Widget).GetTypeCount(widgetType);
                    }
                    // TODO: Ergänzen um weitere Container Model-Klassen
                }
                return count;
            }
        }
    }

    public class ContainerItem
    {
        [XmlAttribute("row")]
        public int Row { get; set; }

        [XmlElement]
        public AbstractWidget Widget { get; set; }
    }
}

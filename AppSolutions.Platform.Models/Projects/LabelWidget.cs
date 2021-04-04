using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    public class LabelWidget: AbstractWidget
    {
        [XmlAttribute("caption")]
        public string Caption { get; set; }
    }
}

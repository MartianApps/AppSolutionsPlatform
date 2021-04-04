using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    [XmlRoot]
    public class Layout : AbstractWidget
    {
        [XmlAttribute("designHeight")]
        public double DesignHeight { get; set; }

        [XmlAttribute("designWidth")]
        public double DesignWidth { get; set; }

        [XmlElement]
        public ContainerWidget Container { get; set; }
    }
}

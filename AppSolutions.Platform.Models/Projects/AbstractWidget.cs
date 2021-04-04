using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    [XmlInclude(typeof(LabelWidget))]
    [XmlInclude(typeof(TextWidget))]
    [XmlInclude(typeof(ContainerWidget))]
    public abstract class AbstractWidget
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}

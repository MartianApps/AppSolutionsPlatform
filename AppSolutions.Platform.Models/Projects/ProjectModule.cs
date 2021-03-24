using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    public class ProjectModule
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}

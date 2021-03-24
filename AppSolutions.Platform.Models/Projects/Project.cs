using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    [XmlRoot]
    public class Project
    {
        public Project()
        {
            Modules = new List<ProjectModule>();
        }

        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Module")]
        public List<ProjectModule> Modules { get; set; }
    }
}

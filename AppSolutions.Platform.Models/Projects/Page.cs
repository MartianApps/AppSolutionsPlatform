using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    [XmlRoot]
    public class Page
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}

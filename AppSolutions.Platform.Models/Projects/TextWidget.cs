﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppSolutions.Platform.Models.Projects
{
    public class TextWidget : AbstractWidget
    {
        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}

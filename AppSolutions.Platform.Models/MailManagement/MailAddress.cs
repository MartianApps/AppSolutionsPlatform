using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailAddress
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string DisplayName { get; set; }
    }
}

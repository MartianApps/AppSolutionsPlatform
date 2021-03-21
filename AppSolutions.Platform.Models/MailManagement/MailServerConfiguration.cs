using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailServerConfiguration
    {
        [DataMember]
        public string Hostname { get; set; }
        [DataMember]
        public int Port { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public bool EnableSsl { get; set; }
        [DataMember]
        public bool IsActive { get; set; }

    }
}

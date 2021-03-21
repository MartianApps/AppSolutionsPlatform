using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.Common
{
    [DataContract]
    public class WorkContext
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string LoginName { get; set; }
    }
}

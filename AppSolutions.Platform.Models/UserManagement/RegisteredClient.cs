using System;
using System.Runtime.Serialization;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class RegisteredClient
    {
        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public RegisteredClientState State { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }
    }

    public enum RegisteredClientState : int
    {
        Pending = 0,
        Active = 1,
        Inactive = 2
    }
}

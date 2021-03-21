using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class ClientRegistrationRequest
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EncryptedPassword { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}

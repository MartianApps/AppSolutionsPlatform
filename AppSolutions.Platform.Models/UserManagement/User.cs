using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool EmailIsValidated { get; set; }

        [DataMember]
        public string Password { get; set; }   

        [DataMember]
        public DateTime PasswordLastChangedDate { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public DateTime LastLoginDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }

        [DataMember]
        public long UpdateTimestamp { get; set; }
    }
}

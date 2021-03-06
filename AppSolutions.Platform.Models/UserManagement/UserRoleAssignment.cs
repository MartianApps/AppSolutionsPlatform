using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class UserRoleAssignment
    {
        [DataMember]
        public Guid UserRoleAssignmentId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public DateTime? ValidFromDate { get; set; }

        [DataMember]
        public DateTime? ValidTillDate { get; set; }

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

        [DataMember]
        public Role Role { get; set; }
        [DataMember]
        public User User { get; set; }
    }
}

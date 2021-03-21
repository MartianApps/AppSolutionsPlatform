using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class RoleOnRoleAssignment
    {
        [DataMember]
        public Guid RoleOnRoleAssignmentId { get; set; }

        [DataMember]
        public Guid ParentRole { get; set; }

        [DataMember]
        public Guid ChildRole { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public Role ChildRoleNavigation { get; set; }

        [DataMember]
        public Role ParentRoleNavigation { get; set; }
    }
}

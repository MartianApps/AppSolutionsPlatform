using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class Group
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public bool IsActive { get; set; }
        
        [DataMember]
        public string Description { get; set; }

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
        public ICollection<GroupRoleAssignment> GroupRoleAssignments { get; set; }
        
        [DataMember]
        public ICollection<UserGroupAssignment> UserGroupAssignments { get; set; }
    }
}

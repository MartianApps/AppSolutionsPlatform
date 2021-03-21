using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class Role
    {
        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }
     
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public bool IsActive { get; set; }
        
        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public string CreationUser { get; set; }
        
        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }
        
        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public long UpdateTimestamp { get; set; }

        [DataMember]
        public ICollection<GroupRoleAssignment> GroupRoleAssignments { get; set; }

        [DataMember]
        public ICollection<RoleOnRoleAssignment> ParentRoleOnRoleAssignments { get; set; }

        [DataMember]
        public ICollection<RoleOnRoleAssignment> ChildRoleOnRoleAssignments { get; set; }

        [DataMember]
        public ICollection<UserRoleAssignment> UserRoleAssignments { get; set; }

        [DataMember]
        public ICollection<RolePermissionAssignment> RolePermissionAssignments { get; set; }
    }
}

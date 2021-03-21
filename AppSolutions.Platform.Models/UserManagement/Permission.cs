using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class Permission
    {
        [DataMember]
        public Guid PermissionId { get; set; }

        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public bool IsMandatory { get; set; }

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
        public ICollection<RolePermissionAssignment> RolePermissionAssignment { get; set; }
    }
}

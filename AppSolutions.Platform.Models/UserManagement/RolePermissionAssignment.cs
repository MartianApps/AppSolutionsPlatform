using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class RolePermissionAssignment
    {
        [DataMember]
        public Guid RolePermissionId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public Guid PermissionId { get; set; }

        [DataMember]
        public bool IsGranted { get; set; }

        [DataMember]
        public DateTime? ValidationFromDate { get; set; }

        [DataMember]
        public DateTime? ValidTillDate { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public Permission Permission { get; set; }

        [DataMember]
        public Role Role { get; set; }
    }
}

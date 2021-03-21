using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class GroupRoleAssignment
    {
        [DataMember]
        public Guid GroupRoleAssignmentId { get; set; }
        
        [DataMember]
        public Guid GroupId { get; set; }
        
        [DataMember]
        public Guid RoleId { get; set; }
        
        [DataMember]
        public string CreationUser { get; set; }
        
        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public Group Group { get; set; }
        
        [DataMember]
        public Role Role { get; set; }
    }
}

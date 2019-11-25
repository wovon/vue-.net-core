using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public partial class UserRole:Entity
    {

        public string UserId { get; set; }


        public string RoleId { get; set; }
        
        [NotMapped()]
        public User User { get; set; }
        [NotMapped()]
        public Role Role { get; set; }
        [NotMapped()]
        public List<UserRole> UserRoles { get; set; }
    }

}

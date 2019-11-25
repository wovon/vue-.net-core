using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class DepartmentRole : Entity
    {
        public string DepartmentId { get; set; }
        //[NotMapped()]
        public string DepartmentName { get; set; }
        [NotMapped()]
        public Department Department { get; set; }

        public string RoleId { get; set; }
        //[NotMapped()]
        public string RoleName { get; set; }

        [NotMapped()]
        public Role Role { get; set; }

        [NotMapped()]
        public List<DepartmentRole> DepartmentRoles { get; set; }

    }
}

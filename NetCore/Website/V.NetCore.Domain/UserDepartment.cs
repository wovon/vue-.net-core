using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Domain.Core;

namespace V.NetCore.Domain
{
    public partial class UserDepartment
    {
        public string UserId { get; set; }
        [NotMapped()]
        public User User { get; set; }
        public string DepartmentId { get; set; }
        [NotMapped()]
        public Department Department { get; set; }

        [NotMapped()]
        public List<UserDepartment> UserDepartments { get; set; }
    }
}

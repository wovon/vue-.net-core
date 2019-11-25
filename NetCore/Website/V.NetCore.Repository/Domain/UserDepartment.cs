using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public partial class UserDepartment:Entity
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        [NotMapped()]
        public User User { get; set; }

        public string DepartmentId { get; set; }

        public  string DepartmentName { get; set; }

        [NotMapped()]
        public Department Department { get; set; }
    }
}

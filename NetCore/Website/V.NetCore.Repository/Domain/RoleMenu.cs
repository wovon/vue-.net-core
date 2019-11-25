using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace V.NetCore.Repository.Domain
{
    public class RoleMenu
    {
        public string RoleId { get; set; }
        [NotMapped()]
        public Role Role { get; set; }
        public string MenuId { get; set; }
        [NotMapped()]
        public Menu Menu { get; set; }
    }
}

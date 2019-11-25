using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class Role:TreeEntity
    {
        public Role()
        {
            Sort = 1000;
        }

        //[Column(Order = 2)]
        //[StringLength(50)]
        //public string Name { get; set; }

        //public string Label { get; set; }

        [Column(Order = 3)]
        [StringLength(500)]
        [DisplayName("说明")]
        public string Explain { get; set; }



        public bool IsAble { get; set; } = false;
        public bool IsDel { get; set; } = false;

        [Column(Order = 5)]
        [DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }

        [Column(Order = 6)]
        [DisplayName("修改时间")]
        public DateTime UpdateTime { get; set; }

        [Column(Order = 7)]
        [DisplayName("排序")]
        public int Sort { get; set; }

        [NotMapped()]
        public string DepartmentIds { get; set; }

        [NotMapped()]
        public string DepartmentNames { get; set; }

        [NotMapped()]
        public List<RoleMenu> RoleMenus { get; set; }

        [NotMapped()]
        public List<User> Users { get; set; }
        [NotMapped()]
        public List<UserRole> UserRoles { get; set; }

        [NotMapped()]
        public List<DepartmentRole> DepartmentRoles { get; set; }
    }
}

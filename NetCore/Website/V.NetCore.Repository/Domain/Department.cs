using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class Department :TreeEntity
    {
        public Department()
        {
            Sort = 1000;
        }

        ////设置对应子类的ID
        //[Column(Order = 4)]
        //[StringLength(50)]
        //[DisplayName("设置对应子类的ID")]
        //public string UseChildId { get; set; }

        [Column("Explain")]
        [StringLength(500)]
        [DisplayName("说明")]
        public string Explain { get; set; }
        
        [Column("CreateTime")]
        [DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }

        [Column("UpdateTime")]
        [DisplayName("修改时间")]
        public DateTime UpdateTime { get; set; }

        [Column("IsAble")]
        [DisplayName("是否禁用")]
        public bool IsAble { get; set; }

        public bool IsDel { get; set; }

        [Column("Sort")] [DisplayName("排序")]
        public int Sort { get; set; }

        [NotMapped()]
        public string RoleIds { get; set; }
        [NotMapped()]
        public string RoleNames { get; set; }

        [NotMapped()]
        public List<User> Users { get; set; }
        [NotMapped()]
        public List<UserDepartment> UserDepartments { get; set; }

        [NotMapped()]
        public List<DepartmentRole> DepartmentRoles { get; set; }
    }
}

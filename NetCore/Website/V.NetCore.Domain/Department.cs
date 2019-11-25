using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Domain.Core;

namespace V.NetCore.Domain
{
    public partial class Department :TreeEntity
    {

        [Column("Explain")]
        [StringLength(500)]
        [DisplayName("说明")]
        public string Explain { get; set; }
        
        [Column("AddTime")]
        [DisplayName("添加时间")]
        public DateTime AddTime { get; set; }

        [Column("UpdateTime")]
        [DisplayName("修改时间")]
        public DateTime UpdateTime { get; set; }

        [Column("IsAble")]
        [DisplayName("是否禁用")]
        public bool IsAble { get; set; }

        [Column("Sort")]
        [DisplayName("排序")]
        public int Sort { get; set; }

        [NotMapped()]
        public List<User> Users { get; set; }
        [NotMapped()]
        public List<UserDepartment> UserDepartments { get; set; }
    }
}

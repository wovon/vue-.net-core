using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace V.NetCore.Domain.Core
{
    public abstract class TreeEntity:Entity
    {
        [Column("ParentId")]
        [StringLength(50)]
        [DisplayName("父级Id")]
        public string ParentId { get; set; }

        [Column("ParentName")]
        [StringLength(50)]
        [DisplayName("部门名称")]
        public string ParentName { get; set; }

        [Column("CascadeId")]
        [StringLength(100)]
        [DisplayName("级别")]
        public string CascadeId { get; set; }

        [Column("Name")]
        [StringLength(50)]
        [DisplayName("部门名称")]
        public string Name { get; set; }

    }
}

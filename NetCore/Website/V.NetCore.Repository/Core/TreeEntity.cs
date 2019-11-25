using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Repository.Core
{
    public abstract class TreeEntity:Entity
    {

        [StringLength(50)]
        [DisplayName("名称")]
        [Column(Order = 2)]
        public string Name { get; set; }

        [Column("ParentId")]
        [StringLength(4000)]
        [DisplayName("父级Id")]
        public string ParentId { get; set; }

        [Column("ParentName")]
        [StringLength(4000)]
        [DisplayName("父级名称")]
        public string ParentName { get; set; }

        [Column("CascadeId")]
        [StringLength(100)]
        [DisplayName("级别")]
        public string CascadeId { get; set; }

        [StringLength(50)]
        public string Label { get; set; }

        [NotMapped()]
        public List<dynamic> children { get; set; }

    }
}

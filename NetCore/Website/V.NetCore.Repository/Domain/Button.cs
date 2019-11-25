using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class Button:Entity
    {
        public Button()
        {
            Sort = 1000;
        }
        [StringLength(50)]
        [DisplayName("名称")]
        [Column(Order = 2)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }
        
        public string Label { get; set; }
        public string Title { get; set; }

        [NotMapped()]
        public string MenuId { get; set; }

        [NotMapped()]
        public string ParentId { get; set; }
        [Column("Explain")]
        [StringLength(500)]
        [DisplayName("说明")]
        public string Explain { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int Sort { get; set; }

        [NotMapped()]
        public List<MenuButton> MenuButtons { get; set; }
    }
}

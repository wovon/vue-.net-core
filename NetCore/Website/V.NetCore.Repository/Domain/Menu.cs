using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V.NetCore.Repository.Core;
using V.NetCore.Repository.Domain.Router;

namespace V.NetCore.Repository.Domain
{
    public class Menu : TreeEntity
    {
        public Menu()
        {
            Sort = 1000;
        }
        

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(500)]
        public string LinkAddress { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public string Title { get; set; }

        public string Component { get; set; }

        public string Redirect { get; set; }

        public string Path { get; set; }

        public bool IsAble { get; set; }

        public bool IsDel { get; set; }

        public bool hidden { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int Sort { get; set; }

        [NotMapped()]
        public string ButtonIds { get; set; }

        [NotMapped()]
        public string ButtonNames { get; set; }

        [NotMapped()]
        public List<Button> Buttons { get; set; }

        [NotMapped()]
        public Meta meta { get; set; }

        [NotMapped()]
        public List<MenuButton> MenuButtons { get; set; }

        [NotMapped()]
        public List<RoleMenu> RoeMenus { get; set; }
    }
}

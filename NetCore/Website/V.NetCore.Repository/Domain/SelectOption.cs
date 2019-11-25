using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class SelectOption : TreeEntity
    {
        public SelectOption()
        {
            Sort = 1000;
            IsHide = false;
            IsDel = false;
        }

        public int Code { get; set; }
        [StringLength(50)]
        public string Text { get; set; }
        public int Value { get; set; }
        public int Sort { get; set; }
        public bool IsHide { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }

        [StringLength(1000)]
        public string Explain { get;set; }
    }
}

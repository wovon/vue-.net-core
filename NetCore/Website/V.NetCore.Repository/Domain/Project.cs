using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class Project : TreeEntity
    {
        public Project() 
        {
            IsAble = false;
            IsDel = false;
            Sort = 1000;
        }
        public int ProjectId { get; set; }

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

        [Column("Sort")]
        [DisplayName("排序")]
        public int Sort { get; set; }

        
        [NotMapped()]
        public string WorkFlowIds { get; set; }
        [NotMapped()]
        public string WorkFlowNames { get; set; }
        [NotMapped()]
        public List<WorkFlow> WorkFlows { get; set; }
        [NotMapped()]
        public List<ProjectWorkflow> ProjectWorkflow { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class WorkFlow : Entity
    {
        public WorkFlow()
        {
            IsAble = false;
            IsDel = false;
            Sort = 1000;
        }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Identity { get; set; }
        public int SqlId { get; set; }

        [StringLength(1000)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string Label { get; set; }
        public bool IsAble { get; set; }

        public bool IsDel { get; set; }

        public DateTime CreateTime { get; set; }

        public int Sort { get; set; }

        [NotMapped()]
        public string ProjectId { get; set; }

        public string WorkOrderManagementId { get; set; }

        [NotMapped()]
        public List<ProjectWorkflow> ProjectWorkflow { get; set; }
    }
}

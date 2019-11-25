using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Repository.Domain
{
    public class ProjectWorkflow:Entity
    {
        public string ProjectId { get; set; }
        [NotMapped()]
        public Project Project { get; set; }
        public string WorkflowId { get; set; }
        [NotMapped()]
        public WorkFlow WorkFlows { get; set; }
        
    }
}

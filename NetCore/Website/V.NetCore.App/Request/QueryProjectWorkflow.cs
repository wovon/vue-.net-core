using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.Request
{
    public class QueryProjectWorkflow
    {
        public string Projects { get; set; }

        public List<ProjectWorkflow> ProjectWorkflows { get; set; }
    }
}

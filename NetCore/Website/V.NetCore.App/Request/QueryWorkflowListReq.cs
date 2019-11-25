using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class QueryWorkflowListReq:PageReq
    {
        public string Name { get; set; }
        public string Identity { get; set; }
        public int SqlId { get; set; }
        public bool IsAble { get; set; }
        public bool IsDel { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}

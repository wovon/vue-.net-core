using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class QueryProjectListReq:PageReq
    {
        public QueryProjectListReq() {
            Name = "";
            ProjectId = 0;
            Explain = "";
        }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public string Explain { get; set; }
    }
}

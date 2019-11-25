using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class QueryMenuReq:PageReq
    {
        public QueryMenuReq()
        {
            Name = "";
            Component = "";
            Path = "";
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Component { get; set; }
    }
}

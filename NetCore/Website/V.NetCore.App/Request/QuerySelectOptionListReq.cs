using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class QuerySelectOptionListReq : PageReq
    {
        public string Name { get; set; }
        public string CascadeId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App.Request
{
    public class QueryUserListReq : PageReq
    {
        public QueryUserListReq()
        {
            Sex = 0;
            Status = 0;
        }

        public string DepartmentId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }
        public int Status { get; set; }

        public string Description { get; set; }

        public string Tel { get; set; }
    }
}

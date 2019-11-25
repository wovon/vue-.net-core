using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.Request
{
    public class QueryDepartmentRole
    {
        public string Departments { get; set; }

        public List<DepartmentRole> DepartmentRoles { get; set; }
    }
}

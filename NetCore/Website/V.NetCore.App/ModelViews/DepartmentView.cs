using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Core;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.ModelViews
{
    public class DepartmentView:TreeEntity
    {
        public DepartmentView() {
            Id = Guid.NewGuid().ToString();
            Sort = 1000;
        }
        //public string Id { get; set; }

        //public string Name { get; set; }

        //public string ParentId { get; set; }

        //public string ParentName { get; set; }

        //public string CascadeId { get; set; }

        //public string Label { get; set; }

        //public List<dynamic> children { get; set; }

        public string Explain { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool IsAble { get; set; }

        public bool IsDel { get; set; }

        public int Sort { get; set; }

        public string RoleIds { get; set; }

        public string RoleNames { get; set; }

        public static implicit operator DepartmentView(Department model)
        {
            return model.MapTo<DepartmentView>();
        }

        public static implicit operator Department(DepartmentView view)
        {
            return view.MapTo<Department>();
        }


        public List<User> Users { get; set; }

        public List<UserDepartment> UserDepartments { get; set; }

        public List<DepartmentRole> DepartmentRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using V.NetCore.App.Interface;
using V.NetCore.Infrastructure;
using V.NetCore.Repository;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;

namespace V.NetCore.App.ManagerApp
{
    public class UserDepartmentManagerApp:BaseApp<UserDepartment>
    {
        private readonly IAuth _auth;

        public UserDepartmentManagerApp(IUnitWork unitWork,IRepository<UserDepartment> userRepository,IAuth auth):base (unitWork, userRepository)
        {
            _auth = auth;
        }

        /// <summary>
        /// 设置用户部门
        /// </summary>
        /// <param name="userDepartments"></param>
        public void Add(List<UserDepartment> userDepartments)
        {
            //删除用户角色
            UnitWork.Delete<UserDepartment>(w => userDepartments.Select(s => s.UserId).Contains(w.UserId));
            foreach (var ud in userDepartments)
            {
                //判断用户ID或者角色ID不能为空
                if (!string.IsNullOrEmpty(ud.UserId) && !string.IsNullOrEmpty(ud.DepartmentId))
                {
                    UnitWork.Add(ud);
                }
            }
            UnitWork.Save();
        }


    }
}

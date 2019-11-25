using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.NetCore.App.Interface;
using V.NetCore.Repository;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;

namespace V.NetCore.App.ManagerApp
{
    public class UserRoleManagerApp:BaseApp<UserRole>
    {
        private readonly IAuth _auth;

        public UserRoleManagerApp(IUnitWork unitWork, IRepository<UserRole> repository, IAuth auth) : base(unitWork,
            repository)
        {
            this._auth = auth;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="listUserRole"></param>
        public void Add(List<UserRole> listUserRole)
        {
            //删除用户角色
            UnitWork.Delete<UserRole>(w => listUserRole.Select(s=>s.UserId).Contains(w.UserId));
            foreach (var ur in listUserRole)
            {
                //判断用户ID或者角色ID不能为空
                if (!string.IsNullOrEmpty(ur.UserId) && !string.IsNullOrEmpty(ur.RoleId))
                {
                    UnitWork.Add(ur);
                }
            }
            UnitWork.Save();
        }

    }
}

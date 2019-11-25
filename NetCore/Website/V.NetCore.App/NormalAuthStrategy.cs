using System;
using System.Collections.Generic;
using System.Linq;
using V.NetCore.App.Interface;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;

namespace V.NetCore.App
{
    /// <summary>
    /// 普通用户授权策略
    /// </summary>
    public class NormalAuthStrategy : BaseApp<User>, IAuthStrategy
    {

        public NormalAuthStrategy(IUnitWork unitWork, IRepository<User> repository) : base(unitWork, repository)
        {
            _user = new User
            {
                Account = "v",
                Name = "超级管理员",
                Id = Guid.NewGuid().ToString()
            };
        }

        protected User _user;

        //private List<string> _userRoleIds; //用户角色GUID

        //public List<Role> Roles
        //{
        //    get {
        //      return UnitWork.Find...
        //  }
        //}

        public List<Department> Departments
        {
            get
            {
                var DeptIds = UnitWork.Find<UserDepartment>(
                    u => u.UserId == _user.Id).Select(u => u.DepartmentId);
                return UnitWork.Find<Department>(u => DeptIds.Contains(u.Id)).ToList();
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                //_userRoleIds = .....
            }
        }

        //用户角色

        
    }
}

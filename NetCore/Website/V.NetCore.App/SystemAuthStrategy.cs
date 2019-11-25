using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using V.NetCore.App.Interface;

namespace V.NetCore.App
{
    /// <summary>
    /// 超级管理员权限
    /// </summary>
    public class SystemAuthStrategy : BaseApp<User>, IAuthStrategy
    {
        protected User _user;
        
        public List<Department> Departments
        {
            get { return UnitWork.Find<Department>(null).ToList(); }
        }

        public User User
        {
            get { return _user; }
            set   //禁止外部设置
            {
                throw new Exception("超级管理员，禁止设置用户");
            }
        }

        public SystemAuthStrategy(IUnitWork unitWork, IRepository<User> repository) : base(unitWork, repository)
        {
            
        }
    }
}

using V.NetCore.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.App.Interface;

namespace V.NetCore.App
{
    public class AuthStrategyContext
    {
        private readonly IAuthStrategy _strategy;
        public AuthStrategyContext(IAuthStrategy strategy)
        {
            _strategy = strategy;
        }

        public User User
        {
            get { return _strategy.User; }
        }

        //public List<Role> Roles
        //{
        //    get { return _strategy.Roles; }
        //}

        public List<Department> Departments
        {
            get { return _strategy.Departments; }
        }
    }
}

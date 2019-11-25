using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.App.Interface;

namespace V.NetCore.App
{
    /// <summary>
    ///  加载用户所有可访问的资源
    /// </summary>
    public class AuthContextFactory
    {
        private SystemAuthStrategy _systemAuth;//是否需要
        private NormalAuthStrategy _normalAuthStrategy;
        private readonly IUnitWork _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
            , NormalAuthStrategy normalAuthStrategy
            , IUnitWork unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string username)
        {
            IAuthStrategy service = null;
            service = _normalAuthStrategy;
            service.User = _unitWork.FindSingle<User>(u => u.Account == username);

            return new AuthStrategyContext(service);
        }
    }
}

using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;
using V.NetCore.Infrastructure.Cache;
using System;

namespace V.NetCore.App.SSO
{
    public class LoginParse
    {
        //这个地方使用IRepository<User> 而不使用UserManagerApp是防止循环依赖
        private readonly IRepository<User> _app;
        private readonly ICacheContext _cacheContext;
        private readonly AppInfoService _appInfoService;

        public LoginParse(AppInfoService infoService, ICacheContext cacheContext, IRepository<User> userApp)
        {
            _appInfoService = infoService;
            _cacheContext = cacheContext;
            _app = userApp;
        }

        public LoginResult Do(PassportLoginRequest model)
        {
            var result = new LoginResult();
            try
            {
                model.Trim();
                //获取应用信息
                var appInfo = _appInfoService.Get(model.AppKey);
                if (appInfo == null)
                {
                    throw new Exception("应用不存在");
                }
                //获取用户信息
                var userInfo = _app.FindSingle(u => u.Account == model.Account);

                if (userInfo == null)
                {
                    throw new Exception("用户不存在");
                }
                if (userInfo.Password != Md5.GetMD5String(model.Password))
                {
                    throw new Exception("密码错误");
                }

                var currentSession = new UserAuthSession
                {
                    Account = model.Account,
                    Name = userInfo.Name,
                    Token = Guid.NewGuid().ToString().GetHashCode().ToString("x"),
                    AppKey = model.AppKey,
                    CreateTime = DateTime.Now
                    // , IpAddress = HttpContext.Current.Request.UserHostAddress
                };

                //创建Session
                _cacheContext.Set(currentSession.Token, currentSession, DateTime.Now.AddDays(10));

                result.Code = 200;
                result.ReturnUrl = appInfo.ReturnUrl;
                result.Token = currentSession.Token;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}

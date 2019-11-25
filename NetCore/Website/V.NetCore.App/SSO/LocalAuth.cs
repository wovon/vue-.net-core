using V.NetCore.App.Interface;
using V.NetCore.Infrastructure.Cache;
using Microsoft.AspNetCore.Http;

namespace V.NetCore.App.SSO
{
    public class LocalAuth : IAuth
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthContextFactory _app;
        private readonly LoginParse _loginParse;
        private readonly ICacheContext _cacheContext;


        public LocalAuth(IHttpContextAccessor httpContextAccessor
            , AuthContextFactory app
            , LoginParse loginParse
            , ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _app = app;
            _loginParse = loginParse;
            _cacheContext = cacheContext;
        }

        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query["Token"];
            if (!string.IsNullOrEmpty(token)) return token;

            token = _httpContextAccessor.HttpContext.Request.Headers["X-Token"];
            if (!string.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            return cookie ?? string.Empty;
        }

        public bool CheckLogin(string token = "", string otherInfo = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetToken();
            }
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            try
            {
                var result = _cacheContext.Get<UserAuthSession>(token) != null;
                return result;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherInfo"></param>
        /// <returns></returns>
        public AuthStrategyContext GetCurrentUser(string otherInfo = "")
        {
            AuthStrategyContext context = null;
            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            if (user != null)
            {
                context = _app.GetAuthStrategyContext(user.Account);
            }
            return context;
        }

        /// <summary>
        /// 获取当前登录的用户名
        /// <para>通过URL中的Token参数或Cookie中的Token</para>
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetUserName()
        {
            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            return user?.Account??"";
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="appKey">应用程序key.</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>System.String.</returns>
        public LoginResult Login(string appKey, string username, string pwd)
        {
            var loginRequest = new PassportLoginRequest() {
                AppKey = appKey,
                Account = username,
                Password = pwd
            };
            return _loginParse.Do(loginRequest);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public bool Logout()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token)) return true;

            try
            {
                _cacheContext.Remove(token);
                return true;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }
        
    }
}

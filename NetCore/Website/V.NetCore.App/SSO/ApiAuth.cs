using System;
using V.NetCore.App.Interface;
using V.NetCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace V.NetCore.App.SSO
{
    public class ApiAuth : IAuth
    {
        private readonly IOptions<AppSetting> _appConfiguration;
        private readonly HttpHelper _helper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthContextFactory _authContextFactory;

        public ApiAuth(IOptions<AppSetting> appConfiguration
            , IHttpContextAccessor httpContextAccessor
            , AuthContextFactory authContextFactory
        )
        {
            _appConfiguration = appConfiguration;
            _helper = new HttpHelper(_appConfiguration.Value.SSOPassport);
            _authContextFactory = authContextFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query["Token"];
            if (!string.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            return cookie ?? string.Empty;
        }

        public bool CheckLogin(string token = "", string otherInfo = "")
        {
            return false;
        }

        public AuthStrategyContext GetCurrentUser(string otherInfo = "")
        {
            string username = GetUserName();
            return _authContextFactory.GetAuthStrategyContext(username);
        }

        public string GetUserName()
        {
            var requestUri = string.Format("/api/Check/GetUserName?token={0}", GetToken());
            try
            {
                var value = _helper.Get(null, requestUri);
                var result = JsonHelper.Instance.Deserialize<Response<string>>(value);
                if (result.Code == 200)
                {
                    return result.Result;
                }
                return string.Empty;
            }
            catch// (Exception ex)
            {
                return string.Empty;
            }
        }

        public LoginResult Login(string appKey, string username, string pwd)
        {
            var result = new LoginResult();
            var requestUri = "/api/Check/Login";
            try
            {
                result = JsonHelper.Instance.Deserialize<LoginResult>(_helper.Post(new
                {
                    AppKey = appKey,
                    Account = username,
                    Password = pwd
                }, requestUri));
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public bool Logout()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token)) return true;
            var requestUri = string.Format("/api/Check/Logout?token={0}", token);
            try
            {
                var value = _helper.Post(null, requestUri);
                return true;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }
    }
}

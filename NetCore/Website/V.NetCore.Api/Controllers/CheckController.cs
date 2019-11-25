using V.NetCore.App;
using V.NetCore.App.Interface;
using V.NetCore.App.SSO;
using V.NetCore.Infrastructure;
using V.NetCore.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private AuthContextFactory _app;
        private LoginParse _loginParse;
        private ICacheContext _cacheContext;
        private IAuth _auth;

        public CheckController(AuthContextFactory app, LoginParse loginParse, ICacheContext cacheContext, IAuth auth)
        {
            _app = app;
            _loginParse = loginParse;
            _cacheContext = cacheContext;
            _auth = auth;
        }

        /// <summary>
        /// 检验token是否有效
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="requestid">备用参数.</param>
        [HttpGet]
        public Response<bool> GetStatus(string token, string requestid = "")
        {
            var result = new Response<bool>();
            try
            {
                result.Result = _cacheContext.Get<UserAuthSession>(token) != null;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登录参数</param>
        /// <returns></returns>
        [HttpPost]
        public LoginResult Login([FromBody]PassportLoginRequest request)
        {
            var result = new LoginResult();
            try
            {
                
                result = _loginParse.Do(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException != null
                    ? "WebAPI数据库访问失败:" + ex.InnerException.Message
                    : "WebAPI数据库访问失败:" + ex.Message;
            }

            
            return result;
        }

        private AuthStrategyContext GetAuthStrategyContext(string token = "")
        {
            if (string.IsNullOrEmpty(token))
            {  //当没有token时，尝试从http url或头里获取
                return _auth.GetCurrentUser();
            }
            
            AuthStrategyContext context = null;

            var user = _cacheContext.Get<UserAuthSession>(token);
            if (user != null)
            {
                context = _app.GetAuthStrategyContext(user.Account);
            }
            return context;
        }

        /// <summary>
        /// 根据token获取用户名称
        /// </summary>
        /// <param name="token"></param>
        [HttpGet]
        public Response<string> GetUserName(string token)
        {
            
            GetAuthStrategyContext(token);
            var result = new Response<string>();
            try
            {
                var user = _cacheContext.Get<UserAuthSession>(token);
                if (user != null)
                    result.Result = user.Account;
                else
                    throw new Exception("token非法或已失效");
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        [HttpGet]
        public Response<object> GetUserInfo(string token)
        {

            GetAuthStrategyContext(token);
            var result = new Response<object>();
            try
            {
                var user = _cacheContext.Get<UserAuthSession>(token);
                if (user != null)
                    result.Result = new
                    {
                        roles = new string[] {user.Account},
                        token = token,
                        introduction = user.Name,
                        avatar = "",
                        name = user.Name

                    };
                else
                    throw new Exception("token非法或已失效");
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="token"></param>
        [HttpPost]
        public Response<bool> Logout(string token)
        {
            var resp = new Response<bool>();
            try
            {
                resp.Result = _cacheContext.Remove(token);
            }
            catch (Exception e)
            {
                resp.Result = false;
                resp.Message = e.Message;
            }
            return resp;
        }
    }
}
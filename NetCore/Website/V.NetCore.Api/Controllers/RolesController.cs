using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App;
using V.NetCore.App.Interface;
using V.NetCore.App.ManagerApp;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public RolesController(RoleManagerApp roleManagerApp,IAuth auth)
        {
            RoleManagerApp = roleManagerApp;
            Auth = auth;
        }

        public RoleManagerApp RoleManagerApp { get; }
        public IAuth Auth { get; }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Role> Add([FromBody]Role role)
        {
            var result = new Response<Role>();
            try
            {
                RoleManagerApp.Add(role);
                result.Result = role;
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                RoleManagerApp.Delete(ids);
                result.Result = ids;
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Role> Update([FromBody]Role role)
        {
            var result = new Response<Role>();
            try
            {
                RoleManagerApp.Update(role);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }
        
        /// <summary>
        /// 加载列表
        /// </summary>
        /// <returns></returns>
        public TableData Load()
        {
            return new TableData()
            {
                statusText = "success",
                data = RoleManagerApp.Load()
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App;
using V.NetCore.App.Interface;
using V.NetCore.App.ManagerApp;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        public MenusController(MenuManagerApp menuManagerApp,IAuth iaAuth)
        {
            MenuManagerApp = menuManagerApp;
            IaAuth = iaAuth;
        }

        public MenuManagerApp MenuManagerApp { get; }

        public IAuth IaAuth { get; }

        [HttpPost]
        public Response<Menu> Add([FromBody]Menu menu)
        {
            var result = new Response<Menu>();
            try
            {
                MenuManagerApp.Add(menu);
                result.Result = menu;
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除菜单 及 子集
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                MenuManagerApp.Delete(ids);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 菜单修改
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Menu> Update([FromBody]Menu menu)
        {
            var result = new Response<Menu>();
            try
            {
                MenuManagerApp.Update(menu);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }


        /// <summary>
        /// 设置菜单角色
        /// </summary>
        /// <param name="menuButtons"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> SetMenuButton([FromBody]QueryMenuButton menuButtons)
        {
            var result = new Response<string>();
            try
            {
                MenuManagerApp.SetMenuButton(menuButtons);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public TableData LoadMenuAuthority()
        {
            return new TableData()
            {
                statusText = "success",
                data = MenuManagerApp.LoadMenuAndButton()
            };
        }

        /// <summary>
        /// 加载菜单，有级别
        /// </summary>
        /// <param name="queryMenuReq"></param>
        /// <returns></returns>
        public TableData Load([FromQuery]QueryMenuReq queryMenuReq)
        {
            return new TableData()
            {
                statusText = "success",
                data = MenuManagerApp.Load(queryMenuReq)
            };
        }

        /// <summary>
        /// 获取菜单，无级别
        /// </summary>
        /// <param name="queryMenuReq"></param>
        /// <returns></returns>
        public TableData LoadNoCascade([FromQuery]QueryMenuReq queryMenuReq)
        {
            return new TableData()
            {
                statusText = "success",
                data = MenuManagerApp.LoadNoCascade(queryMenuReq)
            };
        }
    }
}
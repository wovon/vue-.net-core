using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class HistoryController : ControllerBase
    {
        public IAuth IaAuth { get; }

        public HistoryManagerApp HistoryManagerApp { get; }
        public HistoryController(HistoryManagerApp historyManagerApp, IAuth iaAuth)
        {
            HistoryManagerApp = historyManagerApp;
            IaAuth = iaAuth;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<History> Add([FromBody]History history)
        {
            var result = new Response<History>();
            try
            {
                HistoryManagerApp.Add(history);
                result.Result = history;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                HistoryManagerApp.Delete(ids);
                //result.Result = ids;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> Update([FromBody]History history)
        {
            var result = new Response<string>();
            try
            {
                HistoryManagerApp.Update(history);
                result.Result = history.Id;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }


        //[HttpGet]
        //public Response<Button> Get(string buttonId)
        //{
        //    var result = new Response<Button>
        //    {
        //        Result = ButtonManagerApp.Get(buttonId)
        //    };
        //    return result;
        //}

        /// <summary>
        /// 根据按钮加载列表
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public TableData Load([FromQuery]History history)
        {
            return new TableData()
            {
                message = "success",
                data = HistoryManagerApp.Load()
            };
        }

        ///// <summary>
        ///// 根据菜单加载列表
        ///// </summary>
        ///// <param name="menuIds"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public TableData LoadByMenuIds(string menuIds)
        //{
        //    return new TableData()
        //    {
        //        message = "success",
        //        data = ButtonManagerApp.LoadByMenuIds(menuIds)
        //    };
        //}

        ///// <summary>
        ///// 根据菜单加载列表
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="component"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public TableData LoadByMenuName(string name, string component)
        //{
        //    return new TableData()
        //    {
        //        message = "success",
        //        data = ButtonManagerApp.LoadByMenuName(name, component)
        //    };
        //}
    }
}

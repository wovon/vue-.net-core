using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.Interface;
using V.NetCore.App.ManagerApp;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectOptionsController : ControllerBase
    {
        private readonly SelectOptionManagerApp _app;
        private readonly IAuth _auth;

        public SelectOptionsController(SelectOptionManagerApp app,IAuth auth)
        {
            _app = app;
            _auth = auth;
        }
        
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<SelectOption> Add([FromBody]SelectOption selectOption)
        {
            var result = new Response<SelectOption>();
            try
            {
                _app.Add(selectOption);
                result.Result = selectOption;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }

            return result;
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> Update([FromBody] SelectOption selectOption)
        {
            var result = new Response<string>();
            try
            {
                _app.Update(selectOption);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取选项列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public TableData Load()
        {
            return new TableData()
            {
                statusText = "success",
                data = _app.Load()
            };
        }

    }
}
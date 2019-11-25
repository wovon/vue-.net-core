using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.ManagerApp.WorkOrderManagement;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;

namespace V.NetCore.Api.Controllers.WorkOrderManagement
{
    [Route("api/WorkOrderManagement/[controller]/[action]")]
    [ApiController]
    public class WorkOrderManagementsController : ControllerBase
    {
        public WorkOrderManagementManagerApp App { get; }
        public WorkOrderManagementsController(WorkOrderManagementManagerApp workOrderManagement)
        {
            App = workOrderManagement;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="workOrderManagement"></param>
        /// <returns></returns>
        public Response<string> Add(Repository.Domain.WorkOrderManagement.WorkOrderManagement workOrderManagement)
        {
            var result = new Response<string>();
            try
            {
                App.Add(workOrderManagement);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                App.Delete(ids);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="workOrderManagement"></param>
        /// <returns></returns>
        public Response<string> Update(Repository.Domain.WorkOrderManagement.WorkOrderManagement workOrderManagement)
        {
            var result = new Response<string>();
            try
            {
                App.Update(workOrderManagement);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load([FromQuery]QueryWorkOrderManagementListReq request)
        {
            return App.Load(request);
        }

    }
}
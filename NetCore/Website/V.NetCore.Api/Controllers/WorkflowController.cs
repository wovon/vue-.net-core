using System;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.ManagerApp.Workflow;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowManagerApp _app;

        public WorkflowController(WorkflowManagerApp app)
        {
            _app = app;
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="workFlow"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<WorkFlow> Add([FromBody]WorkFlow workFlow)
        {
            var workflow = new WorkFlow()
            {
                Name = workFlow.Name,
	            Label = workFlow.Name,
                Identity = workFlow.Identity,
                SqlId = workFlow.SqlId,
                Remark = workFlow.Remark,
                IsAble = workFlow.IsAble,
                IsDel = workFlow.IsDel,
                WorkOrderManagementId = workFlow.WorkOrderManagementId,
                Sort = workFlow.Sort,
                CreateTime = DateTime.Now
            };
            return _app.Add(workflow);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Response<WorkFlow> Delete(string ids)
        {
            var result = new Response<WorkFlow>();
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
        /// 修改流程
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public Response<WorkFlow> Update(WorkFlow workflow)
        {
            var result = new Response<WorkFlow>();
            try
            {
                _app.Update(workflow);
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
        public TableData Load([FromQuery] QueryWorkflowListReq response)
        {
            return _app.Load(response);
        }


        [HttpGet]
        public TableData LoadByProjectIds(string projectIds)
        {
            return new TableData()
            {
                message = "success",
                data = _app.LoadByProjectIds(projectIds)
            };
        }
    }
}
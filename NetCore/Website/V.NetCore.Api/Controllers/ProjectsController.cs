using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.Interface;
using V.NetCore.App.ManagerApp;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public IAuth IAuth { get; }

        public ProjectManagerApp ProjectManagerApp { get; }

        public ProjectsController(ProjectManagerApp projectManagerApp, IAuth iaAuth)
        {
            ProjectManagerApp = projectManagerApp;
            IAuth = iaAuth;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Project> Add([FromBody]Project project)
        {
            var result = new Response<Project>();
            try
            {
                ProjectManagerApp.Add(project);
                result.Result = project;
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
                ProjectManagerApp.Delete(ids);
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
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> Update([FromBody]Project project)
        {
            var result = new Response<string>();
            try
            {
                ProjectManagerApp.Update(project);
                result.Result = project.Id;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }


        [HttpGet]
        public Response<Project> Get(string projectId)
        {
            var result = new Response<Project>
            {
                Result = ProjectManagerApp.Get(projectId)
            };
            return result;
        }

        /// <summary>
        /// 根据按钮加载列表
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public TableData Load([FromQuery]QueryProjectListReq project)
        {
            return new TableData()
            {
                message = "success",
                data = ProjectManagerApp.Load(project)
            };
        }

        /// <summary>
        /// 设置项目流程
        /// </summary>
        /// <param name="menuButtons"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> SetProjectWorkflow([FromBody]QueryProjectWorkflow projectWorkflows)
        {
            var result = new Response<string>();
            try
            {
                ProjectManagerApp.SetProjectWorkflow(projectWorkflows);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
    }
}

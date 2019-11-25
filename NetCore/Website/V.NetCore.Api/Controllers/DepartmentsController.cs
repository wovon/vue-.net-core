using System;
using System.Collections.Generic;
using V.NetCore.App;
using V.NetCore.App.Interface;
using V.NetCore.App.Response;
using V.NetCore.Repository.Domain;
using V.NetCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using V.NetCore.App.Request;
using V.NetCore.App.ManagerApp;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentManagerApp _app;
        private readonly AuthStrategyContext _authStrategyContext;
        public DepartmentsController(DepartmentManagerApp app, IAuth authUtil)
        {
            _app = app;
            _authStrategyContext = authUtil.GetCurrentUser();
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Department> Add([FromBody]Department department)
        {
            var result = new Response<Department>();
            try
            {
                _app.Add(department);
                result.Result = department;
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
        public Response<string> Update([FromBody] Department department)
        {
            var result = new Response<string>();
            try
            {
                _app.Update(department);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }

            return result;
        }

        /// <summary>
        /// 传入部门ID  设置角色
        /// </summary>
        /// <param name="departmentRoles"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> SetDepartmentRole([FromBody]QueryDepartmentRole departmentRoles)
        {
            var result = new Response<string>();
            try
            {
                _app.SetDepartRole(departmentRoles);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 传入角色ID  设置部门
        /// </summary>
        /// <param name="departmentRoles"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> SetRoleDepartment([FromBody]QueryDepartmentRole roleDepartments)
        {
            var result = new Response<string>();
            try
            {
                _app.SetRoleDepartment(roleDepartments);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据部门获取角色
        /// </summary>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        [HttpGet]
        public TableData GetDepartmentRole(string departmentIds)
        {
            return new TableData()
            {
                statusText = "success",
                data = _app.GetDepartmentRole(departmentIds)
            };
        }

        /// <summary>
        /// 根据角色获取部门
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpGet]
        public TableData GetRoleDepartment(string roleIds)
        {
            return new TableData()
            {
                statusText = "success",
                data = _app.GetRoleDepartment(roleIds)
            };
        }

        /// <summary>
        /// 通过用户ID获取部门和角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public TableData GetUserDepartmentRole(string userIds)
        {
            return new TableData()
            {
                statusText = "success",
                data = _app.GetUserDepartmentRole(userIds)
            };
        }


        /// <summary>
        /// 通过部门id获取其他信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<Department> Get(string deptId)
        {
            var result = new Response<Department>
            {
                Result = _app.Get(deptId)
            };
            return result;
        }

        /// <summary>
        /// 获取部门列表
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
using System;
using V.NetCore.App;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.Tools;
using V.NetCore.App.ManagerApp;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManagerApp _app;
        public UsersController(UserManagerApp app)
        {
            _app = app;
        }
        

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> Add([FromBody]QueryUserListReq request)
        {
            var result = new Response<string>();
            var view = new UserView
            {
                Account = request.Account,
                Name = request.Name,
                Sex = request.Sex,
                Status = request.Status,
                Description = request.Description,
                Tel = request.Tel,
                IsDel = false,
                IsFinger = false,
                RowNum = 0
            };
            try
            {
                _app.Add(view);
                result.Result = view.Id;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除用户
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
                result.Result = ids;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        //修改用户
        [HttpPost]
        public Response<string> Edit([FromBody]QueryUserListReq request)
        {
            var result = new Response<string>();
            var view = new UserView
            {
                Id = request.Id,
                Account = request.Account,
                //Password = request.Password,
                Name = request.Name,
                Sex = request.Sex.ToInt(),
                Tel = request.Tel,
                Description = request.Description
                //Status = request.Status.ToInt(),
            };
            try
            {
                _app.Update(view);
                result.Result = view.Id;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        [HttpGet("{id}")]
        public Response<UserView> Get(string id)
        {
            var result = new Response<UserView>();
            try
            {
                result.Result = _app.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public TableData Load([FromQuery]QueryUserListReq request)
        {
            
            return _app.Load(request);
        }
    }
}
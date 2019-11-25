using Microsoft.AspNetCore.Mvc;
using System;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.App;
using V.NetCore.Infrastructure;
using V.NetCore.App.ManagerApp;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OutBoxController:ControllerBase
    {
        private readonly OutBoxManagerApp _app;
        public OutBoxController(OutBoxManagerApp app)
        {
            _app = app;
        }
        [HttpPost]
        public Response<string> Add([FromHeader]OutBoxListReq request)
        {
            var result = new Response<string>();
            var view = new OutBoxView
            {
                username = request.username,
                Mbno = request.Mbno,
                Msg = request.Msg,
                SendTime = DateTime.Now,
                ComPort = request.ComPort,
                Report=request.Report,
                IsDel = request.IsDel,

            };
            try
            {
                _app.Add(view);
                result.Result = view.id;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        [HttpDelete("{ids}")]
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
        [HttpPut]
        public Response<string> Update([FromHeader]OutBoxListReq request)
        {
            var result = new Response<string>();
            var view = new OutBoxView
            {
                id= request.Id,
                username = request.username,
                Mbno = request.Mbno,
                Msg =request.Msg,
                SendTime = request.SendTime,

            };
            try
            {
                _app.Update(view);
                result.Result = view.id;
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

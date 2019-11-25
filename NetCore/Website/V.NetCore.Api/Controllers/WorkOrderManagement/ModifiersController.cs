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
using V.NetCore.Repository.Domain.WorkOrderManagement;

namespace V.NetCore.Api.Controllers.WorkOrderManagement
{
    [Route("api/WorkOrderManagement/[controller]/[action]")]
    [ApiController]
    public class ModifiersController : ControllerBase
    {
        private readonly ModifiersManagerApp app;

        public ModifiersController(ModifiersManagerApp app)
        {
            this.app = app;
        }

        public Response<string> Add(Modifier modifier)
        {
            var result = new Response<string>();
            try
            {
                app.Add(modifier);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public Response<string> Delete(string ids)
        {
            var result = new Response<string>();
            try
            {
                app.Delete(ids);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public Response<string> Update(Modifier modifier)
        {
            var result = new Response<string>();
            try
            {
                app.Update(modifier);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        public TableData Load([FromQuery]QueryModifierListReq request)
        {
            return app.Load(request);
        }
    }
}
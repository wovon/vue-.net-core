using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.ManagerApp;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleManagerApp _app;

        public UserRoleController(UserRoleManagerApp app)
        {
            _app = app;
        }


        [HttpPost]
        public Response<string> Add([FromBody] List<UserRole> userRole)
        {
            var result = new Response<string>();

            try
            {
                _app.Add(userRole);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message??e.Message;
            }
            return result;
        }
    }
}
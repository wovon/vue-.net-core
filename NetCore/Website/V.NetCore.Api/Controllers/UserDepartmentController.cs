using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App;
using V.NetCore.App.ManagerApp;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDepartmentController : ControllerBase
    {
        private readonly UserDepartmentManagerApp _app;

        public UserDepartmentController(UserDepartmentManagerApp userDepartmentManagerApp)
        {
            _app = userDepartmentManagerApp;
        }

        [HttpPost]
        public Response<string> Add([FromBody]List<UserDepartment> userDepartments)
        {
            var result = new Response<string>();
            try
            {
                _app.Add(userDepartments);
            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }

    }
}
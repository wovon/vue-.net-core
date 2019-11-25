using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.Repository.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController1 : Controller
    {
        // POST api/<controller>
        [HttpPost]
        public User Login([FromBody]string value)
        {
            return  new User();
        }
        
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.Repository.Domain;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var list1 = new List<Model1> { new Model1 { A = 1, B = "1", C = "1",  E = Guid.NewGuid().ToString() },
                new Model1 { A = 2, B = "2", C = "1", E = Guid.NewGuid().ToString() }};

            foreach (var l in list1)
            {
                var list2 = new List<Model1> { new Model1 { A =5, B = Guid.NewGuid().ToString(), C = "2" }, new Model1 { A = 6, B = Guid.NewGuid().ToString(), C = "6" }, new Model1 { A = 7, B = Guid.NewGuid().ToString(), C = "7" }, };
                foreach (var item in list2)
                {
                    item.A = l.A;
                    //item.B = l.B;
                    item.C = item.B;
                    item.E = l.E;
                    item.F = item.C;
                }
                l.D = new List<dynamic>(list2);
            }

            foreach (var item in list1)
            {
                Console.WriteLine(item.A + ":" + item.D.FirstOrDefault().A);
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Response.WriteAsync("a");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Tools;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VueServiceController : ControllerBase
    {
        public TableData List()
        {
            var result = new TableData
            {
                status = 200,
                statusText = "成功"
            };  
            var _listTems = new List<Article>();

            for (var i = 1; i <= 50; i++)
            {
                _listTems.Add(new Article()
                {
                    author = "" + i,
                    display_time = DateTime.Now.ToTime("yyyy-MM-dd HH:mm:ss"),
                    forecast = i,
                    id = i,
                    importance = 2,
                    pageviews = "",
                    reviewer = "",
                    status = "",
                    timestamp = 123445123,
                    title = "adsfasdf" + i,
                    type = "china"

                });
            }

            result.items = _listTems;
            result.total = _listTems.Count;

            return result;
        }
    }
}
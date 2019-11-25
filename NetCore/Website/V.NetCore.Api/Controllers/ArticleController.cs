using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V.NetCore.App;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Tools;

namespace V.NetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        public ArticleController()
        {
        }
        
        public TableData List()
        {
            var result = new TableData
            {
                status = 200,
                statusText = "成功"
            };
            var _items = new List<Article>();

            for (int i = 1; i <= 50; i++)
            {
                _items.Add(new Article()
                {
                    author = "" + i,
                    display_time = DateTime.Now.ToTime("yyyy-MM-dd HH:mm:ss"),
                    forecast = i,
                    id = i,
                    importance = 2,
                    pageviews = "",
                    reviewer="",
                    status = "",
                    timestamp = 123445123,
                    title = "adsfasdf" + i,
                    type = "china"

                });
            }
            result.data = new Result()
            {
                items = _items,
                total = _items.Count

            };

            return result;
        }
    }
}
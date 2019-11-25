using Microsoft.AspNetCore.Mvc;

namespace V.NetCore.App
{
    public class BaseControllerApi:ControllerBase
    {
        protected Infrastructure.Response Result = new Infrastructure.Response();
    }
}

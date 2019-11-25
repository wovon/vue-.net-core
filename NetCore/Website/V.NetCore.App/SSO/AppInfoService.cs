using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V.NetCore.App.SSO
{
    public class AppInfoService
    {
        public AppInfo Get(string appKey)
        {
            appKey = string.IsNullOrEmpty(appKey) ? "v" : appKey;
            //将来放到数据里
            return appList.SingleOrDefault(u => u.AppKey == appKey);
        }

        private AppInfo[] appList = new[]
        {
            new AppInfo
            {
                AppKey = "v",
                Icon = "",
                IsEnable = true,
                Remark = "龙图科技",
                ReturnUrl = "http://192.168.1.168:5000",
                Title = "V.NetCore",
                CreateTime = DateTime.Now,
            }
        };
    }
}

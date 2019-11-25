using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.App
{
    public class AppSetting
    {
        public AppSetting()
        {
            SSOPassport = "http://localhost:5000";
            Version = "";
        }
        /// <summary>
        /// SSO地址
        /// </summary>
        public string SSOPassport { get; set; }

        /// <summary>
        /// 版本信息
        /// demo时屏蔽Post请求
        /// </summary>
        public string Version { get; set; }
    }
}

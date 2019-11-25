using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V.NetCore.App.SSO
{
    public class AppInfo
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public string Icon { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsEnable { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

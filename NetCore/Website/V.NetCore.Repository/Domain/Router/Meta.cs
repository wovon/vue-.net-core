using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.Repository.Domain.Router
{
    public class Meta
    {
        public string title { get; set; }
        public string icon { get; set; }
        public string[] roles { get; set; }
        public bool noCache { get; set; }
    }
}

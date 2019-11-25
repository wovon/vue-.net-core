using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain.Router
{
    public class Router:Entity
    {
        public string path { get; set; }
        public string component { get; set; }
        public string redirect { get; set; }
        public string alwaysShow { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public bool noCache { get; set; }
        public Meta meta { get; set; }
        public List<Router> children { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool hidden { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace V.NetCore.Repository.Domain.Router
{
    public class RouterChildren
    {
        public string path { get; set; }
        public string component { get; set; }
        public string redirect { get; set; }
        public string name { get; set; }
        public Meta meta { get; set; }
    }
}

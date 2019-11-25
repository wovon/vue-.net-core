using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace V.NetCore.App.Request
{
    public class OutBoxListReq : PageReq
    {
        public OutBoxListReq()
        {
            IsDel = true;
        }
        public string username { get; set; }
        public string Mbno { get; set; }
        public string Msg { get; set; }
        public DateTime SendTime { get; set; }
        public int ComPort { get; set; }
        public int Report { get; set; }
        public bool IsDel { get; set; }

    }
}
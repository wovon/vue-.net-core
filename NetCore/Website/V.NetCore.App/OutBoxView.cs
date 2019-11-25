using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App
{
    public class OutBoxView
    {
        public string id { get; set; }
        public string username { get; set; }
        public string Mbno { get; set; }
        public string Msg { get; set; }
        public DateTime SendTime { get; set; }
        public int ComPort { get; set; }
        public int Report { get; set; }
        public bool IsDel { get; set; }

        public OutBoxView()
        {
            id = Guid.NewGuid().ToString();
        }

        public static implicit operator OutBoxView(OutBox outBox)
        {
            return outBox.MapTo<OutBoxView>();
        }

        public static implicit operator OutBox(OutBoxView outBoxView)
        {
            return outBoxView.MapTo<OutBox>();
        }
    }
}

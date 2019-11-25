using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.ModelViews
{
    public class HistoryView
    {

        public string Id { get; set; }

        public string Remake { get; set; }

        public string UserName { get; set; }


        
        public int GU_ID { get; set; }

        //主表ID
        
        public int Wom_ID { get; set; }


        //用户Id
     
        public int UserID { get; set; }

 

        //历史修改时间
        public DateTime Updatetime { get; set; }

        


        public static implicit operator HistoryView(History history)
        {
            return history.MapTo<HistoryView>();
        }

        public static implicit operator History(HistoryView view)
        {
            return view.MapTo<History>();
        }
    }
}

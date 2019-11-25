using V.NetCore.Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.Repository.Domain
{
    [Table("History")]
   public class History : Entity
    {

     
  
        [DefaultValue(0)]
        public int GU_ID { get; set; }
        
        //主表ID
        [DefaultValue(0)]
        public int Wom_ID { get; set; }


        //用户Id
        [DefaultValue(0)]
        public int UserID { get; set; }

        //用户姓名
        [StringLength(50)]
        public string UserName { get; set; }

        //历史修改时间
        public DateTime Updatetime { get; set; }

        //意见
        [StringLength(50)]
        public string Remake { get; set; }

    }
}

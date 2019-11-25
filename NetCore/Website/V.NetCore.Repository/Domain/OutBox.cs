using V.NetCore.Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.Repository.Domain
{
    [Table("OutBox")]
    public partial class OutBox : Entity
    {
        public OutBox()
        {
            this.username = string.Empty;
        }
        [Column("username")]
        [DisplayName("用户名")]
        public string username { get; set; }

        [Column("Mbno")]
        [DisplayName("手机号码")]
        public string Mbno { get; set; }

        [DisplayName("发送内容")]
        [StringLength(200)]
        public string Msg { get; set; }

        [DisplayName("发送时间")]
        public DateTime SendTime { get; set; }

        [DisplayName("表现")]
        public int ComPort { get; set; }

        [DisplayName("表现")]
        public int Report { get; set; }

        [DisplayName("发送状态")]
        public bool IsDel { get; set; }

    }
}
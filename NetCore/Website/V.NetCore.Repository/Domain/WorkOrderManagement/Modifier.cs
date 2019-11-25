using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain.WorkOrderManagement
{
    public class Modifier:Entity
    {
        public Modifier()
        {
            UpdateNumber = 0;
            SO_Id = 0;
        }

        [Required]
        [DisplayName("工单Id")]
        [StringLength(50)]
        public string WOM_Id { get; set; }

        [DefaultValue(0)]
        public int UpdateNumber { get; set; }

        [DefaultValue(0)]
        public int SO_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Remark { get; set; }

    }
}

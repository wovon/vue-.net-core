using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class DepartmentParent:Entity
    {

        /// <summary>
        /// 部门ID
        /// </summary>
        [StringLength(50)]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [StringLength(50)]
        public string ParentId { get; set; }
    }
}

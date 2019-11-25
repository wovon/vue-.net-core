using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain.WorkOrderManagement
{
    public class WorkOrderManagement : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [NotMapped]
        public List<Project> Projects { get; set; }

        /// <summary>
        /// 流程
        /// </summary>
        [NotMapped]
        public List<WorkFlow> WorkFlows { get; set; }

        [StringLength(500)]
        [DisplayName("问题标题")]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("内容ID")]
        public string Content { get; set; }

        /// <summary>
        /// 对应上一个问题
        /// </summary>
        [StringLength(500)]
        [DisplayName("对应上一个问题ID")]
        public string ParentId { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        [DisplayName("反馈时间")]
        public DateTime FeedbackTime { get; set; }

        /// <summary>
        /// 反馈给谁 Id
        /// </summary>
        [StringLength(500)]
        [DisplayName("反馈给用户ID")]
        public string FeedbackUserId { get; set; }

        /// <summary>
        /// 反馈给谁 Name
        /// </summary>
        [StringLength(500)]
        [DisplayName("反馈给谁")]
        public string FeedbackUserName { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [DisplayName("优先级")]
        public int Priority { get; set; }

        /// <summary>
        /// 指定修改人
        /// </summary>
        [NotMapped]
        public List<Modifier> Modifiers { get; set; }

        /// <summary>
        /// 修改人记录
        /// </summary>
        [NotMapped]
        public List<History> Histories { get; set; }

        /// <summary>
        /// 选项列表（这里对应修改状态）
        /// </summary>
        [NotMapped]
        public List<SelectOption> SelectOptions { get; set; }

        [StringLength(500)]
        [DisplayName("用户ID")]
        public string UserId { get; set; }

        [StringLength(500)]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        [StringLength(500)]
        [DisplayName("部门ID")]
        public string DepartmentId { get; set; }

        [StringLength(500)]
        [DisplayName("部门名")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 类别 （手机端  PC端）
        /// </summary>
        [DisplayName("类别")]
        public int Type { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [StringLength(500)]
        [DisplayName("来源")]
        public string Origin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }
        [DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }
        [DisplayName("修改时间")]
        public DateTime UpdateTime { get; set; }

        public bool IsDel { get; set; }

    }
}

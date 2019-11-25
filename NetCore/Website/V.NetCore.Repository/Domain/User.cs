using V.NetCore.Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.Repository.Domain
{
    [Table("Users")]
    public partial class User : Entity
    {
        public User()
        {
            Account = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
            Sex = 0;
            Status = 0;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 序号
        /// </summary>
        public long RowNum { get; set; }

        [DisplayName("用户名")]
        [Column(Order = 2)]
        [Required]
        [StringLength(50)]
        public string Account { get; set; }
        
        [DisplayName("密码")]
        [Column(Order = 3)]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [DisplayName("姓名")]
        [Column(Order = 4)]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("性别")]
        [Column(Order = 5)]
        [DefaultValue(0)]
        public int Sex { get; set; }

        [DisplayName("状态")]
        [Column(Order = 6)]
        [DefaultValue(0)]
        public int Status { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [Column(Order = 7)]
        [DefaultValue(0)]
        public string Tel { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [Column(Order = 8)]
        [DefaultValue(0)]
        public string Description { get; set; }

        /// <summary>
        /// 是否指纹账号
        /// </summary>
        [DisplayName("是否指纹账号")]
        [Column(Order = 9)]
        [DefaultValue(0)]
        public bool IsFinger { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        [Column(Order = 10)]
        [DefaultValue(0)]
        public bool IsDel { get; set; }
        
        [DisplayName("添加时间")]
        [Column(Order = 11)]
        //[DefaultValue(typeof(DateTime),"1900-01-01")]
        public DateTime CreateTime { get; set; }

        
        [DisplayName("修改时间")]
        [Column(Order = 12)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [DisplayName("用户号")]
        [Column(Order = 13)]
        [DefaultValue(0)]
        public string UserNumber { get; set; }

        [NotMapped()]
        public List<UserDepartment> UserDepartments { get; set; }

        [NotMapped()]
        public List<UserRole> UserRoles { get; set; }
    }
}

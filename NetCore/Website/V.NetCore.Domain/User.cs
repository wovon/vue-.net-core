using V.NetCore.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.Domain
{
    [Table("Users")]
    public partial class User : Entity
    {
        public User()
        {
            this.Account = string.Empty;
            this.Password = string.Empty;
            this.Name = string.Empty;
            this.Sex = 0;
            this.Status = 0;
            this.CreateTime = DateTime.Now;
        }

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

        [DisplayName("添加时间")]
        [Column(Order = 7)]
        //[DefaultValue(typeof(DateTime),"1900-01-01")]
        public DateTime CreateTime { get; set; }

        
        [DisplayName("修改时间")]
        [Column(Order = 8)]
        public DateTime UpdateTime { get; set; }



        [NotMapped()]
        public List<UserDepartment> UserDepartments { get; set; }
    }
}

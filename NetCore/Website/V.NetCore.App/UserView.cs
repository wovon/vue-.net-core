using V.NetCore.Repository.Domain;
using V.NetCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.App
{
    public class UserView
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int RowNum { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public int Sex { get; set; }
        
        /// <summary>
        /// 当前状态
        /// </summary>
        /// <returns></returns>
        public int Status { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否指纹账号
        /// </summary>
        public bool IsFinger { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        
        public DateTime CreateTime { get; set; }


        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNumber { get; set; }

        /// <summary>
        /// 所属部门名称，多个用，分隔
        /// </summary>
        /// <value>The organizations.</value>
        public string DepartmentNames { get; set; }

        public string DepartmentsIds { get; set; }

        /// <summary>
        /// 角色ID  ，多个用，分隔
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 角色名 ，多个用，分隔
        /// </summary>
        public string RoleNames { get; set; }
        
        public static implicit operator UserView(User user)
        {
            return user.MapTo<UserView>();
        }

        public static implicit operator User(UserView view)
        {
            return view.MapTo<User>();
        }

        public UserView()
        {
            Id = Guid.NewGuid().ToString();
            DepartmentNames = string.Empty;
            DepartmentsIds = string.Empty;
        }
    }
}

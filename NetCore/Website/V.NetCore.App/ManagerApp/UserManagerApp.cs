using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp
{
    public class UserManagerApp : BaseApp<User>
    {
        private readonly IAuth _auth;
        public UserManagerApp(IUnitWork unitWork, IRepository<User> repository, IAuth auth) : base(unitWork, repository)
        {
            _auth = auth;
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="view"></param>
        public void Add(UserView view)
        {
            User user = view;
            user.Password = Md5.GetMD5String("123");
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            UnitWork.Add(user);
            UnitWork.Save();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {
            UnitWork.Delete<UserDepartment>(ud => ids.Contains(ud.UserId));
            UnitWork.Delete<UserRole>(ur => ids.Contains(ur.UserId));
            //UnitWork.Delete<User>(u => ids.Contains(u.Id));
            UnitWork.Update<User>(u=>ids.Contains(u.Id),u=>new User()
            {
                IsDel = true
            });

            UnitWork.Save();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="view"></param>
        public void Update(UserView view)
        {
            UnitWork.Update<User>(u => u.Id == view.Id, u => new User
            {
                Account = view.Account,
                //Password = Md5.GetMD5String(view.Password),
                Name = view.Name,
                Sex = view.Sex,
                Tel = view.Tel,
                Description = view.Description,
                UpdateTime = DateTime.Now,
                //Status = view.Status,
            });
            UnitWork.Save();
        }

        /// <summary>
        /// 加载用户的部门
        /// </summary>
        public IEnumerable<Department> LoadByUser(string userId)
        {
            var result = from u in UnitWork.Find<User>(null)
                         join ud in UnitWork.Find<UserDepartment>(null) on u.Id equals ud.UserId
                         join d in UnitWork.Find<Department>(null) on ud.DepartmentId equals d.Id
                         where u.Id == userId
                         select d;
            return result;
        }


        public User GetByAccount(string account)
        {
            return Repository.FindSingle(u => u.Account == account);
        }

        /// <summary>
        ///     加载当前登录用户可访问的一个部门及子部门全部用户
        /// </summary>
        public TableData Load(QueryUserListReq request)
        {
            
            var loginUser = _auth.GetCurrentUser();

            //where条件
            var sSqlWhere = "IsDel=0";
            //模糊查询
            if (!string.IsNullOrEmpty(request.KeyWorld))
            {
                sSqlWhere = sSqlWhere+ " and (Name like '%" + request.KeyWorld + "%' or Account like '%" + request.KeyWorld + "%') ";
            }
            //获取分页存储过程的参数
            var sqlPageParams = GetQueryPageParams("Users", request.PrimaryKey,
                "Id, Account,'' as Password, Name, Sex, Status, CreateTime, UpdateTime, Description, IsDel, IsFinger, Tel, UserNumber",
                request.PageSize, request.PageIndex, sSqlWhere,
                "", request.PrimaryKey + " " + request.Order);
            //获取用户数据
            var users = UnitWork.SqlQuery<User>(
                "proc_Paging @TableNames,@PrimaryKey,@Columns,@PageSize,@PageIndex,@sWhere,@Group,@Order,@totalCount output"
                , sqlPageParams).ToList();
            
            //权限问题（待做）
            
            //添加部门信息，逗号隔开
            var userViews = new List<UserView>();
            //
            if (users.Any())
            {
                foreach (var user in users)
                {
                    UserView uv = user;
                    var listDeparmentRoles = UnitWork.SqlQuery<DepartmentRole>(
                        @"select '' Id, DepartmentId, DepartmentName, RoleId, RoleName From view_UserRoleAndDepartment where UserId = @UserId
                    ", new object[]
                        {
                            new SqlParameter()
                            {
                                ParameterName = "@UserId",
                                Value = user.Id,
                                DbType = DbType.String
                            }
                        }).ToList();
                    uv.DepartmentsIds = string.Join(",", listDeparmentRoles.Select(dr => dr.DepartmentId).Distinct().ToList());
                    uv.DepartmentNames = string.Join(",", listDeparmentRoles.Select(dr => dr.DepartmentName).Distinct().ToList());
                    
                    uv.RoleIds = string.Join(",", listDeparmentRoles.Select(dr => dr.RoleId).Distinct().ToList());
                    uv.RoleNames = string.Join(",", listDeparmentRoles.Select(dr => dr.RoleName).Distinct().ToList());
                    
                    userViews.Add(uv);
                }
            }

            return new TableData
            {
                total = ((SqlParameter)sqlPageParams.ToList()[8]).Value.ToInt(0),
                items = userViews
            };
        }
    }
}
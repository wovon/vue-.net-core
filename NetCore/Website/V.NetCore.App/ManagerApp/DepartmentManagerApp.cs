using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V.NetCore.App.Request;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.App.ModelViews;

namespace V.NetCore.App.ManagerApp
{
    public class DepartmentManagerApp : BaseApp<Department>
    {
        public DepartmentManagerApp(IUnitWork unitWork, IRepository<Department> repository) : base(unitWork, repository)
        {
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public string Add(Department department)
        {
            department.CreateTime = DateTime.Now;
            department.UpdateTime = DateTime.Now;
            department.ParentId = string.IsNullOrEmpty(department.ParentId) ? null : department.ParentId;
            ChangeModuleCascade(department);
            UnitWork.Add(department);
            UnitWork.Save();
            return department.Id;
        }

        /// <summary>
        /// 删除部门及子类 以及用户关联的
        /// </summary>
        /// <param name="deptIds"></param>
        /// <returns></returns>
        public void Delete(string deptIds)
        {
            //当前部门有用户关联的
            UnitWork.Delete<UserDepartment>(ud => deptIds.Contains(ud.DepartmentId));
            //删除有角色对应的
            UnitWork.Delete<DepartmentRole>(dr => deptIds.Contains(dr.DepartmentId));
            //删除部门
            //var listDept = Repository.Find(w => deptIds.Contains(w.Id) && !w.ParentId.Contains(",")).ToList();
            var listDept = Repository.Find(w => deptIds.Contains(w.Id)).ToList();
            foreach (var ld in listDept)
            {
                foreach (var casc in ld.CascadeId.Split(','))
                {
                    UnitWork.Update<Department>(d=>d.CascadeId.Contains(casc),d=>new Department()
                    {
                        IsDel = true
                    });
                }
                //upParentId(listDept,ld.Id);
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department"></param>
        public void Update(Department department)
        {
            department.ParentId = string.IsNullOrEmpty(department.ParentId) ? null : department.ParentId;
            ChangeModuleCascade(department);

            //获取旧的的CascadeId
            var cascadeId = Repository.FindSingle(d => d.Id == department.Id).CascadeId;
            //根据CascadeId查询子部门
            var departments = Repository.Find(d => d.CascadeId.Contains(cascadeId) && d.Id!=department.Id)
                .OrderBy(d => d.CascadeId).ToList();
            
            //
            UnitWork.Update<Department>(d => d.Id == department.Id, d => new Department()
            {
                Explain = department.Explain,
                //UseChildId = department.UseChildId,
                UpdateTime = DateTime.Now,
                IsAble = department.IsAble,
                Sort = department.Sort,
                Name = department.Name,
                ParentId = department.ParentId,
                ParentName = department.ParentName,
                CascadeId = department.CascadeId
            });
            //更新子部门的CascadeId
            foreach (var dept in departments)
            {
                ChangeModuleCascade(dept);
                UnitWork.Update<Department>(d=>d.Id== dept.Id,dd=>new Department()
                {
                    CascadeId = dept.CascadeId
                });
            }
            UnitWork.Save();
        }

        
        /// <summary>
        /// 通过部门设置角色
        /// </summary>
        /// <param name="departments"></param>
        public void SetDepartRole(QueryDepartmentRole departments)
        {
            UnitWork.Delete<DepartmentRole>(d =>
                departments.DepartmentRoles.Where(w => w.DepartmentId == d.DepartmentId).Count()>0);
            foreach (var dept in departments.DepartmentRoles)
            {
                if (!string.IsNullOrEmpty(dept.RoleId))
                {
                    UnitWork.Add(new DepartmentRole()
                    {
                        DepartmentId = dept.DepartmentId,
                        RoleId = dept.RoleId
                    });
                }
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 通过角色设置部门
        /// </summary>
        /// <param name="roles"></param>
        public void SetRoleDepartment(QueryDepartmentRole roles)
        {

            UnitWork.Delete<DepartmentRole>(d =>
                roles.DepartmentRoles.Where(w => w.RoleId == d.RoleId).Count() > 0);
            foreach (var r in roles.DepartmentRoles)
            {
                if (!string.IsNullOrEmpty(r.DepartmentId))
                {
                    UnitWork.Add(new DepartmentRole()
                    {
                        DepartmentId = r.DepartmentId,
                        RoleId = r.RoleId
                    });
                }
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 通过部门获取角色
        /// </summary>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        public List<Role> GetDepartmentRole(string departmentIds)
        {
            if (string.IsNullOrEmpty(departmentIds))
            {
                return new List<Role>();
            }
            //else if (departmentIds.Trim(',').Contains(","))
            //{
            //    return new List<Role>();
            //}
            else
            {
                return UnitWork.SqlQuery<Role>("select r.id,r.name as Label,name,createTime,updateTime,Sort,explain,IsAble,IsDel,r.CascadeId,r.ParentId,r.ParentName " +
                                               "from Roles r left join DepartmentRole dr on r.Id = dr.RoleId " +
                                               "where r.isAble=0 and r.isDel=0 and dr.DepartmentId in ('" + departmentIds.Trim(',').Replace(",","','")+"')", new object[] { }).ToList();
            }
        }

        /// <summary>
        /// 通过角色获取部门
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<Department> GetRoleDepartment(string roleIds)
        {
            if (string.IsNullOrEmpty(roleIds))
            {
                return new List<Department>();
            }
            //else if (roleIds.Trim(',').Contains(","))
            //{
            //    return new List<Department>();
            //}
            else
            {
                var sSQL = "select distinct d.id,d.name as Label,d.name,d.createTime,d.updateTime,d.Sort,d.explain,d.IsAble,d.IsDel,d.CascadeId,d.ParentId,d.ParentName " +
                           "From Departments d left join DepartmentRole dr on d.Id = dr.DepartmentId " +
                           "where d.isAble=0 and d.isDel=0 and dr.RoleId in ('" + roleIds.Trim(',').Replace(",", "','") + "')";
                return UnitWork.SqlQuery<Department>(sSQL, new object[] { }).ToList();
                //return UnitWork.SqlQuery<Department>("select r.id,r.name as Label,name,createTime,updateTime,Sort,explain,IsAble,IsDel,r.CascadeId,r.ParentId,r.ParentName from Roles r left join DepartmentRoles dr on r.Id = dr.RoleId where dr.RoleId in ('" + roleIds.Trim(',').Replace(",", "','") + "')", new object[] { }).ToList();
            }
        }

        /// <summary>
        /// 通过用户ID获取对应的部门和角色
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public List<DepartmentRole> GetUserDepartmentRole(string userIds)
        {
            if (string.IsNullOrEmpty(userIds))
            {
                return new List<DepartmentRole>();
            }
            else
            {
                //return UnitWork.SqlQuery<DepartmentRole>(
                //    @"select isnull(dr.Id,'') as Id,isnull(r.Id,'') RoleId,r.Name RoleName, isnull(d.Id,'') as DepartmentId,isnull(d.Name,'') as DepartmentName From Users u
                //        left join UserRole ur on u.Id = ur.UserId
                //        left join Roles r on ur.RoleId = r.Id
                //        left join DepartmentRoles dr on r.Id = dr.RoleId
                //        left join Departments d on dr.DepartmentId = d.Id
                //        where u.id in (@UserId)
                return UnitWork.SqlQuery<DepartmentRole>(
                    @"select '' Id, RoleId, RoleName, DepartmentId, DepartmentName From view_UserRoleAndDepartment where UserId in (@UserId)
                    ", new object[]
                    {
                        new SqlParameter()
                        {
                            ParameterName = "@UserId",
                            Value = userIds.Trim(',').Replace(",", "','"),
                            DbType = DbType.String
                        }
                    }).ToList();
            }
        }

        /// <summary>
        /// 加载特定用户的部门
        /// </summary>
        /// <param name="userId">The user unique identifier.</param>
        public List<Department> LoadForUser(string userId)
        {
            var result = from u in UnitWork.Find<User>(null)
                         join ud in UnitWork.Find<UserDepartment>(null) on u.Id equals ud.UserId
                         join d in UnitWork.Find<Department>(null) on ud.DepartmentId equals d.Id
                         where u.Id == userId
                         select d;
            return result.ToList();
        }
        

        /// <summary>
        /// 加载所有部门
        /// </summary>
        /// <returns></returns>
        public List<DepartmentView> Load()
        {
            
            //var result = (from d in UnitWork.Find<Department>(null) orderby d.CascadeId select d).ToList();
            //var listCascade = UnitWork.SqlQuery<Department>(
            //    "select Id, Name,Name as Label, ParentId, ParentName, CascadeId,UseChildId, Explain, CreateTime, UpdateTime, IsAble, Sort from Departments where id in (select UseChildId From Departments where UseChildId is not null)"
            //    , new object[] { }).ToList();

            var sWhere = new SqlParameter()
            {
                ParameterName = "@where",
                Value = "",
                DbType = DbType.String
            };
            var listDept = UnitWork.SqlQuery<Department>(
                "select Id, Name,Name as Label, ParentId, ParentName, CascadeId, Explain, CreateTime, UpdateTime, IsAble,isDel, Sort from Departments where isAble = 0 and isDel=0 order by Sort desc,CreateTime asc"
                , new object[] { sWhere }).ToList();
            var listDV = new List<DepartmentView>();
            foreach (var d in listDept)
            {
                DepartmentView dv = d;
                var Roles = UnitWork.SqlQuery<Role>(
                    "select r.id,r.name as Label,name,createTime,updateTime,Sort,explain,IsAble,IsDel,r.CascadeId,r.ParentId,r.ParentName " +
                    "from Roles r left join DepartmentRole dr on r.Id = dr.RoleId " +
                    "where r.isAble=0 and r.isDel=0 and dr.DepartmentId in ('" +d.Id + "')", new object[] { }).ToList();
                //d.Roles = Roles;
                dv.RoleIds = string.Join(',', Roles.Select(s => s.Id).ToList());
                dv.RoleNames = string.Join(',', Roles.Select(s => s.Name).ToList());
                listDV.Add(dv);
            }
            return BindNew(listDV, null);
        }
    }
}

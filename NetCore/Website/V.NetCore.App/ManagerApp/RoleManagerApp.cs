using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp
{
    public class RoleManagerApp : BaseApp<Role>
    {
        public RoleManagerApp(IUnitWork unitWork, IRepository<Role> repository) : base(unitWork, repository)
        {
        }

        public void Add(Role role)
        {
            role.IsAble = role.IsAble;
            role.CreateTime = DateTime.Now;
            role.UpdateTime = DateTime.Now;
            role.ParentId = string.IsNullOrEmpty(role.ParentId) ? null : role.ParentId;
            ChangeModuleCascade(role);
            UnitWork.Add(role);
            UnitWork.Save();
        }

        public void Delete(string ids)
        {
            UnitWork.Delete<UserRole>(ur => ids.Contains(ur.RoleId));
            UnitWork.Delete<DepartmentRole>(dr => ids.Contains(dr.RoleId));
            UnitWork.Update<Role>(r => ids.Contains(r.Id),r=>new Role()
            {
                IsDel = true
            });
            UnitWork.Save();
        }

        public void Update(Role role)
        {
            role.ParentId = string.IsNullOrEmpty(role.ParentId) ? null : role.ParentId;
            ChangeModuleCascade(role);

            //获取旧的的CascadeId
            var cascadeId = Repository.FindSingle(d => d.Id == role.Id).CascadeId;
            //根据CascadeId查询子数据
            var roles = Repository.Find(r => r.CascadeId.Contains(cascadeId) && r.Id != role.Id)
                .OrderBy(r => r.CascadeId).ToList();
            //更新
            UnitWork.Update<Role>(r => r.Id == role.Id, u => new Role
            {
                Name = role.Name,
                Explain = role.Explain,
                Sort = role.Sort,
                UpdateTime = DateTime.Now,
                IsAble = role.IsAble,
                IsDel = role.IsDel,
                ParentId = role.ParentId,
                ParentName = role.ParentName,
                CascadeId = role.CascadeId
            });
            //更新子数据的CascadeId
            foreach (var rol in roles)
            {
                ChangeModuleCascade(rol);
                UnitWork.Update<Role>(r => r.Id == rol.Id, r => new Role()
                {
                    CascadeId = rol.CascadeId
                });
            }
            UnitWork.Save();
        }

        public List<Role> Load()
        {
            var listRole = UnitWork.SqlQuery<Role>(
                "select  Id, Name,Name as Label, ParentId, ParentName, CascadeId, Explain, CreateTime, UpdateTime, IsAble,IsDel, Sort from Roles where IsAble = 0 and IsDel=0 order by IsAble asc,CreateTime asc",
                new object[] { }).ToList();
            //return UnitWork.Find<Role>(r=>r.IsDel==false).OrderBy(r=>r.IsAble).ThenBy(r => r.CreateTime).ToList();

            foreach (var r in listRole)
            {
                var Department = UnitWork.SqlQuery<Department>("select distinct d.id,d.name as Label,d.name,d.createTime,d.updateTime,d.Sort,d.explain,d.IsAble,d.IsDel,d.CascadeId,d.ParentId,d.ParentName " +
                                                     "From Departments d left join DepartmentRole dr on d.Id = dr.DepartmentId " +
                                                     "where d.isAble=0 and d.isDel=0 and dr.RoleId in ('" + r.Id + "')", new object[] { }).ToList();
                r.DepartmentIds = string.Join(',', Department.Select(s => s.Id).ToList());
                r.DepartmentNames = string.Join(',', Department.Select(s => s.Name).ToList());
            }
            return BindNew(listRole, null);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain.WorkOrderManagement;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp.WorkOrderManagement
{
    public class ModifiersManagerApp : BaseApp<Modifier>
    {
        private readonly IAuth _auth;
        public ModifiersManagerApp(IUnitWork unitWork, IRepository<Modifier> repository, IAuth auth) : base(unitWork, repository)
        {
            this._auth = auth;
        }

        public void Add(Modifier modifier)
        {
            modifier.CreateTime = DateTime.Now;
            modifier.UpdateTime = DateTime.Now;
            UnitWork.Add(modifier);
            UnitWork.Save();
        }

        public void Delete(string ids)
        {
            UnitWork.Delete<Modifier>(d=>ids.Contains(d.Id));
            UnitWork.Save();
        }

        public void Update(Modifier modifier)
        {
            UnitWork.Update<Modifier>(m=>m.Id==modifier.Id,m=>new Modifier()
            {
                WOM_Id = modifier.WOM_Id,
                UpdateNumber = modifier.UpdateNumber,
                SO_Id = modifier.SO_Id,
                User_Id = modifier.User_Id,
                UserName = modifier.UserName,
                UpdateTime = DateTime.Now,
                Remark = modifier.Remark
            });
            UnitWork.Save();
        }

        public TableData Load(QueryModifierListReq request)
        {
            var loginUser = _auth.GetCurrentUser();
            //var modifiersList = UnitWork.SqlQuery<Modifier>("select [Id], [WOM_ID], [UpdateNumber], [SO_ID], [User_ID], [UserName], [CreateTime], [UpdateTime] from Modifiers").ToList();

            //where条件
            var sSqlWhere = "";
            ////模糊查询
            //if (!string.IsNullOrEmpty(request.KeyWorld))
            //{
            //    sSqlWhere = sSqlWhere + " and (Name like '%" + request.KeyWorld + "%' or Account like '%" + request.KeyWorld + "%') ";
            //}
            //获取分页存储过程的参数
            var sqlPageParams = GetQueryPageParams("Modifiers", request.PrimaryKey,
                "[Id], [WOM_ID], [UpdateNumber], [SO_ID], [User_ID], [UserName], [CreateTime], [UpdateTime],[Remark]",
                request.PageSize, request.PageIndex, sSqlWhere,
                "", request.PrimaryKey + " " + request.Order);
            //获取用户数据
            var modifiersList = UnitWork.SqlQuery<Modifier>(
                "proc_Paging @TableNames,@PrimaryKey,@Columns,@PageSize,@PageIndex,@sWhere,@Group,@Order,@totalCount output"
                , sqlPageParams).ToList();



            return new TableData()
            {
                total = ((SqlParameter)sqlPageParams.ToList()[8]).Value.ToInt(0),
                data = modifiersList
            };
        }
    }
}

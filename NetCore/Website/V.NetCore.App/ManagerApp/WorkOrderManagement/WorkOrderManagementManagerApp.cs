using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Domain.WorkOrderManagement;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp.WorkOrderManagement
{
    public class WorkOrderManagementManagerApp : BaseApp<Repository.Domain.WorkOrderManagement.WorkOrderManagement>
    {
        private readonly IAuth _auth;

        public WorkOrderManagementManagerApp(IUnitWork unitWork,
            IRepository<Repository.Domain.WorkOrderManagement.WorkOrderManagement> repository, IAuth auth) : base(
            unitWork, repository)
        {
            _auth = auth;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="workOrderManagement"></param>
        /// <returns></returns>
        public void Add(Repository.Domain.WorkOrderManagement.WorkOrderManagement workOrderManagement)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="workOrderManagement"></param>
        public void Update(Repository.Domain.WorkOrderManagement.WorkOrderManagement workOrderManagement)
        {

        }

        /// <summary>
        /// 加载列表
        /// </summary>
        /// <returns></returns>
        public TableData Load(QueryWorkOrderManagementListReq request)
        {
            var loginUser = _auth.GetCurrentUser();

            //where条件
            var sSqlWhere = "IsDel=0";
            //模糊查询
            if (!string.IsNullOrEmpty(request.KeyWorld))
            {
                //sSqlWhere = sSqlWhere + " and (Name like '%" + request.KeyWorld + "%' or Account like '%" + request.KeyWorld + "%') ";
            }
            //获取分页存储过程的参数
            var sqlPageParams = GetQueryPageParams("WorkOrderManagements", request.PrimaryKey,
                "Id,Name,Title,Content,ParentId,FeedbackTime,FeedbackUserId,FeedbackUserName,Priority,UserId,UserName,DepartmentId,DepartmentName,Type,Origin,Sort,CreateTime,UpdateTime,IsDel",
                request.PageSize, request.PageIndex, sSqlWhere,
                "", request.PrimaryKey + " " + request.Order);
            //获取用户数据
            var workOrderManagements = UnitWork.SqlQuery<Repository.Domain.WorkOrderManagement.WorkOrderManagement>(
                "proc_Paging @TableNames,@PrimaryKey,@Columns,@PageSize,@PageIndex,@sWhere,@Group,@Order,@totalCount output"
                , sqlPageParams).ToList();
            //
            return new TableData()
            {
                total = ((SqlParameter)sqlPageParams.ToList()[8]).Value.ToInt(0),
                items = workOrderManagements
            };
        }
    }
}

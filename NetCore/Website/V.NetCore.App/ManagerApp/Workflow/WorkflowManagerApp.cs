using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.App.Response;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp.Workflow
{
    public class WorkflowManagerApp:BaseApp<V.NetCore.Repository.Domain.WorkFlow>
    {
        private readonly IAuth _auth;

        public WorkflowManagerApp(IUnitWork unitWork,IRepository<Repository.Domain.WorkFlow> repository,IAuth auth) : base(unitWork, repository)
        {
            _auth = auth;
        }
        
        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public Response<Repository.Domain.WorkFlow> Add(Repository.Domain.WorkFlow workflow)
        {
            var result = new Response<Repository.Domain.WorkFlow>();
            var checkWorkflow = UnitWork.Find<Repository.Domain.WorkFlow>(w =>
                w.Name == workflow.Name || w.Identity == workflow.Identity);
            if (!checkWorkflow.Any())
            {
                UnitWork.Add<Repository.Domain.WorkFlow>(workflow);
                UnitWork.Save();
                return result;
            }
            result.Code = 500;
            result.Message = "流程名或流程标识重复";
            return result;
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string ids)
        {
            UnitWork.Delete<Repository.Domain.WorkFlow>(w=>ids.Contains(w.Id));
            UnitWork.Save();
        }

        /// <summary>
        /// 修改流程
        /// </summary>
        /// <param name="workflow"></param>
        public void Update(Repository.Domain.WorkFlow workflow)
        {
            UnitWork.Update<Repository.Domain.WorkFlow>(w => w.Id == workflow.Id, f => new Repository.Domain.WorkFlow
            {
                Name = workflow.Name,
                Identity = workflow.Identity,
	            Label = workflow.Name,
                IsAble = workflow.IsAble,
                IsDel = workflow.IsDel,
                WorkOrderManagementId = workflow.WorkOrderManagementId,
                Remark = workflow.Remark,
                Sort = workflow.Sort,
                SqlId = workflow.SqlId
            });
            UnitWork.Save();
        }

        /// <summary>
        /// 加载流程列表
        /// </summary>
        /// <returns></returns>
        public TableData Load(QueryWorkflowListReq request)
        {
            //获取分页存储过程的参数
            //var sqlPageParams = GetQueryPageParams("Workflow", "Id",
            //    "Id, Name,[Identity], SqlId, Remark, IsAble, IsDel, Sort",100, 1, "","", "Id Desc");

            var loginUser = _auth.GetCurrentUser();
            //where条件
            var sSqlWhere = "IsDel=0";
            //模糊查询
            if (!string.IsNullOrEmpty(request.KeyWorld))
            {
                sSqlWhere = sSqlWhere + " and (Name like '%" + request.KeyWorld + "%') ";
            }
            //获取分页存储过程的参数
            var sqlPageParams = GetQueryPageParams("WorkFlows", request.PrimaryKey,
                "Id, Name,[Identity], Label,SqlId, Remark, IsAble, IsDel,WorkOrderManagementId,CreateTime, Sort",
                request.PageSize, request.PageIndex, sSqlWhere,
                "", request.PrimaryKey + " " + request.Order);


            //获取流程列表
            var wfList = UnitWork.SqlQuery<Repository.Domain.WorkFlow>(
                "proc_Paging @TableNames,@PrimaryKey,@Columns,@PageSize,@PageIndex,@sWhere,@Group,@Order,@totalCount output"
                , sqlPageParams).ToList();
            return new TableData
            {
                total = ((SqlParameter)sqlPageParams.ToList()[8]).Value.ToInt(0),
                data = wfList
            };
        }

        public List<Repository.Domain.WorkFlow> LoadByProjectIds(string ids)
        {
            var ListWorkflow = UnitWork.SqlQuery<Repository.Domain.WorkFlow>(
                "select wf.* from Project p left join ProjectWorkflow pw on p.Id = pw.ProjectId left join WorkFlows wf on pw.WorkflowId = wf.Id where wf.Id is not " +
                " null and p.ID in ('" + ids.Trim(',').Replace(",", "','") + "') order by wf.Sort desc,wf.id desc", new object[] { }).ToList();          
            return ListWorkflow;
        }

    }
}

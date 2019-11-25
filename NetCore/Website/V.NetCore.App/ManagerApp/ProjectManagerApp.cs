using System;
using System.Collections.Generic;
using V.NetCore.Repository.Interface;
using V.NetCore.Repository.Domain;
using System.Linq;
using V.NetCore.App.Request;
using System.Data.SqlClient;
using System.Data;

namespace V.NetCore.App.ManagerApp
{
    public class ProjectManagerApp:BaseApp<Project>
    {
        public ProjectManagerApp(IUnitWork unitWork,IRepository<Project> repository) : base(unitWork, repository)
        { 
        }

        public void Add(Project project)
        {
            project.CreateTime = DateTime.Now;
            project.UpdateTime = DateTime.Now;
            project.ParentId = string.IsNullOrEmpty(project.ParentId) ? null : project.ParentId;
            ChangeModuleCascade(project);
            UnitWork.Add(project);
            UnitWork.Save();
        }

        public void Update(Project project)
        {
            project.ParentId = string.IsNullOrEmpty(project.ParentId) ? null : project.ParentId;
            ChangeModuleCascade(project);
            //获取旧的CascadeId
            var cascadeId = Repository.FindSingle(p => p.Id == project.Id).CascadeId;
            var projects = Repository.Find(d => d.CascadeId.Contains(cascadeId) && d.Id != project.Id)
                .OrderBy(d => d.CascadeId).ToList();

            UnitWork.Update<Project>(b => b.Id == project.Id, b => new Project
            {
                Name = project.Name,
                UpdateTime = DateTime.Now,
                IsAble = project.IsAble,
                Sort = project.Sort,
                Explain = project.Explain,
                ParentId = project.ParentId,
                ParentName = project.ParentName,
                CascadeId = project.CascadeId
            });
            //更新子部门的CascadeId
            foreach (var proj in projects)
            {
                ChangeModuleCascade(proj);
                UnitWork.Update<Project>(p => p.Id == proj.Id, dd => new Project()
                {
                    CascadeId = proj.CascadeId
                });
            }
            UnitWork.Save();
        }

        public void Delete(string projectIds)
        {
            //var listProj = Repository.Find(w => projectIds.Contains(w.Id) && !w.ParentId.Contains(",")).ToList();
            //foreach (var ld in listProj)
            //{
            //    foreach (var casc in ld.CascadeId.Split(','))
            //    {
            //        UnitWork.Update<Project>(d => d.CascadeId.Contains(casc), d => new Project()
            //        {
            //            IsDel = true
            //        });
            //    }
            //}
            //UnitWork.Save();
            UnitWork.Delete<Project>(b => projectIds.Contains(b.Id));
            UnitWork.Save();
        }

        public List<Project> Load(QueryProjectListReq queryProject)
        {
            //var result = from d in UnitWork.Find<Button>(null) select d;
            var listProject = UnitWork.SqlQuery<Project>("select Id, ProjectId,Name,ParentId,ParentName, CascadeId, Label, Explain, CreateTime, " +
                "UpdateTime, Sort,IsAble,IsDel From Project order by Sort desc,CreateTime asc", new object[] { }).ToList();

            foreach (var p in listProject)
            {
                var ListWorkflow = UnitWork.SqlQuery<WorkFlow>(
                    "select wf.* from ProjectWorkflow pw left join Workflow wf on pw.WorkflowId = wf.Id where pw.ProjectId = @ProjectId",
                    new object[]
                    {
                        new SqlParameter()
                        {
                            ParameterName = "@ProjectId",
                            Value = p.Id,
                            DbType = DbType.String
                        }
                    }).ToList();
                p.WorkFlowIds = string.Join(",", ListWorkflow.Select(s => s.Id).ToList());
                p.WorkFlowNames = string.Join(",",ListWorkflow.Select(s => s.Name).ToList());
                p.WorkFlows = ListWorkflow;
            }
            return BindNew(listProject, null);
        }

        public void SetProjectWorkflow(QueryProjectWorkflow projectWorkFlows)
        {
            UnitWork.Delete<ProjectWorkflow>(d => projectWorkFlows.ProjectWorkflows.Any(pw => pw.ProjectId.Contains(d.ProjectId)));
            foreach (var pw in projectWorkFlows.ProjectWorkflows)
            {
                if (!string.IsNullOrEmpty(pw.WorkflowId))
                {
                    UnitWork.Add(new ProjectWorkflow()
                    {
                        ProjectId = pw.ProjectId,
                        WorkflowId = pw.WorkflowId
                    });
                }
            }
            UnitWork.Save();
        }
    }
}

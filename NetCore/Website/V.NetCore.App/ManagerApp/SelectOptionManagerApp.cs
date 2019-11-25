using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V.NetCore.App.Interface;
using V.NetCore.App.Request;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Interface;
using V.NetCore.Tools;

namespace V.NetCore.App.ManagerApp
{
    public class SelectOptionManagerApp : BaseApp<SelectOption>
    {
        private readonly IAuth _auth;
        public SelectOptionManagerApp(IUnitWork unitWork, IRepository<SelectOption> repository, IAuth auth) : base(unitWork, repository)
        {
            _auth = auth;
        }


        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="selectOption"></param>
        /// <returns></returns>
        public string Add(SelectOption selectOption)
        {
            selectOption.Sort = selectOption.Sort.ToInt(1000);
            selectOption.IsHide = selectOption.IsHide.ToBool(false);
            selectOption.IsDel = selectOption.IsDel.ToBool(false);
            selectOption.ParentId = string.IsNullOrEmpty(selectOption.ParentId) ? null : selectOption.ParentId;
            ChangeModuleCascade(selectOption);
            UnitWork.Add(selectOption);
            UnitWork.Save();
            return selectOption.Id;
        }

        /// <summary>
        /// 删除选项及子类
        /// </summary>
        /// <param name="ssIds"></param>
        /// <returns></returns>
        public void Delete(string ssIds)
        {
            //var listSelectOptions = Repository.Find(w => ssIds.Contains(w.Id) && !w.ParentId.Contains(",")).ToList();
            var listSelectOptions = Repository.Find(w => ssIds.Contains(w.Id)).ToList();
            foreach (var ld in listSelectOptions)
            {
                foreach (var casc in ld.CascadeId.Split(','))
                {
                    UnitWork.Update<SelectOption>(d => d.CascadeId.Contains(casc), d => new SelectOption()
                    {
                        IsDel = true
                    });
                }
            }
            UnitWork.Save();
        }

        /// <summary>
        /// 修改选项
        /// </summary>
        /// <param name="selectOption"></param>
        public void Update(SelectOption selectOption)
        {
            selectOption.ParentId = string.IsNullOrEmpty(selectOption.ParentId) ? null : selectOption.ParentId;
            ChangeModuleCascade(selectOption);
            //获取旧的的CascadeId
            var cascadeId = Repository.FindSingle(s => s.Id == selectOption.Id).CascadeId;
            //根据CascadeId查询子选项
            var selectOptions = Repository.Find(s => s.CascadeId.Contains(cascadeId) && s.Id != selectOption.Id)
                .OrderBy(d => d.CascadeId).ToList();

            //
            UnitWork.Update<SelectOption>(s => s.Id == selectOption.Id, s => new SelectOption()
            {
                IsHide = selectOption.IsHide,
                IsDel = selectOption.IsDel,
                Sort = selectOption.Sort,
                Name = selectOption.Name,
                ParentId = selectOption.ParentId,
                ParentName = selectOption.ParentName,
                CascadeId = selectOption.CascadeId
            });
            //更新子选项的CascadeId
            foreach (var ss in selectOptions)
            {
                ChangeModuleCascade(ss);
                UnitWork.Update<SelectOption>(s => s.Id == ss.Id, dd => new SelectOption()
                {
                    CascadeId = ss.CascadeId
                });
            }
            UnitWork.Save();
        }
        
        /// <summary>
        /// 加载所有选项
        /// </summary>
        /// <returns></returns>
        public List<SelectOption> Load()//QuerySelectOptionListReq request
        {
            var sWhere = new SqlParameter()
            {
                ParameterName = "@where",
                Value = "",
                DbType = DbType.String
            };
            var listsSelectOptions = UnitWork.SqlQuery<SelectOption>(
                "select Id, [Text] as Name,[Text] as Label, ParentId, ParentName, CascadeId, [Code], [Text], [Value], IsHide,isDel, Sort,CreateTime,Explain from SelectOptions where IsHide = 0 and isDel=0 order by Sort desc"
                , new object[] { sWhere }).ToList();
            return BindNew(listsSelectOptions, null);
            
            //var loginUser = _auth.GetCurrentUser();
            ////where条件
            //var sSqlWhere = "IsHide = 0 and IsDel = 0";
            ////模糊查询
            //if (!string.IsNullOrEmpty(request.KeyWorld))
            //{
            //    sSqlWhere = sSqlWhere + " and Name like '%" + request.KeyWorld + "%' ";
            //}
            ////获取分页存储过程的参数
            //var sqlPageParams = GetQueryPageParams("SelectOptions", request.PrimaryKey,
            //    "Id, Name,Name as Label, ParentId, ParentName, CascadeId, [Code], [Text], [Value], IsHide,isDel, Sort",
            //    request.PageSize, request.PageIndex, sSqlWhere,
            //    "", request.PrimaryKey + " " + request.Order);
            ////获取数据
            //var listsSelectOptions = UnitWork.SqlQuery<SelectOption>(
            //    "proc_Paging @TableNames,@PrimaryKey,@Columns,@PageSize,@PageIndex,@sWhere,@Group,@Order,@totalCount output"
            //    , sqlPageParams).ToList();
            //return BindNew(listsSelectOptions, null);
        }
        
    }
}

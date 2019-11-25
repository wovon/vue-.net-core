using V.NetCore.Repository.Core;
using V.NetCore.Repository.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using V.NetCore.Repository.Domain;
using System.Text;
using V.NetCore.Tools;

namespace V.NetCore.App
{
    /// <summary>
    /// 业务层基类，UnitWork用于事务操作，Repository用于普通的数据库操作
    /// <para>如用户管理：Class UserManagerApp:BaseApp</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseApp<T> where T : Entity
    {
        /// <summary>
        /// 用于事务操作
        /// </summary>
        /// <value>The unit work.</value>
        protected IUnitWork UnitWork;

        public BaseApp(IUnitWork unitWork, IRepository<T> repository)
        {
            UnitWork = unitWork;
            Repository = repository;
        }

        /// <summary>
        /// 用于普通的数据库操作
        /// </summary>
        /// <value>The repository.</value>
        protected IRepository<T> Repository;

        /// <summary>
        /// 按id批量删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(string[] ids)
        {
            Repository.Delete(u => ids.Contains(u.Id));
        }

        /// <summary>
        /// 通过ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(string id)
        {
            return Repository.FindSingle(u => u.Id == id);
        }

        /// <summary>
        /// 分页参数
        /// </summary>
        /// <param name="sTableNames"></param>
        /// <param name="sPrimaryKey"></param>
        /// <param name="sColumns"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="sWhere"></param>
        /// <param name="sGroup"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        public object[] GetQueryPageParams(string sTableNames,string sPrimaryKey,string sColumns,int iPageSize,int iPageIndex,string sWhere,string sGroup,string sOrder)
        {

            var tableNames = new SqlParameter()
            {
                ParameterName = "@TableNames",
                Value = sTableNames,
                DbType = DbType.String
            };
            var primaryKey = new SqlParameter()
            {
                ParameterName = "@PrimaryKey",
                Value = sPrimaryKey,
                DbType = DbType.String
            };
            var columns = new SqlParameter()
            {
                ParameterName = "@Columns",
                Value = sColumns,
                DbType = DbType.String
            };
            var pageSize = new SqlParameter()
            {
                ParameterName = "@PageSize",
                Value = iPageSize,
                DbType = DbType.Int32
            };
            var pageIndex = new SqlParameter()
            {
                ParameterName = "@PageIndex",
                Value = iPageIndex-1,
                DbType = DbType.Int32
            };
            var where = new SqlParameter()
            {
                ParameterName = "@sWhere",
                Value = sWhere,
                DbType = DbType.String
            };
            var group = new SqlParameter()
            {
                ParameterName = "@Group",
                Value = sGroup,
                DbType = DbType.String
            };
            var order = new SqlParameter()
            {
                ParameterName = "@Order",
                Value = sOrder,
                DbType = DbType.String
            };
            var totalCount = new SqlParameter("@totalCount", DbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            
            return new []
            {
                tableNames,primaryKey,columns,pageSize,pageIndex,where,group,order,totalCount
            };

        }

        /// <summary>
        /// 如果一个类有层级结构（树状），则修改该节点时，要修改该节点的所有子节点
        /// //修改对象的级联ID，生成类似XXX.XXX.X.XX
        /// </summary>
        /// <typeparam name="TU">U必须是一个继承TreeEntity的结构</typeparam>
        /// <param name="entity"></param>
        public void ChangeModuleCascade<TU>(TU entity) where TU : TreeEntity
        {
            List<string> listCascadeId = new List<string>();
            List<string> listParentName = new List<string>();
            int currentCascadeId = 1;  //当前结点的级联节点最后一位
            //var sameLevels = UnitWork.Find<TU>(o => (o.ParentId == entity.ParentId) && o.Id != entity.Id);
            var sameLevels = UnitWork.Find<TU>(o => (entity.ParentId.Contains(o.ParentId) || string.IsNullOrEmpty(entity.ParentId)) && o.Id != entity.Id).ToList();
            if (!string.IsNullOrEmpty(entity.ParentId) && entity.ParentId.Contains(","))
            {
                foreach (var obj in sameLevels)
                {
                    int objCascadeId = int.Parse(obj.CascadeId.TrimEnd('.').Split('.').Last());
                    if (currentCascadeId <= objCascadeId) currentCascadeId = objCascadeId + 1;
                }
                if (!string.IsNullOrEmpty((entity.ParentId)))
                {
                    //var parent = UnitWork.FindSingle<TU>(o => o.Id == entity.ParentId);
                    var parent = UnitWork.Find<TU>(o => entity.ParentId.Contains(o.Id));
                    if (parent.Any())
                    {
                        foreach (var p in parent)
                        {
                            listCascadeId.Add(p.CascadeId + currentCascadeId + ".");
                            listParentName.Add(p.Name);
                        }
                    }
                    else
                    {
                        throw new Exception("未能找到该组织的父节点信息");
                    }
                }
                else
                {
                    listCascadeId.Add(".0." + currentCascadeId + ".");
                    listParentName.Add("根节点");
                }
            }
            else
            {
                foreach (var obj in sameLevels)
                {
                    int objCascadeId = int.Parse(obj.CascadeId.TrimEnd('.').Split('.').Last());
                    if (currentCascadeId <= objCascadeId) currentCascadeId = objCascadeId + 1;
                }
                if (!string.IsNullOrEmpty((entity.ParentId)))
                {
                    //var parent = UnitWork.FindSingle<TU>(o => o.Id == entity.ParentId);
                    var parent = UnitWork.FindSingle<TU>(o => entity.ParentId.Contains(o.Id));
                    if (parent != null)
                    {
                        listCascadeId.Add(parent.CascadeId + currentCascadeId + ".");
                        listParentName.Add(parent.Name);
                    }
                    else
                    {
                        throw new Exception("未能找到该组织的父节点信息");
                    }
                }
                else
                {
                    listCascadeId.Add(".0." + currentCascadeId + ".");
                    listParentName.Add("根节点");
                }
            }
            entity.CascadeId = string.Join(',',listCascadeId);
            entity.ParentName = string.Join(',', listParentName);
        }
        

        /// <summary>
        /// 递归子类
        /// </summary>
        /// <typeparam name="TD"></typeparam>
        /// <param name="listDept"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<TD> BindNew<TD>(List<TD> listDept, string parentId) where TD:TreeEntity
        {
            var resultD = new List<TD>();
            var forList = listDept.Where(w => w.ParentId == parentId).ToList();
            foreach (var item in forList)
            {
                var n = System.Activator.CreateInstance<TD>();
                if (string.IsNullOrEmpty(item.ParentId))
                {
                    n = item;
                    n.Id = item.Id;
                    n.Name = item.Name;
                    n.ParentId = item.ParentId;
                    var Childs = GetChild(listDept, n).ConvertAll(s => (object) s);
                    n.children = Childs.Count>0? Childs : null;
                    
                }
                resultD.Add(n);
            }
            return resultD;
        }
        public List<TD> GetChild<TD>(List<TD> listDept, TD d) where TD : TreeEntity
        {
            var dr = listDept.Where(w => w.ParentId != null && w.ParentId.Contains(d.Id)).ToList();
            var child = new List<TD>();
            foreach (var item in dr)
            {
                var n = System.Activator.CreateInstance<TD>();
                n = item;
                n.Id = item.Id;
                n.Name = item.Name;
                n.ParentId = item.ParentId;
                child.Add(n);
                var dr1 = listDept.Where(w => w.ParentId !=null && w.ParentId.Contains(n.Id)).ToList();
                if (dr1.Any())
                {
                    var Childs = GetChild(listDept, n).ConvertAll(s => (object)s);
                    n.children = Childs.Count > 0 ? Childs : null;
                }
            }
            return child;
        }
        

        //public List<Department> BindNew(List<Department> listDept,string parentId)
        //{
        //    List<Department> resultD = new List<Department>();

        //    foreach (var item in listDept.Where(w => w.ParentId == parentId).ToList())
        //    {
        //        var n = item;
        //        if (string.IsNullOrEmpty(item.ParentId))
        //        {
        //            n.Id = item.Id;
        //            n.Name = item.Name;
        //            n.ParentId = item.ParentId;
        //            n.children = GetChild(listDept,n);
        //        }
        //        resultD.Add(n);
        //    }
        //    return resultD;
        //}
        //public List<Department> GetChild(List<Department> listDept, Department node)
        //{
        //    var dr = listDept.Where(w => w.ParentId == node.Id).ToList();
        //    var child = new List<Department>();
        //    foreach (var item in dr)
        //    {
        //        var n = item;
        //        n.Id = item.Id;
        //        n.Name = item.Name;
        //        n.ParentId = item.ParentId;
        //        child.Add(n);
        //        var dr1 = listDept.Where(w => w.ParentId == n.Id).ToList();
        //        if (dr1.Any())
        //        {
        //            n.children = GetChild(listDept,n);
        //        }
        //    }
        //    return child;
        //}
    }

}

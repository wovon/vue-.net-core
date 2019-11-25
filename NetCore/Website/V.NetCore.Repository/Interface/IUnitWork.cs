using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Interface
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitWork
    {
        VDbContext GetDbContext();
        T FindSingle<T>(Expression<Func<T, bool>> exp = null) where T : class;
        bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class;
        IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class;

        IQueryable<T> Find<T>(int page = 1, int limit = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null) where T : class;

        int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class;

        void Add<T>(T entity) where T : Entity;

        void BatchAdd<T>(T[] entities) where T : Entity;

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;


        /// <summary>
        /// 实现按需要只更新部分更新
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class;

        /// <summary>
        /// 批量删除
        /// </summary>
        void Delete<T>(Expression<Func<T, bool>> exp) where T : class;

        void Save();

        void ExecuteSql(string sql);

        IQueryable<T> Get<T>(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);

        IQueryable<TView> SqlQuery<TView>(string sql, params object[] parameters) where TView : class, new();

        IQueryable<T> ExecSqlReader<T>(string sql, params object[] sqlParams);


        void Update<T>(T model, params string[] updateColumns);
    }
}

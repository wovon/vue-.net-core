using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using V.NetCore.Repository.Core;
using V.NetCore.Repository.Interface;
using Z.EntityFramework.Plus;
using V.NetCore.Infrastructure;
using System.Data.SqlClient;

namespace V.NetCore.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly VDbContext _context;

        public UnitWork(VDbContext context)
        {
            _context = context;
        }
        public VDbContext GetDbContext()
        {
            return _context;
        }

        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return Filter(exp);
        }

        public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return _context.Set<T>().Any(exp);
        }

        /// <summary>
        /// 查找单个
        /// </summary>
        public T FindSingle<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="page">The pageindex.</param>
        /// <param name="limit">每页条数</param>
        /// <param name="orderby">排序，格式如："Id"/"Id descending"</param>
        public IQueryable<T> Find<T>(int page, int limit, string orderby = "", Expression<Func<T, bool>> exp = null) where T : class
        {
            if (page < 1) page = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "Id descending";
            return Filter(exp).OrderBy(orderby).Skip(limit * (page - 1)).Take(limit);
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return Filter(exp).Count();
        }

        public void Add<T>(T entity) where T : Entity
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd<T>(T[] entities) where T : Entity
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().AddRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!_context.ChangeTracker.HasChanges())
            {
                entry.State = EntityState.Unchanged;
            }

        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class
        {
            _context.Set<T>().Where(where).Update(entity);
        }

        public virtual void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        {
            _context.Set<T>().RemoveRange(Filter(exp));
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch //(DbEntityValidationException e)
            {
                //throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            }

        }

        private IQueryable<T> Filter<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        public void ExecuteSql(string sql)
        {
            _context.Database.ExecuteSqlCommand(sql);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TView> SqlQuery<TView>(string sql, params object[] parameters) where TView : class, new()
        {
            return _context.Set<TView>().FromSql(sql, parameters);
        }

        public IQueryable<T> ExecSqlReader<T>(string sql, params object[] sqlParams)
        {
            //_context.
            return null;
        }

        public void Update<T>(T model, params string[] updateColumns)
        {
            throw new NotImplementedException();
        }
    }
}

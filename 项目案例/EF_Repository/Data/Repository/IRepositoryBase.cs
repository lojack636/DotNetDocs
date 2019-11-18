using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        #region 数据对象操作

        /// <summary>
        /// 数据上下文
        /// </summary>
        DbContext Context { get; }
        /// <summary>
        /// 数据模型操作
        /// </summary>
        DbSet<T> dbSet { get; }
        /// <summary>
        /// EF事务
        /// </summary>
        DbContextTransaction Transaction { get; set; }
        /// <summary>
        /// 事务提交结果
        /// </summary>
        bool Committed { get; set; }
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        #endregion

        bool Add(T entity, bool isCommit = true);
        Task<bool> AddAsync(T entity, bool isCommit = true);
        bool AddRange(IEnumerable<T> entities, bool isCommit = true);
        Task<bool> AddRangeAsync(IEnumerable<T> entities, bool isCommit = true);

        bool Update(T entity, bool isCommit = true);
        Task<bool> UpdateAsync(T entity, bool isCommit = true);
        bool Update(IEnumerable<T> entities, bool isCommit = true);
        Task<bool> UpdateAsync(IEnumerable<T> entities, bool isCommit = true);

        bool Del(T entity, bool isCommit = true);
        Task<bool> DelAsync(T entity, bool isCommit = true);
        bool Del(Expression<Func<T, bool>> filter, bool isCommit = true);
        Task<bool> DelAsync(Expression<Func<T, bool>> filter, bool isCommit = true);

        T GetEntity(Expression<Func<T, bool>> filter);
        T Find(object id);
        Task<T> FindAsync(object id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IEnumerable<T> GetAll(ref int pageIndex, ref int pageSize, out int pageCount, out int total, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        IEnumerable<T> SqlQuery(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}

using LinqKit;
using SysEntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysRepository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly SysDbContext _dbContext;
        public Repository(SysDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 固定公用帮助，含事务

        /// <summary>
        /// 数据上下文
        /// </summary>
        public DbContext Context
        {
            get
            {
                _dbContext.Configuration.ValidateOnSaveEnabled = false;
                return _dbContext;
            }
        }
        /// <summary>
        /// 公用泛型处理属性
        /// 注:所有泛型操作的基础
        /// </summary>
        public DbSet<T> dbSet
        {
            get { return Context.Set<T>(); }
        }
        /// <summary>
        /// 事务
        /// </summary>
        private DbContextTransaction _transaction = null;
        /// <summary>
        /// 开始事务
        /// </summary>
        public DbContextTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = this.Context.Database.BeginTransaction();
                }
                return _transaction;
            }
            set { _transaction = value; }
        }
        /// <summary>
        /// 事务状态
        /// </summary>
        public bool Committed { get; set; }
        /// <summary>
        /// 异步锁定
        /// </summary>
        private readonly object sync = new object();
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (!Committed)
            {
                lock (sync)
                {
                    if (_transaction != null)
                    {
                        _transaction.Commit();
                    }
                }
                Committed = true;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            Committed = false;
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        #endregion

        #region Add

        public bool Add(T entity, bool isCommit = true)
        {
            dbSet.Add(entity);
            return !isCommit || Context.SaveChanges() > 0;
        }
        public async Task<bool> AddAsync(T entity, bool isCommit = true)
        {
            dbSet.Add(entity);
            if (!isCommit) return false;
            return await Context.SaveChangesAsync() > 0;
        }
        public bool AddRange(IEnumerable<T> entities, bool isCommit = true)
        {
            dbSet.AddRange(entities);
            return Context.SaveChanges() > 0;
        }
        public async Task<bool> AddRangeAsync(IEnumerable<T> entities, bool isCommit = true)
        {
            dbSet.AddRange(entities);
            if (!isCommit) return false;
            return await Context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Update

        public bool Update(T entity, bool isCommit = true)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            return !isCommit || Context.SaveChanges() > 0;
        }
        public async Task<bool> UpdateAsync(T entity, bool isCommit = true)
        {
            if (!isCommit) return false;
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            return await Context.SaveChangesAsync() > 0;
        }
        public bool Update(IEnumerable<T> entities, bool isCommit = true)
        {
            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                Context.Entry(entity).State = EntityState.Modified;
            }
            return !isCommit || Context.SaveChanges() > 0;
        }
        public async Task<bool> UpdateAsync(IEnumerable<T> entities, bool isCommit = true)
        {
            if (!isCommit) return false;
            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                Context.Entry(entity).State = EntityState.Modified;
            }
            return await Context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Delete

        public bool Del(T entity, bool isCommit = true)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return !isCommit || Context.SaveChanges() > 0;
        }
        public async Task<bool> DelAsync(T entity, bool isCommit = true)
        {
            if (!isCommit) return false;
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }
        public bool Del(Expression<Func<T, bool>> filter, bool isCommit = true)
        {
            var entities = dbSet.Where(filter);
            dbSet.RemoveRange(entities);
            return !isCommit || Context.SaveChanges() > 0;
        }
        public async Task<bool> DelAsync(Expression<Func<T, bool>> filter, bool isCommit = true)
        {
            if (!isCommit) return false;
            var entities = dbSet.Where(filter);
            dbSet.RemoveRange(entities);
            return await Context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Select

        public T GetEntity(Expression<Func<T, bool>> filter)
        {
            return dbSet.AsExpandable().Where(filter).FirstOrDefault();
        }
        public T Find(object id)
        {
            return dbSet.Find(id);
        }
        public async Task<T> FindAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> list = dbSet;
            if (filter != null)
            {
                list = dbSet.AsExpandable().Where(filter);
            }
            if (orderBy != null)
            {
                list = orderBy(dbSet);
            }
            return list.ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> list = dbSet;
                if (filter != null)
                {
                    list = dbSet.AsExpandable().Where(filter);
                }
                if (orderBy != null)
                {
                    list = orderBy(dbSet);
                }
                return list.ToList();
            });
        }

        public IEnumerable<T> GetAll(ref int pageIndex, ref int pageSize, out int pageCount, out int total, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> list = dbSet;
            if (filter != null)
            {
                list = list.AsExpandable().Where(filter);
            }
            if (orderBy != null)
            {
                list = orderBy(list);
            }
            if (pageIndex > 0 && pageSize > 0)
            {
                total = dbSet.Count();
                pageCount = total / pageSize + (total % pageSize == 0 ? 0 : 1);
                if (pageIndex > pageCount)
                {
                    pageIndex = pageCount;
                }
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                total = list.Count();
                pageCount = 0;
            }
            return list.ToList();
        }

        #endregion

        #region SQL执行

        public IEnumerable<T> SqlQuery(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(sql, parameters);
        }
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }
        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        #endregion
    }
}

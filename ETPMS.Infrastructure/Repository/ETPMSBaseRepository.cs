using EntityFramework.Extensions;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ETPMS.Infrastructure.Repository
{
    public class ETPMSBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public ETPMSDbSession DbSession { get; private set; }
        private IDbSet<TEntity> _dbSet { get; set; }

        public ETPMSBaseRepository(ETPMSDbSession dbSession)
        {
            this.DbSession = dbSession;
            this._dbSet = dbSession.GetDbSet<TEntity>();
        }

        public TEntity Add(TEntity entity, bool isSave = true)
        {
            var item = this._dbSet.Add(entity);
            if (isSave) this.DbSession.Commite();
            return item;
        }

        public IEnumerable Add(IList<TEntity> entities, bool isSave = true)
        {
            var items = new List<TEntity>();
            entities.ForEach(k=> items.Add( this._dbSet.Add(k)));
            if (isSave) this.DbSession.Commite();
            return items;
        }

        public void Update(TEntity entity, bool isSave = true)
        {
            this.DbSession.Modify<TEntity>(entity);
            if (isSave) this.DbSession.Commite();
        }

        public void Update(TEntity originalEntity, TEntity newEntity, bool isSave = true)
        {
            this.DbSession.Modify(originalEntity, newEntity);
            if (isSave) this.DbSession.Commite();
        }

        public TEntity Delete(TEntity entity, bool isSave = true)
        {
            this.DbSession.Attach<TEntity>(entity);
            var item = this._dbSet.Remove(entity);
            if (isSave) this.DbSession.Commite();
            return item;
        }

        public int Delete(Expression<Func<TEntity, bool>> whereExpression, bool isSave = true)
        {
            var count = this._dbSet.Where(whereExpression).Delete();
            if (isSave) this.DbSession.Commite();
            return count;
        }

        public void TrackItem(TEntity entity)
        {
            this.DbSession.Attach<TEntity>(entity);
        }

        public TEntity GetById<T>(T id)
        {
            return this._dbSet.Find(id);
        }

        public TEntity GetFirstOrDefualt(Expression<Func<TEntity, bool>> whereExpression)
        {
            return this._dbSet.FirstOrDefault(whereExpression);
        }

        public TEntity GetFirstOrDefualt<TProperty>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true)
        {
            if (ascending)
                return this._dbSet.OrderBy(orderByExpression).FirstOrDefault(whereExpression);
            else
                return this._dbSet.OrderByDescending(orderByExpression).FirstOrDefault(whereExpression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._dbSet;
        }

        public IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> whereExpression)
        {
            return this._dbSet.Where(whereExpression);
        }

        public IEnumerable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> whereExpression, bool hasManyWhereCondition)
        {
            return this._dbSet.Wheres(whereExpression);
        }

        public IQueryable<TEntity> GetPaged<TProperty>(out int totalCount, int pageIndex, int pageCount, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true)
        {
            totalCount = this._dbSet.Count();
            if (ascending)
                return this._dbSet.OrderBy(orderByExpression).Take(pageCount * (pageIndex - 1)).Skip(pageIndex);
            else
                return this._dbSet.OrderByDescending(orderByExpression).Take(pageCount * (pageIndex - 1)).Skip(pageIndex);
        }

        public IEnumerable<TEntity> GetPaged(out int totalCount, int pageIndex, int pageCount, string orderText, bool ascending = true)
        {
            totalCount = this._dbSet.Count();
            if (string.IsNullOrEmpty(orderText))
                return this._dbSet.Skip(pageCount * (pageIndex - 1)).Take(pageCount);
            else
                return this._dbSet.DynamicOrderBy(orderText, ascending).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
        }

        public IQueryable<TEntity> GetPaged<TProperty>(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageCount, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true)
        {
            totalCount = this._dbSet.Where(whereExpression).Count();
            if (ascending)
                return this._dbSet.Where(whereExpression).OrderBy(orderByExpression).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
            else
                return this._dbSet.Where(whereExpression).OrderByDescending(orderByExpression).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
        }

        public IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageCount,  string orderText, bool ascending = true)
        {
            totalCount = this._dbSet.Where(whereExpression).Count();
            if (string.IsNullOrEmpty(orderText))
                return this._dbSet.Where(whereExpression).Skip(pageCount * (pageIndex-1)).Take(pageCount);
            else
                return this._dbSet.Where(whereExpression).DynamicOrderBy(orderText, ascending).Skip(pageCount * (pageIndex-1)).Take(pageCount);
        }

        public IEnumerable<TEntity> GetPaged<TProperty>(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageSize, Func<TEntity, TProperty> orderByExpression, bool ascending = true, bool hasManyWhereCondition = false)
        {
            totalCount = this._dbSet.Where(whereExpression).Count();
            if (ascending)
                return this._dbSet.Wheres(whereExpression).OrderBy(orderByExpression).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            else
                return this._dbSet.Wheres(whereExpression).OrderByDescending(orderByExpression).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        public IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageCount,  string orderText, bool ascending = true, bool hasManyCondition = false)
        {
            totalCount = this._dbSet.Where(whereExpression).Count();
            if (string.IsNullOrEmpty(orderText))
                return this._dbSet.Wheres(whereExpression).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
            else
                return this._dbSet.Wheres(whereExpression).DynamicOrderBy(orderText, ascending).Skip(pageCount * (pageIndex - 1)).Take(pageCount);
        }

        public void Dispose()
        {
            if (this.DbSession != null)
                this.DbSession.Dispose();
        }
    }
}

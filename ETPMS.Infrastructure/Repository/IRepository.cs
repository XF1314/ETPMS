using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETPMS.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        ETPMSDbSession DbSession { get; }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        TEntity Add(TEntity entity,bool isSave = true);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities">实体s</param>
        /// <param name="isSave">是否立即保存</param>
        IEnumerable Add(IList<TEntity> entities,bool isSave = true);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        void Update(TEntity entity,bool isSave = true);

        /// <summary>
        /// 实体部分更新
        /// </summary>
        /// <param name="originalEntity">原实体</param>
        /// <param name="newEntity">新实体</param>
        /// <param name="isSave">是否立即保存</param>
        void Update(TEntity originalEntity, TEntity newEntity,bool isSave = true);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        TEntity Delete(TEntity entity,bool isSave = true);

        /// <summary>
        /// 删除满足条件的实体
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        /// <param name="isSave">是否立即保存</param>
        int Delete(Expression<Func<TEntity, bool>> whereExpression, bool isSave = true);

        /// <summary>
        /// 设置实体为UnChanged状态
        /// </summary>
        /// <param name="entity">实体</param>
        void TrackItem(TEntity entity);

        /// <summary>
        /// 根据Id查找实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        TEntity GetById<T>(T id);
        
        /// <summary>
        /// 获取查询结果的第一个或是默认值
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        TEntity GetFirstOrDefualt(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 获取查询结果的第一个或是默认值
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="ascending">排序</param>
        /// <returns></returns>
        TEntity GetFirstOrDefualt<TProperty>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取所有满足条件的实体
        /// </summary>
        /// <param name="whereExpression">where条件</param>
        /// <returns></returns>
        IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 获取所有满足条件的实体
        /// </summary>
        /// <param name="whereExpression">表达式</param>
        /// <param name="hasManyWhereCondition">true</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> whereExpression, bool hasManyWhereCondition);

        /// <summary>
        /// 获取排序分页后的实体集
        /// </summary>
        /// <typeparam name="TProperty">排序的属性</typeparam>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        IQueryable<TEntity> GetPaged<TProperty>(out int totalCount, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true);

        /// <summary>
        /// 获取排序分页后的实体集
        /// </summary>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderText">排序的字段名称</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(out int totalCount, int pageIndex, int pageSize, string orderText, bool ascending = true);

        /// <summary>
        /// 根据where条件获取排序分页后的实体集
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="whereExpression">where条件</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        IQueryable<TEntity> GetPaged<TProperty>(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending = true);

        /// <summary>
        ///  根据where条件获取排序分页后的实体集
        /// </summary>
        /// <param name="whereEpxression">where条件</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderText">排序的字段名称</param>
        /// <param name="ascending">排序表达式</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> whereEpxression, out int totalCount, int pageIndex, int pageSize,  string orderText, bool ascending  = true);

        /// <summary>
        /// 根据多Where条件获取排序分页后的实体集
        /// </summary>
        /// <typeparam name="TProperty">排序的属性</typeparam>
        /// <param name="whereExpression">表达式</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <param name="hasManyWhereCondition">true</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged<TProperty>(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageSize, Func<TEntity, TProperty> orderByExpression, bool ascending = true, bool hasManyWhereCondition = false);

        /// <summary>
        /// 根据多Where条件获取排序分页后的实体集
        /// </summary>
        /// <param name="whereExpression">表达式</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="orderText">排序的字段名称</param>
        /// <param name="ascending">是否升序</param>
        /// <param name="hasManyWhereCondition">true</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> whereExpression, out int totalCount, int pageIndex, int pageSize,  string orderText, bool ascending = true, bool hasManyWhereCondition = false);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETPMS.Infrastructure.Extensions
{
    public static class PredicateBuilder
    {
        /// <summary>
        /// 返回条件结果为真的表达式
        /// </summary>
        /// <typeparam name="T">查询的类（泛型）</typeparam>
        /// <returns>结果为真的表达式</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// 返回条件结果为假的表态式
        /// </summary>
        /// <typeparam name="T">查询的类（泛型）</typeparam>
        /// <returns>结果为假的表达式</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// 将两个条件以“且”的运算进行拼接
        /// </summary>
        /// <typeparam name="T">查询的类（泛型）</typeparam>
        /// <param name="first">条件表达式1</param>
        /// <param name="second">条件表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return  first.Compose(second, Expression.And);
        }

        /// <summary>
        /// 将两个条件以“或”的运算进行拼接
        /// </summary>
        /// <typeparam name="T">查询的类（泛型）</typeparam>
        /// <param name="first">条件表达式1</param>
        /// <param name="second">条件表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        /// <summary>
        /// 对表达式进行“非”运算
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>
        /// 对两个表达式进行合并重建
        /// </summary>
        /// <typeparam name="T">查询的类（泛型）</typeparam>
        /// <param name="first">条件表达式1</param>
        /// <param name="second">条件表达式2</param>
        /// <param name="merge">合并重建的方式</param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)  
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first  
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression   
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    /// <summary>
    /// 拼接Lambda时进行参数映射和重建
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}

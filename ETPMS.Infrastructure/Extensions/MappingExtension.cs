using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ETPMS.Infrastructure.Extensions
{
    public static class MappingExtension
    {
        /// <summary>
        ///  单个对象映射
        /// </summary>
        /// <typeparam name="TD">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static TD MapTo<TD>(this object source)
         where TD : class, new()
        {
            if (source == null) return default(TD);
            Mapper.Initialize(x => x.CreateMap(source.GetType(), typeof(TD)));
            return Mapper.Map<TD>(source);
        }

        /// <summary>
        /// 单个对象映射
        /// </summary>
        /// <typeparam name="TS">源对象类型</typeparam>
        /// <typeparam name="TD">目标对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static TD MapTo<TS, TD>(this TS source)
        where TS : class, new()
        where TD : class, new()
        {
            Mapper.Initialize(x => x.CreateMap<TS, TD>());
            return Mapper.Map(source, default(TD));
        }

        /// <summary>
        /// 对象集合映射
        /// </summary>
        /// <typeparam name="TS">源对象类型</typeparam>
        /// <typeparam name="TD">目标对象类型</typeparam>
        /// <param name="source">源对象s</param>
        /// <returns></returns>
        public static List<TD> MapToList<TS, TD>(this IEnumerable<TS> source)
        where TS : class, new()
        where TD : class, new()
        {
            Mapper.Initialize(x => x.CreateMap<TS, TD>());
            return Mapper.Map<List<TD>>(source);
        }

        /// <summary>
        /// 对象集合映射
        /// </summary>
        /// <typeparam name="TS">源对象类型</typeparam>
        /// <typeparam name="TD">目标对象类型</typeparam>
        /// <param name="source">源对象s</param>
        /// <returns></returns>
        public static List<TD> MapToList<TS, TD>(this IQueryable<TS> source)
        where TS : class, new()
        where TD : class, new()
        {
            Mapper.Initialize(x => x.CreateMap<TS, TD>());
            return Mapper.Map<List<TD>>(source);
        }
    }
}
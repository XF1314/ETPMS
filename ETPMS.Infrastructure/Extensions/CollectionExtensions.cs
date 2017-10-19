using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace ETPMS.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>    
        /// 将泛型集合类转换成DataTable    
        /// </summary>    
        /// <typeparam name="T">集合项类型</typeparam>    
        /// <param name="items">集合</param>    
        /// <param name="propertyNames">需要返回的列的列名</param>    
        /// <returns>数据集(表)</returns>    
        public static DataTable ToDataTable<T>(this IList<T> items, params string[] propertyNames) where  T:class 
        {
            var dataTable = new DataTable();
            propertyNames = propertyNames ?? new string[0];
            var propertyNameList = propertyNames.ToList();

            if (items != null && items.Any())
            {
               var properties = items[0].GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (propertyNameList.Count == 0)
                    {
                        dataTable.Columns.Add(property.Name, property.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(property.Name))
                            dataTable.Columns.Add(property.Name, property.PropertyType);
                    }
                }

                foreach (var item in items)
                {
                    var tempList = new ArrayList();
                    foreach (var property in properties)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            var obj = property.GetValue(item, null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(property.Name))
                            {
                                var obj = property.GetValue(item, null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    var array = tempList.ToArray();
                    dataTable.LoadDataRow(array, true);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// IQueryable转DataTable
        /// </summary>
        /// <typeparam name="TEntity">泛型类T</typeparam>
        /// <param name="queryable">IQueryable</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ToDataTable<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class
        {
            var dataTable = new DataTable();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(typeof(TEntity)))
            {
                if (!(propertyDescriptor.PropertyType.IsGenericType && propertyDescriptor.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    dataTable.Columns.Add(propertyDescriptor.Name, propertyDescriptor.PropertyType);
            }
            foreach (var item in queryable)
            {
                var row = dataTable.NewRow();

                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(TEntity)))
                {
                    if (!(pd.PropertyType.IsGenericType && pd.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        row[pd.Name] = pd.GetValue(item);
                }

                dataTable.Rows.Add(row);
            }
        
            return dataTable;
        }
    }
}

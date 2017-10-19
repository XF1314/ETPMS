using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ETPMS.Infrastructure.Extensions
{
    public static class DataTableExtensions
    {
        #region DataTableToDidctionary
        /// <summary>
        ///  获取某一列非重复数据
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static List<string> GetDistinctColumnValue(this DataTable dataTable, string columnName)
        {
            var columnValues = new List<string>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var enumerator = dataTable.Rows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var currentDataRow = enumerator.Current as DataRow;
                    if (currentDataRow != null && currentDataRow[columnName] != null && !columnValues.Contains(currentDataRow[columnName]))
                    {
                        columnValues.Add(currentDataRow[columnName].ToString());
                    }
                }
            }

            return columnValues;
        }

        /// <summary>
        /// 获取某两列非重复数据（返回字典一列为key,一列为value）
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="keyColumnName">key列名</param>
        /// <param name="valueColumnName">value列名</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDistinctKeyValuePair(this DataTable dataTable, string keyColumnName, string valueColumnName)
        {
            var keyValuePairs = new Dictionary<string, string>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var enumerator = dataTable.Rows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var currentDataRow = enumerator.Current as DataRow;
                    if (currentDataRow != null && currentDataRow[keyColumnName] != null && !keyValuePairs.Keys.Contains(currentDataRow[keyColumnName]))
                    {
                        keyValuePairs.Add(currentDataRow[keyColumnName].ToString(), currentDataRow[valueColumnName].ToString());
                    }
                }
            }

            return keyValuePairs;
        }

        #endregion

        #region DataTableToModel
        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="dataTable">dataTable</param>
        /// <returns></returns>
        public static List<T> ToList_V1<T>(this DataTable dataTable) where T : new()
        {
            var oblist = new List<T>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var prlist = new List<PropertyInfo>();
                var type = typeof(T);
                Array.ForEach(
                    type.GetProperties(),
                    p =>
                    {
                        if (dataTable.Columns.IndexOf(p.Name) != -1)
                        {
                            prlist.Add(p);
                        }
                    });
                foreach (DataRow row in dataTable.Rows)
                {
                    var ob = new T();
                    prlist.ForEach(
                        p =>
                        {
                            if (row[p.Name] != DBNull.Value)
                            {
                                p.SetValue(ob, row[p.Name], null);
                            }
                        });
                    oblist.Add(ob);
                }
            }

            return oblist;
        }

        /// <summary>
        /// DataTable转List(兼容int64Toint32)
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="dataTable">dataTable</param>
        /// <returns></returns>
        public static List<T> ToList_V2<T>(this DataTable dataTable) where T : class, new()
        {
            var oblist = new List<T>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var prlist = new List<PropertyInfo>();
                var type = typeof(T);
                Array.ForEach(
                    type.GetProperties(),
                    p =>
                    {
                        if (dataTable.Columns.IndexOf(p.Name) != -1)
                        {
                            prlist.Add(p);
                        }
                    });
                foreach (DataRow row in dataTable.Rows)
                {
                    var ob = new T();
                    prlist.ForEach(
                        p =>
                        {
                            if (row[p.Name] != DBNull.Value)
                            {
                                if (row[p.Name].GetType() == p.PropertyType)
                                {
                                    p.SetValue(ob, row[p.Name], null);
                                }
                                else
                                {
                                    p.SetValue(ob, Convert.ToInt32(row[p.Name]), null);
                                }

                            }
                        });
                    oblist.Add(ob);
                }
            }

            return oblist;
        }

        #endregion
    }
}

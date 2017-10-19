using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ETPMS.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 将枚举转换为SelectList
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="valueType">枚举值类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList(this Type type, ValueType valueType, string defaultValue = "")
        {
            var selectList = new List<SelectListItem>();
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                if (valueType == ValueType.Int)
                {
                    selectList.AddRange(
                        from Enum value in values
                        select new SelectListItem() { Value = (Convert.ToInt32(value)).ToString(), Text = GetDescription(value) });
                }
                else
                {
                    selectList.AddRange(
                        from Enum value in values
                        select new SelectListItem() { Value = (value).ToString(), Text = GetDescription(value) });
                }

                selectList.ForEach(p => { if (defaultValue.Equals(p.Value)) { p.Selected = true; } });//设置默认值
            }

            return selectList;
        }

        /// <summary>
        /// 将枚举转换为Key类型为Int的List(Key,Value),
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> ToIntKeyValueList(this Type type)
        {
            var list = new List<KeyValuePair<int, string>>();
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                list.AddRange(
                    from Enum value in values
                    select new KeyValuePair<int, string>(Convert.ToInt32(value), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        /// 将枚举转换为Key类型为String的List(Key,Value)
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> ToStringKeyValueList(this Type type)
        {
            var list = new List<KeyValuePair<string, string>>();
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                list.AddRange(
                    from Enum value in values
                    select new KeyValuePair<string, string>(value.ToString(), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        /// 将枚举转换为Value类型为String的List(Value,Text)
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<ValueTextPair<string, string>> ToStringValueTextList(this Type type)
        {
            var list = new List<ValueTextPair<string, string>>();
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                list.AddRange(
                    from Enum value in values
                    select new ValueTextPair<string, string>(value.ToString(), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        /// 将枚举转换为Value类型为Int的List(Value,Text)
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static List<ValueTextPair<int, string>> ToIntValueTextList(this Type type)
        {
            var list = new List<ValueTextPair<int, string>>();
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                list.AddRange(
                    from Enum value in values
                    select new ValueTextPair<int, string>(Convert.ToInt32(value), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        /// 将枚举转换为ArrayList(Key,Value)
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>ArrayList</returns>
        public static ArrayList ToArrayList(this Type type)
        {
            var arrayList = new ArrayList();

            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                foreach (Enum value in values)
                {
                    arrayList.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
                }
            }

            return arrayList;
        }

        /// <summary>
        /// 从枚举中获取Description
        /// </summary>
        /// <param name="enumName">枚举名称</param>
        /// <returns>描述内容</returns>
        public static string GetDescription(this Enum enumName)
        {
            var fieldInfo = enumName.GetType().GetField(enumName.ToString());
            var attributes = fieldInfo.GetDescriptAttr();

            return attributes != null && attributes.Length > 0 ? attributes[0].Description : enumName.ToString();
        }
        /// <summary>
        /// 获取字段Description
        /// </summary>
        public static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            return (DescriptionAttribute[])fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        }

        /// <summary>
        /// 根据Description获取枚举
        /// </summary>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举</returns>
        public static T GetEnumName<T>(string description)
        {
            var type = typeof(T);
            foreach (var fieldInfo in type.GetFields())
            {
                var descripions = fieldInfo.GetDescriptAttr();
                if (descripions != null && descripions.Length > 0)
                {
                    if (descripions[0].Description == description)
                        return (T)fieldInfo.GetValue(null);
                }
                else
                {
                    if (fieldInfo.Name == description)
                        return (T)fieldInfo.GetValue(null);
                }
            }

            throw new ArgumentException($"{description} 未能找到对应的枚举.", nameof(description));
        }

    }
}

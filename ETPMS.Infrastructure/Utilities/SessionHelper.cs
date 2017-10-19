using System.Web;

namespace ETPMS.Infrastructure.Utilities
{
    public static class SessionHelper
    {
        /// <summary>
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="key">session 名</param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="key">session 名</param>
        /// <param name="value">session 值</param>
        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session.Remove(key);
            HttpContext.Current.Session.Add(key, value);
        }
        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="key">Session对象名称</param>
        /// <param name="value">Session值</param>
        public static void Add(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
            HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="key">Session对象名称</param>
        /// <param name="values">Session值数组</param>
        public static void Adds(string key, string[] values)
        {
            HttpContext.Current.Session[key] = values;
            HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="key">Session对象名称</param>
        /// <param name="value">Session值</param>
        /// <param name="expires">调动有效期（分钟）</param>
        public static void Add(string key, string value, int expires)
        {
            HttpContext.Current.Session[key] = value;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="key">Session对象名称</param>
        /// <param name="values">Session值数组</param>
        /// <param name="expires">调动有效期（分钟）</param>
        public static void Adds(string key, string[] values, int expires)
        {
            HttpContext.Current.Session[key] = values;
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值数组</returns>
        public static string[] Gets(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Remove(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}

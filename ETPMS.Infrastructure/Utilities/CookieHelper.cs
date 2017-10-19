using System;
using System.Web;

namespace ETPMS.Infrastructure.Utilities
{
    public static class CookieHelper
    {
        /// <summary>                                                                
        /// 清除指定名称的Cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void ClearCookie(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }

            return str;
        }

        /// <summary>
        /// 添加一个Cookie（不设置过期时间，页面关闭cookie失效）
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            var cookie = new HttpCookie(cookieName, cookieValue);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            var cookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = expires
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="timeSpan">绝对过期时间 TimeSpan</param>
        public static void SetCookie(string cookieName, string cookieValue, TimeSpan timeSpan)
        {
            var cookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = DateTime.Now.Add(timeSpan)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

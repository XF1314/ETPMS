using ETPMS.Infrastructure.Configurations;
using System.Text;
using System.Web;

namespace ETPMS.Infrastructure.Utilities
{
    public static class DESEncryptWrapper
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="originalText">待解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string Decrypt(string originalText)
        {
            return DecryptByKey(originalText, ETPMSSetting.G_Salt);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="originalText">待加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Encrypt(string originalText)
        {

            return EncryptByKey(originalText, ETPMSSetting.G_Salt);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="original">待解密的字符串</param>
        /// <param name="key">加解密key</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecryptByKey(string original, string key)
        {
            if (string.IsNullOrEmpty(original))
            {
                return string.Empty;
            }

            original = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(original));
            return DESEncryptHelper.Decrypt(original, key);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original">待加密的字符串</param>
        /// <param name="key">加解密key</param>
        /// <returns>返回加密后的字符串</returns>
        public static string EncryptByKey(string original, string key)
        {
            return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(DESEncryptHelper.Encrypt(original, key)));
        }


    }
}

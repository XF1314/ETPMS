using System.Security.Cryptography;
using System.Text;

namespace ETPMS.Infrastructure.Utilities
{
    public static class MD5EncryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="originalText">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string originalText)
        {
            return MD5Encrypt(originalText, new UTF8Encoding());
        }

        /// <summary>
        /// md5加密16|32位
        /// </summary>
        /// <param name="originalText">需要加密的字符串</param>
        /// <param name="length">16|32</param>
        /// <returns></returns>
        public static string MD5Encrypt(string originalText, int length)
        {
            var cipherText = MD5Encrypt(originalText, new UTF8Encoding());
            if (length == 16)
            {
                cipherText = cipherText.Substring(8, 16);
            }
            return cipherText;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="originalText">需要加密的字符串</param>
        /// <param name="encoding">字符的编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string originalText, Encoding encoding)
        {
            if (string.IsNullOrEmpty(originalText))
            {
                return string.Empty;
            }
            var md5ServiceProvider = new MD5CryptoServiceProvider();
            var bytes = md5ServiceProvider.ComputeHash(encoding.GetBytes(originalText));
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

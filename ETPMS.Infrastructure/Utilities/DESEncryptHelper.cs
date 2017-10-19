using ETPMS.Infrastructure.Configurations;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace ETPMS.Infrastructure.Utilities
{
    public static class DESEncryptHelper
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="originalText">待加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string originalText)
        {
            return Encrypt(originalText, ETPMSSetting.G_Salt);
        }
        /// <summary> 
        /// DES加密
        /// </summary> 
        /// <param name="originalText">待加密的字符串</param> 
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Encrypt(string originalText, string key)
        {
            var desProvider = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(originalText);
            desProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            desProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="originalText">待解密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string originalText)
        {
            return Decrypt(originalText, ETPMSSetting.G_Salt);
        }
        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="originalText">待解密的字符串</param>
        /// <param name="key">密钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string originalText, string key)
        {
            var desProvider = new DESCryptoServiceProvider();
            int len;
            len = originalText.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(originalText.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            desProvider.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            desProvider.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}

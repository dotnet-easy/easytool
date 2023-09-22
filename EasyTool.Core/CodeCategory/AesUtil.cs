using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyTool.CodeCategory
{
    public static class AesUtil
    {
        /// <summary>
        /// Aes 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sk"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string sk, CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (str == null) return "";
            if (string.IsNullOrEmpty(sk)) throw new ArgumentNullException("请输入秘钥");
            encoding ??= Encoding.UTF8;
            byte[] key = encoding.GetBytes(sk).Take(8).ToArray();
            byte[] toEncrypt = encoding.GetBytes(str);
            var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = cipher;
            aes.Padding = padding;
            ICryptoTransform cTransform = aes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// Aes 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sk"></param>
        /// <returns></returns>
        public static string Decrypt(string str, string sk, CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (str == null) return "";
            if (string.IsNullOrEmpty(sk)) throw new ArgumentNullException("请输入秘钥");
            encoding ??= Encoding.UTF8;
            byte[] key = encoding.GetBytes(sk).Take(8).ToArray();
            byte[] toDecrypt = Convert.FromBase64String(str);
            var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = cipher;
            aes.Padding = padding;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            return encoding.GetString(resultArray);
        }
    }
}

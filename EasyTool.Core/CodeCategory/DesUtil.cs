using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyTool.CodeCategory
{
    public static class DesUtil
    {
        /// <summary>
        /// des 加密
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="sk">秘钥</param>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string sk, CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (!IsLegalSize(sk)) throw new ArgumentException("不合规的秘钥，请确认秘钥为8位的字符");
            encoding ??= Encoding.UTF8;
            byte[] keyBytes = encoding.GetBytes(sk).ToArray();
            byte[] toEncrypt = encoding.GetBytes(str);
            var des = DES.Create();
            des.Mode = cipher;
            des.Padding = padding;
            des.Key = keyBytes;
            des.IV = keyBytes;

            ICryptoTransform cTransform = des.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// Des 解密
        /// </summary>
        /// <param name="str">待解密字符串</param>
        /// <param name="sk">秘钥</param>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrypt(string str, string sk, CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (!IsLegalSize(sk)) throw new ArgumentException("不合规的秘钥，请确认秘钥为8位的字符");
            encoding ??= Encoding.UTF8;
            byte[] keyBytes = encoding.GetBytes(sk).ToArray();
            byte[] toDecrypt = Convert.FromBase64String(str);
            var des = DES.Create();
            des.Mode = cipher;
            des.Padding = padding;
            des.Key = keyBytes;
            des.IV = keyBytes;
            ICryptoTransform cTransform = des.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            return encoding.GetString(resultArray);
        }

        private static bool IsLegalSize(string sk)
        {
            if (!string.IsNullOrEmpty(sk) && sk.Length == 8)
                return true;

            return false;
        }
    }
}

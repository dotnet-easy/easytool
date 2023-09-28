using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (!IsLegalSize(sk)) throw new ArgumentException("不合规的秘钥，请确认秘钥为16 、24、 32位的字符");
            encoding ??= Encoding.UTF8;
            byte[] key = encoding.GetBytes(sk).ToArray();
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
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (!IsLegalSize(sk)) throw new ArgumentException("不合规的秘钥，请确认秘钥为16 、24、 32位的字符");
            encoding ??= Encoding.UTF8;
            byte[] key = encoding.GetBytes(sk).ToArray();
            byte[] toDecrypt = Convert.FromBase64String(str);
            var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = cipher;
            aes.Padding = padding;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            return encoding.GetString(resultArray);
        }

        private static bool IsLegalSize(string sk)
        {
            var legalSizes = new KeySizes(128, 256, 64);
            if (string.IsNullOrEmpty(sk)) return false;
            var size = sk.Length * 8;
            if (size >= legalSizes.MinSize && size <= legalSizes.MaxSize)
            {
                // If the number is in range, check to see if it's a legal increment above MinSize
                int delta = size - legalSizes.MinSize;

                // While it would be unusual to see KeySizes { 10, 20, 5 } and { 11, 14, 1 }, it could happen.
                // So don't return false just because this one doesn't match.
                if (delta % legalSizes.SkipSize == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

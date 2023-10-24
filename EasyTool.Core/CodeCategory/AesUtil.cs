using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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



        /*
         * AES Blowfish
         * 对称加密算法的优点是速度快，
         * 缺点是密钥管理不方便，要求共享密钥。
         * 可逆对称加密  密钥长度 16 24 32 iv长度 16 
         */

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="text">需要加密的值</param>
        /// <param name="key">加密key</param>
        /// <param name="iv">向量iv</param>
        /// <param name="cipher">默认CBC</param>
        /// <param name="padding">默认PKCS7</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns>加密后的结果</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Encrypt(string text, string key, string iv ,CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty; ;
            if (!KeyIsLegalSize(key)) 
                throw new ArgumentException("不合规的秘钥，请确认秘钥为16 、24、 32位的字符");
            if (!IvIsLegalSize(iv)) 
                throw new ArgumentException("不合规的iv，请确认iv为16位的字符");
            encoding ??= Encoding.UTF8;

            var plainTextBytes = encoding.GetBytes(text);
            var keyBytes = encoding.GetBytes(key);
            var ivBytes = encoding.GetBytes(iv);

            using var symmetricKey = Aes.Create();
            symmetricKey.Mode = cipher;
            symmetricKey.Padding = padding;
            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivBytes);
            // 加密后的输出流
            using var memoryStream = new MemoryStream();
            // 将加密后的目标流（encryptStream）与加密转换（encryptTransform）相连接
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // 将一个字节序列写入当前 CryptoStream （完成加密的过程）
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            return Convert.ToBase64String(cipherTextBytes);
        }
        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="text">需要解密的值</param>
        /// <param name="key">解密key</param>
        /// <param name="iv">向量iv</param>
        /// <param name="cipher">默认CBC</param>
        /// <param name="padding">默认PKCS7</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns>解密后的结果</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Decrypt(string text, string key, string iv, CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty; ;
            if (!KeyIsLegalSize(key))
                throw new ArgumentException("不合规的秘钥，请确认秘钥为16 、24、 32位的字符");
            if (!IvIsLegalSize(iv))
                throw new ArgumentException("不合规的iv，请确认iv为16位的字符");
            encoding ??= Encoding.UTF8;
            var cipherTextBytes = Convert.FromBase64String(text);
            var keyBytes = encoding.GetBytes(key);
            var ivBytes = encoding.GetBytes(iv);
            using var symmetricKey = Aes.Create();
            symmetricKey.Mode = cipher;
            symmetricKey.Padding = padding;
            // 用当前的 Key 属性和初始化向量 IV 创建对称解密器对象
            using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivBytes);
            // 解密后的输出流
            using var memoryStream = new MemoryStream(cipherTextBytes);
            // 解密后的输出流
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            return System.Text.Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        private static bool KeyIsLegalSize(string sk)
        {
            int keyLength = sk.Length;
            return keyLength == 16 || keyLength == 24 || keyLength == 32;
        }
        private static bool IvIsLegalSize(string iv) => iv.Length == 16;
    }
}

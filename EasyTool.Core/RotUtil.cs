using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// ROT 工具类
    /// </summary>
    public static class RotUtil
    {
        /// <summary>
        /// 将给定的字符串按照 ROT 加密算法进行加密。
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="n">偏移量</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string text, int n)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            string upperCaseText = text.ToUpper();
            return new string(upperCaseText.Select(c =>
            {
                if (!char.IsLetter(c))
                {
                    return c;
                }

                int x = c - 'A';
                int y = (x + n) % 26;
                return (char)(y + 'A');
            }).ToArray());
        }

        /// <summary>
        /// 将给定的字符串按照 ROT 加密算法进行解密。
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <param name="n">偏移量</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string text, int n)
        {
            return Encrypt(text, 26 - n);
        }
    }
}

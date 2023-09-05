using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    public class UnicodeUtil
    {

        /// <summary>
        /// 将Unicode编码的字符串转换为普通字符串。
        /// </summary>
        /// <param name="unicodeStr">Unicode编码的字符串</param>
        /// <returns>普通字符串</returns>
        public static string UnicodeToString(string unicodeStr)
        {
            StringBuilder sb = new StringBuilder();

            int len = unicodeStr.Length;
            for (int i = 0; i < len; i++)
            {
                if (unicodeStr[i] == '\\' && (i + 1) < len && unicodeStr[i + 1] == 'u')
                {
                    string hexStr = unicodeStr.Substring(i + 2, 4);
                    int code = Convert.ToInt32(hexStr, 16);
                    sb.Append((char)code);
                    i += 5;
                }
                else
                {
                    sb.Append(unicodeStr[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将普通字符串转换为Unicode编码的字符串。
        /// </summary>
        /// <param name="str">普通字符串</param>
        /// <returns>Unicode编码的字符串</returns>
        public static string StringToUnicode(string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
            {
                sb.Append("\\u");
                sb.Append(((int)c).ToString("x4"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将Unicode字符转换为普通字符。
        /// </summary>
        /// <param name="unicodeChar">Unicode字符</param>
        /// <returns>普通字符</returns>
        public static char UnicodeToChar(string unicodeChar)
        {
            int code = Convert.ToInt32(unicodeChar, 16);
            return (char)code;
        }

        /// <summary>
        /// 将普通字符转换为Unicode字符。
        /// </summary>
        /// <param name="c">普通字符</param>
        /// <returns>Unicode字符</returns>
        public static string CharToUnicode(char c)
        {
            return "\\u" + ((int)c).ToString("x4");
        }

        /// <summary>
        /// 将Unicode编码的字符数组转换为普通字符串。
        /// </summary>
        /// <param name="unicodeChars">Unicode编码的字符数组</param>
        /// <returns>普通字符串</returns>
        public static string UnicodeCharsToString(char[] unicodeChars)
        {
            StringBuilder sb = new StringBuilder();

            int len = unicodeChars.Length;
            for (int i = 0; i < len; i++)
            {
                if (i + 1 < len && unicodeChars[i] == '\\' && unicodeChars[i + 1] == 'u')
                {
                    string hexStr = new string(unicodeChars, i + 2, 4);
                    int code = Convert.ToInt32(hexStr, 16);
                    sb.Append((char)code);
                    i += 5;
                }
                else
                {
                    sb.Append(unicodeChars[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将普通字符串转换为Unicode编码的字符数组。
        /// </summary>
        /// <param name="str">普通字符串</param>
        /// <returns>Unicode编码的字符数组</returns>
        public static char[] StringToUnicodeChars(string str)
        {
            List<char> chars = new List<char>();

            foreach (char c in str)
            {
                chars.AddRange(CharToUnicode(c).ToCharArray());
            }

            return chars.ToArray();
        }

        /// <summary>
        /// 将Unicode编码的字符数组转换为普通字符串数组。
        /// </summary>
        /// <param name="unicodeChars">Unicode编码的字符数组</param>
        /// <returns>普通字符串数组</returns>
        public static string[] UnicodeCharsToStringArray(char[] unicodeChars)
        {
            List<string> strs = new List<string>();

            StringBuilder sb = new StringBuilder();

            int len = unicodeChars.Length;
            for (int i = 0; i < len; i++)
            {
                if (i + 1 < len && unicodeChars[i] == '\\' && unicodeChars[i + 1] == 'u')
                {
                    string hexStr = new string(unicodeChars, i + 2, 4);
                    int code = Convert.ToInt32(hexStr, 16);
                    sb.Append((char)code);
                    i += 5;
                }
                else if (unicodeChars[i] == '\0')
                {
                    strs.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(unicodeChars[i]);
                }
            }

            if (sb.Length > 0)
            {
                strs.Add(sb.ToString());
            }

            return strs.ToArray();
        }

        /// <summary>
        /// 将普通字符串数组转换为Unicode编码的字符数组。
        /// </summary>
        /// <param name="strs">普通字符串数组</param>
        /// <returns>Unicode编码的字符数组</returns>
        public static char[] StringArrayToUnicodeChars(string[] strs)
        {
            List<char> chars = new List<char>();

            foreach (string str in strs)
            {
                chars.AddRange(StringToUnicodeChars(str));
                chars.Add('\0');
            }

            return chars.ToArray();
        }
    }
}

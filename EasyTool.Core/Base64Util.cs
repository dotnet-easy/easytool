using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Base64 编码解码工具类
    /// </summary>
    public static class Base64Util
    {
        // Base64 字符集，共 64 个字符
        private static readonly char[] BASE64_CHARS =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

        // Base64 填充字符
        private const char BASE64_PADDING_CHAR = '=';

        /// <summary>
        /// 将给定的字节数组转换为 Base64 编码字符串。
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <returns>转换后的 Base64 编码字符串</returns>
        public static string Encode(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            int length = bytes.Length;
            if (length == 0)
            {
                return string.Empty;
            }

            char[] chars = new char[(length + 2) / 3 * 4];
            int index = 0;
            for (int i = 0; i < length; i += 3)
            {
                int val = (bytes[i] << 16) + ((i + 1 < length ? bytes[i + 1] : 0) << 8) + (i + 2 < length ? bytes[i + 2] : 0);
                chars[index++] = BASE64_CHARS[(val >> 18) & 0x3F];
                chars[index++] = BASE64_CHARS[(val >> 12) & 0x3F];
                chars[index++] = BASE64_CHARS[(val >> 6) & 0x3F];
                chars[index++] = BASE64_CHARS[val & 0x3F];
            }

            // 添加填充字符
            int paddingCount = length % 3;
            if (paddingCount > 0)
            {
                chars[chars.Length - 1] = BASE64_PADDING_CHAR;
                if (paddingCount == 1)
                {
                    chars[chars.Length - 2] = BASE64_PADDING_CHAR;
                }
            }

            return new string(chars);
        }

        /// <summary>
        /// 将给定的 Base64 编码字符串转换为字节数组。
        /// </summary>
        /// <param name="str">要转换的 Base64 编码字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String is null or empty.", nameof(str));
            }
            int length = str.Length;
            if (length % 4 != 0)
            {
                throw new ArgumentException("Invalid length of input string: " + length, nameof(str));
            }

            int paddingCount = 0;
            if (length > 0 && str[length - 1] == BASE64_PADDING_CHAR)
            {
                paddingCount++;
            }
            if (length > 1 && str[length - 2] == BASE64_PADDING_CHAR)
            {
                paddingCount++;
            }

            byte[] bytes = new byte[length / 4 * 3 - paddingCount];
            int index = 0;
            for (int i = 0; i < length; i += 4)
            {
                int val = (DecodeBase64Char(str[i]) << 18) +
                          (DecodeBase64Char(str[i + 1]) << 12) +
                          (DecodeBase64Char(str[i + 2]) << 6) +
                          DecodeBase64Char(str[i + 3]);
                bytes[index++] = (byte)(val >> 16);
                if (index < bytes.Length)
                {
                    bytes[index++] = (byte)(val >> 8);
                }
                if (index < bytes.Length)
                {
                    bytes[index++] = (byte)val;
                }
            }

            return bytes;
        }

        // 解码 Base64 字符
        private static int DecodeBase64Char(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return c - 'A';
            }
            if (c >= 'a' && c <= 'z')
            {
                return c - 'a' + 26;
            }
            if (c >= '0' && c <= '9')
            {
                return c - '0' + 52;
            }
            if (c == '+')
            {
                return 62;
            }
            if (c == '/')
            {
                return 63;
            }
            throw new ArgumentException("Invalid character in input string: " + c, nameof(c));
        }
    }
}

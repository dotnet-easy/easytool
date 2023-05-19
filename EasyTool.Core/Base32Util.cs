using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Base32 编码解码工具类
    /// </summary>
    public static class Base32Util
    {
        // Base32 字符集，共 32 个字符
        private static readonly char[] BASE32_CHARS =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

        // Base32 填充字符
        private const char BASE32_PADDING_CHAR = '=';

        /// <summary>
        /// 将给定的字节数组转换为 Base32 编码字符串。
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <returns>转换后的 Base32 编码字符串</returns>
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

            char[] chars = new char[(length + 4) / 5 * 8];
            int index = 0;
            for (int i = 0; i < length; i += 5)
            {
                int val = (bytes[i] << 24) + ((i + 1 < length ? bytes[i + 1] : 0) << 16) +
                          ((i + 2 < length ? bytes[i + 2] : 0) << 8) + ((i + 3 < length ? bytes[i + 3] : 0) << 0);
                chars[index++] = BASE32_CHARS[(val >> 35) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 30) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 25) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 20) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 15) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 10) & 0x1F];
                chars[index++] = BASE32_CHARS[(val >> 5) & 0x1F];
                chars[index++] = BASE32_CHARS[val & 0x1F];
            }

            // 添加填充字符
            int paddingCount = length % 5;
            if (paddingCount > 0)
            {
                chars[chars.Length - 1] = BASE32_PADDING_CHAR;
                if (paddingCount == 1)
                {
                    chars[chars.Length - 2] = BASE32_PADDING_CHAR;
                }
                if (paddingCount <= 2)
                {
                    chars[chars.Length - 3] = BASE32_PADDING_CHAR;
                }
                if (paddingCount <= 3)
                {
                    chars[chars.Length - 4] = BASE32_PADDING_CHAR;
                }
                if (paddingCount <= 4)
                {
                    chars[chars.Length - 5] = BASE32_PADDING_CHAR;
                }
            }

            return new string(chars);
        }

        /// <summary>
        /// 将给定的 Base32 编码字符串转换为字节数组。
        /// </summary>
        /// <param name="str">要转换的 Base32 编码字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String is null or empty.", nameof(str));
            }

            int length = str.Length;
            if (length % 8 != 0)
            {
                throw new ArgumentException("Invalid length of input string: " + length, nameof(str));
            }

            int paddingCount = 0;
            if (length > 0 && str[length - 1] == BASE32_PADDING_CHAR)
            {
                paddingCount++;
            }
            if (length > 1 && str[length - 2] == BASE32_PADDING_CHAR)
            {
                paddingCount++;
            }
            if (length > 3 && str[length - 3] == BASE32_PADDING_CHAR)
            {
                paddingCount++;
            }
            if (length > 4 && str[length - 4] == BASE32_PADDING_CHAR)
            {
                paddingCount++;
            }
            if (length > 6 && str[length - 6] == BASE32_PADDING_CHAR)
            {
                paddingCount++;
            }

            byte[] bytes = new byte[length / 8 * 5 - paddingCount];
            int index = 0;
            for (int i = 0; i < length; i += 8)
            {
                int val = (DecodeBase32Char(str[i]) << 35) +
                          (DecodeBase32Char(str[i + 1]) << 30) +
                          (DecodeBase32Char(str[i + 2]) << 25) +
                          (DecodeBase32Char(str[i + 3]) << 20) +
                          (DecodeBase32Char(str[i + 4]) << 15) +
                          (DecodeBase32Char(str[i + 5]) << 10) +
                          (DecodeBase32Char(str[i + 6]) << 5) +
                          DecodeBase32Char(str[i + 7]);
                bytes[index++] = (byte)(val >> 24);
                if (index < bytes.Length)
                {
                    bytes[index++] = (byte)(val >> 16);
                }
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

        // 解码 Base32 字符
        private static int DecodeBase32Char(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return c - 'A';
            }
            if (c >= '2' && c <= '7')
            {
                return c - '2' + 26;
            }
            throw new ArgumentException("Invalid character in input string: " + c, nameof(c));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Base62 编码解码工具类
    /// </summary>
    public static class Base62Util
    {
        // Base62 字符集，共 62 个字符
        private static readonly char[] BASE62_CHARS =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// 将给定的整数转换为 Base62 编码字符串。
        /// </summary>
        /// <param name="number">要转换的整数</param>
        /// <returns>转换后的 Base62 编码字符串</returns>
        public static string Encode(long number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be non-negative.");
            }

            if (number == 0)
            {
                return BASE62_CHARS[0].ToString();
            }

            List<char> chars = new List<char>();
            int targetBase = BASE62_CHARS.Length;
            while (number > 0)
            {
                int index = (int)(number % targetBase);
                chars.Add(BASE62_CHARS[index]);
                number = number / targetBase;
            }
            chars.Reverse();
            return new string(chars.ToArray());
        }

        /// <summary>
        /// 将给定的 Base62 编码字符串转换为整数。
        /// </summary>
        /// <param name="str">要转换的 Base62 编码字符串</param>
        /// <returns>转换后的整数</returns>
        public static long Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String is null or empty.", nameof(str));
            }

            long result = 0;
            int sourceBase = BASE62_CHARS.Length;
            long multiplier = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                int digit = Array.IndexOf(BASE62_CHARS, str[i]);
                if (digit == -1)
                {
                    throw new ArgumentException("Invalid character in string: " + str[i], nameof(str));
                }
                result += digit * multiplier;
                multiplier *= sourceBase;
            }
            return result;
        }
    }
}

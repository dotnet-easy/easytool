using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// 字符串处理工具类
    /// </summary>
    public class StrUtil
    {
        /// <summary>
        /// 移除字符串中的所有空格
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string RemoveAllSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        /// <summary>
        /// 将字符串中的指定字符替换成新的字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldChar">要替换的字符</param>
        /// <param name="newChar">新的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceChar(string str, char oldChar, char newChar)
        {
            return str.Replace(oldChar, newChar);
        }

        /// <summary>
        /// 检查字符串是否为数字
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是数字，则返回true，否则返回false</returns>
        public static bool IsNumeric(string str)
        {
            double result;
            return double.TryParse(str, out result);
        }

        /// <summary>
        /// 检查字符串是否为整数
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是整数，则返回true，否则返回false</returns>
        public static bool IsInteger(string str)
        {
            int result;
            return int.TryParse(str, out result);
        }

        /// <summary>
        /// 检查字符串是否为正整数
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是正整数，则返回true，否则返回false</returns>
        public static bool IsPositiveInt(string str)
        {
            int result;
            return int.TryParse(str, out result) && result > 0;
        }

        /// <summary>
        /// 检查字符串是否为单精度浮点型
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是单精度浮点型，则返回true，否则返回false</returns>
        public static bool IsFloat(string str)
        {
            float result;
            return float.TryParse(str, out result);
        }

        /// <summary>
        /// 检查字符串是否为正单精度浮点型
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是正单精度浮点型，则返回true，否则返回false</returns>
        public static bool IsPositiveFloat(string str)
        {
            float result;
            return float.TryParse(str, out result) && result > 0f;
        }

        /// <summary>
        /// 检查字符串是否为正双精度浮点型
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是正双精度浮点型，则返回true，否则返回false</returns>
        public static bool IsPositiveDouble(string str)
        {
            double result;
            return double.TryParse(str, out result) && result > 0.0;
        }

        /// <summary>
        /// 检查字符串是否为日期
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是日期，则返回true，否则返回false</returns>
        public static bool IsDate(string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result);
        }

        /// <summary>
        /// 字符串转换日期(年月日)，转换失败返回值返回false，输出参数返回DateTime.MinValue
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <param name="dt">转换后结果</param>
        /// <returns>如果字符串转换日期(年月日)成功，则返回true，否则返回false</returns>
        public static bool TransferDate(string str, out DateTime dt)
        {
            // 日期格式
            List<string> dateFormats = new List<string> { "yyyy-MM-dd", "yyyyMMdd", "yyyy/MM/dd", "d-MMM-yyyy" };

            dt = DateTime.MinValue;
            int num = 0;
            bool flag = DateTime.TryParse(str, out dt);
            if (!flag)
            {
                do
                {
                    flag = DateTime.TryParseExact(str, dateFormats[num++], CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                }
                while (!flag && num < dateFormats.Count);
            }
            return flag;
        }

        /// <summary>
        /// 字符串转换(年月日 时分秒)，转换失败返回值返回false，输出参数返回DateTime.MinValue
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <param name="dt">转换后结果</param>
        /// <returns>如果字符串转换时间(年月日 时分秒)成功，则返回true，否则返回false</returns>
        public static bool TransferTime(string str, out DateTime dt)
        {
            //  时间格式
            List<string> dateTimeFormats = new List<string> { "yyyy-MM-dd HH:mm:ss", "yyyyMMdd HHmmss", "yyyy/MM/dd HH:mm:ss" };

            dt = DateTime.MinValue;
            int num = 0;
            bool flag = DateTime.TryParse(str, out dt);
            if (!flag)
            {
                do
                {
                    flag = DateTime.TryParseExact(str, dateTimeFormats[num++], CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                }
                while (!flag && num < dateTimeFormats.Count);
            }
            return flag;
        }

        /// <summary>
        /// 获取字符串的字节数组
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>字符串的字节数组</returns>
        public static byte[] GetBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes">要处理的字节数组</param>
        /// <returns>字节数组转换后的字符串</returns>
        public static string GetString(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 将字符串转换为大写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ToUpperCase(string str)
        {
            return str.ToUpper();
        }

        /// <summary>
        /// 将字符串转换为小写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ToLowerCase(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// 检查字符串是否为空或null
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是空或null，则返回true，否则返回false</returns>
        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否为空或仅由空格组成
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是空或仅由空格组成，则返回true，否则返回false</returns>
        public static bool IsNullOrWhiteSpace(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 截取字符串的指定部分
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="startIndex">起始位置（从0开始）</param>
        /// <param name="length">要截取的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string str, int startIndex, int length)
        {
            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 将字符串转换为驼峰命名法
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToCamelCase(string str)
        {
            string[] words = str.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    result += words[i].ToLower();
                }
                else
                {
                    result += words[i].Substring(0, 1).ToUpper() + words[i].Substring(1).ToLower();
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为帕斯卡命名法（大驼峰命名法）
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToPascalCase(string str)
        {
            string[] words = str.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            for (int i = 0; i < words.Length; i++)
            {
                result += words[i].Substring(0, 1).ToUpper() + words[i].Substring(1).ToLower();
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为下划线命名法
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToSnakeCase(string str)
        {
            string[] words = str.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    result += words[i].ToLower();
                }
                else
                {
                    result += "_" + words[i].ToLower();
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为连字符命名法（短横线命名法）
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToKebabCase(string str)
        {
            string[] words = str.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    result += words[i].ToLower();
                }
                else
                {
                    result += "-" + words[i].ToLower();
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串中的 HTML 标记去除
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>去除 HTML 标记后的字符串</returns>
        public static string StripHtml(string str)
        {
            return Regex.Replace(str, "<.*?>", "");
        }

        /// <summary>
        /// 比较两个字符串是否相等，忽略大小写和空格
        /// </summary>
        /// <param name="str1">第一个字符串</param>
        /// <param name="str2">第二个字符串</param>
        /// <returns>如果相等，则返回true，否则返回false</returns>
        public static bool EqualsIgnoreCaseAndWhiteSpace(string str1, string str2)
        {
            return string.Equals(RemoveAllSpaces(str1), RemoveAllSpaces(str2), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 在字符串的左侧填充指定字符，使字符串达到指定长度
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="paddingChar">填充字符</param>
        /// <returns>处理后的字符串</returns>
        public static string PadLeft(string str, int length, char paddingChar)
        {
            return str.PadLeft(length, paddingChar);
        }

        /// <summary>
        /// 在字符串的右侧填充指定字符，使字符串达到指定长度
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="paddingChar">填充字符</param>
        /// <returns>处理后的字符串</returns>
        public static string PadRight(string str, int length, char paddingChar)
        {
            return str.PadRight(length, paddingChar);
        }

        /// <summary>
        /// 将字符串中的某些字符替换成指定的字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="chars">要替换的字符数组</param>
        /// <param name="newChar">新的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceChars(string str, char[] chars, char newChar)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                str = str.Replace(chars[i], newChar);
            }
            return str;
        }

        /// <summary>
        /// 将字符串中的某些子字符串替换成指定的子字符串
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldValues">要替换的子字符串数组</param>
        /// <param name="newValue">新的子字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceStrings(string str, string[] oldValues, string newValue)
        {
            for (int i = 0; i < oldValues.Length; i++)
            {
                str = str.Replace(oldValues[i], newValue);
            }
            return str;
        }

        /// <summary>
        /// 将字符串中的某些子字符串替换成指定的子字符串，忽略大小写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="oldValues">要替换的子字符串数组</param>
        /// <param name="newValue">新的子字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceStringsIgnoreCase(string str, string[] oldValues, string newValue)
        {
            for (int i = 0; i < oldValues.Length; i++)
            {
                str = Regex.Replace(str, oldValues[i], newValue, RegexOptions.IgnoreCase);
            }
            return str;
        }

        /// <summary>
        /// 将字符串转换为 Title Case 格式，即每个单词的首字母大写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ToTitleCase(string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// 将字符串中的首字母大写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ToFirstLetterUpperCase(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }

        /// <summary>
        /// 将字符串中的首字母小写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ToFirstLetterLowerCase(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }
    }
}

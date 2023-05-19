using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    public class StrSplitter
    {
        /// <summary>
        /// 使用指定的分隔符将输入字符串分割成字符串数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="separator">分隔符字符串</param>
        /// <param name="options">分割选项</param>
        /// <returns>包含分割结果的字符串数组</returns>
        public static string[] Split(string input, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return input.Split(new string[] { separator }, options);
        }

        /// <summary>
        /// 使用指定的分隔符将输入字符串分割成整数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="separator">分隔符字符串</param>
        /// <param name="options">分割选项</param>
        /// <returns>包含分割结果的整数数组</returns>
        public static int[] SplitToIntArray(string input, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] stringArray = input.Split(new string[] { separator }, options);
            return Array.ConvertAll(stringArray, int.Parse);
        }

        /// <summary>
        /// 使用指定的分隔符将输入字符串分割成双精度浮点数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="separator">分隔符字符串</param>
        /// <param name="options">分割选项</param>
        /// <returns>包含分割结果的双精度浮点数数组</returns>
        public static double[] SplitToDoubleArray(string input, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] stringArray = input.Split(new string[] { separator }, options);
            return Array.ConvertAll(stringArray, double.Parse);
        }

        /// <summary>
        /// 使用指定的分隔符将输入字符串分割成指定类型的数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="separator">分隔符字符串</param>
        /// <param name="converter">类型转换函数</param>
        /// <param name="options">分割选项</param>
        /// <returns>包含分割结果的指定类型的数组</returns>
        public static T[] SplitToArray<T>(string input, string separator, Converter<string, T> converter, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] stringArray = input.Split(new string[] { separator }, options);
            return Array.ConvertAll<string,T>(stringArray, converter);
        }

        /// <summary>
        /// 使用指定的分隔符将输入字符串分割成二维字符串数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="rowSeparator">行分隔符字符串</param>
        /// <param name="colSeparator">列分隔符字符串</param>
        /// <param name="options">分割选项</param>
        /// <returns>包含分割结果的二维字符串数组</returns>
        public static string[,] SplitTo2DArray(string input, string rowSeparator, string colSeparator, StringSplitOptions options = StringSplitOptions.None)
        {
            string[] rows = input.Split(new string[] { rowSeparator }, options);
            int rowCount = rows.Length;
            int colCount = rows[0].Split(new string[] { colSeparator }, options).Length;
            string[,] result = new string[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                string[] cols = rows[i].Split(new string[] { colSeparator }, options);
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = cols[j];
                }
            }

            return result;
        }

        /// <summary>
        /// 使用指定的正则表达式将输入字符串分割成字符串数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>包含分割结果的字符串数组</returns>
        public static string[] SplitByRegex(string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }

        /// <summary>
        /// 使用指定的正则表达式将输入字符串分割成整数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>包含分割结果的整数数组</returns>
        public static int[] SplitToIntArrayByRegex(string input, string pattern)
        {
            string[] stringArray = SplitByRegex(input, pattern);
            return Array.ConvertAll(stringArray, int.Parse);
        }

        /// <summary>
        /// 使用指定的正则表达式将输入字符串分割成双精度浮点数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>包含分割结果的双精度浮点数数组</returns>
        public static double[] SplitToDoubleArrayByRegex(string input, string pattern)
        {
            string[] stringArray = SplitByRegex(input, pattern);
            return Array.ConvertAll(stringArray, double.Parse);
        }

        /// <summary>
        /// 使用指定的正则表达式将输入字符串分割成指定类型的数组。
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="converter">类型转换函数</param>
        /// <returns>包含分割结果的指定类型的数组</returns>
        public static T[] SplitToArrayByRegex<T>(string input, string pattern, Converter<string, T> converter)
        {
            string[] stringArray = SplitByRegex(input, pattern);
            return Array.ConvertAll(stringArray, converter);
        }

        /// <summary>
        /// 将输入字符串按固定长度分割成字符串数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="length">分割长度</param>
        /// <returns>包含分割结果的字符串数组</returns>
        public static string[] SplitByLength(string input, int length)
        {
            int count = (int)Math.Ceiling((double)input.Length / length);
            string[] result = new string[count];

            for (int i = 0; i < count; i++)
            {
                int startIndex = i * length;
                int len = Math.Min(length, input.Length - startIndex);
                result[i] = input.Substring(startIndex, len);
            }

            return result;
        }

        /// <summary>
        /// 将输入字符串按固定长度分割成整数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="length">分割长度</param>
        /// <returns>包含分割结果的整数数组</returns>
        public static int[] SplitToIntArrayByLength(string input, int length)
        {
            string[] stringArray = SplitByLength(input, length);
            return Array.ConvertAll(stringArray, int.Parse);
        }

        /// <summary>
        /// 将输入字符串按固定长度分割成双精度浮点数数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="length">分割长度</param>
        /// <returns>包含分割结果的双精度浮点数数组</returns>
        public static double[] SplitToDoubleArrayByLength(string input, int length)
        {
            string[] stringArray = SplitByLength(input, length);
            return Array.ConvertAll(stringArray, double.Parse);
        }

        /// <summary>
        /// 将输入字符串按固定长度分割成指定类型的数组。
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="length">分割长度</param>
        /// <param name="converter">类型转换函数</param>
        /// <returns>包含分割结果的指定类型的数组</returns>
        public static T[] SplitToArrayByLength<T>(string input, int length, Converter<string, T> converter)
        {
            string[] stringArray = SplitByLength(input, length);
            return Array.ConvertAll(stringArray, converter);
        }

        /// <summary>
        /// 将输入字符串按固定长度分割成二维字符串数组。
        /// </summary>
        /// <param name="input">要分割的输入字符串</param>
        /// <param name="rowLength">行长度</param>
        /// <param name="colLength">列长度</param>
        /// <returns>包含分割结果的二维字符串数组</returns>
        public static string[,] SplitTo2DArrayByLength(string input, int rowLength, int colLength)
        {
            int rowCount = (int)Math.Ceiling((double)input.Length / (rowLength * colLength));
            string[,] result = new string[rowCount, colLength];

            for (int i = 0; i < rowCount; i++)
            {
                int startIndex = i * rowLength * colLength;
                int len = Math.Min(rowLength * colLength, input.Length - startIndex);
                string row = input.Substring(startIndex, len);
                string[] cols = SplitByLength(row, colLength);
                for (int j = 0; j < colLength; j++)
                {
                    result[i, j] = cols[j];
                }
            }

            return result;
        }

    }
}

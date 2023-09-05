using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    public class RandomUtil
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 生成指定范围内的随机整数
        /// </summary>
        /// <param name="min">随机整数的最小值</param>
        /// <param name="max">随机整数的最大值</param>
        /// <returns>生成的随机整数</returns>
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// 生成指定位数的随机数字字符串
        /// </summary>
        /// <param name="length">生成的随机数字字符串的长度</param>
        /// <returns>生成的随机数字字符串</returns>
        public static string RandomDigitString(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10);
            }
            return result;
        }

        /// <summary>
        /// 生成指定位数的随机字母数字字符串
        /// </summary>
        /// <param name="length">生成的随机字母数字字符串的长度</param>
        /// <returns>生成的随机字母数字字符串</returns>
        public static string RandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += chars[random.Next(chars.Length)];
            }
            return result;
        }

        /// <summary>
        /// 生成指定长度的随机字母字符串
        /// </summary>
        /// <param name="length">生成的随机字母字符串的长度</param>
        /// <returns>生成的随机字母字符串</returns>
        public static string RandomLetterString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += chars[random.Next(chars.Length)];
            }
            return result;
        }

        /// <summary>
        /// 生成随机的布尔值
        /// </summary>
        /// <returns>生成的随机布尔值</returns>
        public static bool RandomBool()
        {
            return random.Next(2) == 0;
        }

        /// <summary>
        /// 生成指定长度的随机数组
        /// </summary>
        /// <param name="length">生成的随机数组的长度</param>
        /// <returns>生成的随机数组</returns>
        public static int[] RandomIntArray(int length)
        {
            int[] result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = random.Next();
            }
            return result;
        }

        /// <summary>
        /// 生成指定长度的随机双精度浮点数数组
        /// </summary>
        /// <param name="length">生成的随机数组的长度</param>
        /// <returns>生成的随机双精度浮点数数组</returns>
        public static double[] RandomDoubleArray(int length)
        {
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = random.NextDouble();
            }
            return result;
        }

        /// <summary>
        /// 生成指定长度的随机字符串数组
        /// </summary>
        /// <param name="length">生成的随机数组的长度</param>
        /// <param name="strLength">每个随机字符串的长度</param>
        /// <returns>生成的随机字符串数组</returns>
        public static string[] RandomStringArray(int length, int strLength)
        {
            string[] result = new string[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = RandomAlphanumericString(strLength);
            }
            return result;
        }

        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="startDate">随机日期的最早时间</param>
        /// <param name="endDate">随机日期的最晚时间</param>
        /// <returns>生成的随机日期</returns>
        public static DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, 0, random.Next(0, (int)timeSpan.TotalSeconds));
            return startDate + newSpan;
        }

        /// <summary>
        /// 生成随机枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>生成的随机枚举值</returns>
        public static T RandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }

        /// <summary>
        /// 获取一个指定范围内的随机整数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机整数</returns>
        public static int GetRandomInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// 获取一个指定范围内的随机双精度浮点数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机双精度浮点数</returns>
        public static double GetRandomDouble(double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * random.NextDouble();
        }

        /// <summary>
        /// 获取一个指定范围内的随机日期时间
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机日期时间</returns>
        public static DateTime GetRandomDateTime(DateTime minValue, DateTime maxValue)
        {
            TimeSpan timeSpan = maxValue - minValue;
            double totalSeconds = timeSpan.TotalSeconds;
            int randomSeconds = GetRandomInt(0, (int)totalSeconds);
            return minValue.AddSeconds(randomSeconds);
        }

        /// <summary>
        /// 从给定的集合中随机选取一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>随机选取的元素</returns>
        public static T GetRandomElement<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int count = source.Count();
            if (count == 0)
            {
                throw new ArgumentException("集合中必须至少有一个元素", nameof(source));
            }

            int index = GetRandomInt(0, count - 1);
            return source.ElementAt(index);
        }

        /// <summary>
        /// 生成指定长度的随机数字字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机数字字符串</returns>
        public static string RandomNumberString(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// 生成指定长度的随机字母数字字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字母数字字符串</returns>
        public static string RandomString(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                int code = random.Next(36) + 48;
                if (code >= 58 && code <= 64)
                {
                    code += 7;
                }
                if (code >= 91 && code <= 96)
                {
                    code += 6;
                }
                result += Convert.ToChar(code);
            }
            return result;
        }
    }
}

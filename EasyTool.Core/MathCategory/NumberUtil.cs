using System;

namespace EasyTool
{
    /// <summary>
    /// 数字工具类，提供了多种对数字的操作方法
    /// </summary>
    public class NumberUtil
    {
        /// <summary>
        /// 针对数字类型做加法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的和</returns>
        public static decimal Add(float a, float b)
        {
            return (decimal)a + (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做加法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的和</returns>
        public static decimal Add(double a, double b)
        {
            return (decimal)a + (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做减法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的差</returns>
        public static decimal Sub(float a, float b)
        {
            return (decimal)a - (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做减法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的差</returns>
        public static decimal Sub(double a, double b)
        {
            return (decimal)a - (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做乘法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的积</returns>
        public static decimal Mul(float a, float b)
        {
            return (decimal)a * (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做乘法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的积</returns>
        public static decimal Mul(double a, double b)
        {
            return (decimal)a * (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做除法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的商</returns>
        public static decimal Div(float a, float b)
        {
            return (decimal)a / (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做除法
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的商</returns>
        public static decimal Div(double a, double b)
        {
            return (decimal)a / (decimal)b;
        }

        /// <summary>
        /// 针对数字类型做除法，并限制返回的小数位数
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <param name="decimalPlaces">限制返回的小数位数</param>
        /// <returns>两个数字的商</returns>
        public static decimal Div(float a, float b, int decimalPlaces)
        {
            decimal result = Div(a, b);
            return decimal.Round(result, decimalPlaces);
        }

        /// <summary>
        /// 针对数字类型做除法，并限制返回的小数位数
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <param name="decimalPlaces">限制返回的小数位数</param>
        /// <returns>两个数字的商</returns>
        public static decimal Div(double a, double b, int decimalPlaces)
        {
            decimal result = Div(a, b);
            return decimal.Round(result, decimalPlaces);
        }

        /// <summary>
        /// 格式化一个 decimal 数字
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormat(decimal number, string format)
        {
            return number.ToString(format);
        }

        /// <summary>
        /// 保留一个 decimal 数字的小数点后指定位数
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <param name="decimalPlaces">小数点后保留的位数</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormat(decimal number, int decimalPlaces)
        {
            string format = "0.";
            for (int i = 0; i < decimalPlaces; i++)
            {
                format += "0";
            }
            return DecimalFormat(number, format);
        }

        /// <summary>
        /// 格式化一个 decimal 数字，并加上千位分隔符
        /// </summary>
        /// <param name="number">待格式化的数字</param>
        /// <returns>格式化后的字符串</returns>
        public static string DecimalFormatWithCommas(decimal number)
        {
            return DecimalFormat(number, "0,0.00");
        }



        /// <summary>
        /// 判断一个数字是否是质数
        /// </summary>
        /// <param name="number">待判断的数字</param>
        /// <returns>如果是质数，则返回 true；否则返回 false</returns>
        public static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 求一个数字的阶乘
        /// </summary>
        /// <param name="number">待求阶乘的数字</param>
        /// <returns>该数字的阶乘</returns>
        public static int Factorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("阶乘只能求非负整数");
            }

            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }

        /// <summary>
        /// 求两个数字的最大公约数
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的最大公约数</returns>
        public static int GCD(int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException("求最大公约数只能接受非负整数");
            }

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        /// <summary>
        /// 求两个数字的最小公倍数
        /// </summary>
        /// <param name="a">第一个数字</param>
        /// <param name="b">第二个数字</param>
        /// <returns>两个数字的最小公倍数</returns>
        public static int LCM(int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException("求最小公倍数只能接受非负整数");
            }

            return a * b / GCD(a, b);
        }

        /// <summary>
        /// 把一个数字转换为二进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的二进制字符串</returns>
        public static string ToBinaryString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                result = (number % 2).ToString() + result;
                number /= 2;
            }

            return result;
        }

        /// <summary>
        /// 把一个数字转换为八进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的八进制字符串</returns>
        public static string ToOctalString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                result = (number % 8).ToString() + result;
                number /= 8;
            }

            return result;
        }

        /// <summary>
        /// 把一个数字转换为十六进制字符串
        /// </summary>
        /// <param name="number">待转换的数字</param>
        /// <returns>该数字的十六进制字符串</returns>
        public static string ToHexString(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string result = string.Empty;
            while (number > 0)
            {
                int remainder = number % 16;
                if (remainder < 10)
                {
                    result = remainder.ToString() + result;
                }
                else
                {
                    result = (char)('A' + remainder - 10) + result;
                }
                number /= 16;
            }

            return result;
        }

        /// <summary>
        /// 求一个数字的绝对值
        /// </summary>
        /// <param name="number">待求绝对值的数字</param>
        /// <returns>该数字的绝对值</returns>
        public static int Abs(int number)
        {
            return number < 0 ? -number : number;
        }

        /// <summary>
        /// 求一个数字的平方
        /// </summary>
        /// <param name="number">待求平方的数字</param>
        /// <returns>该数字的平方</returns>
        public static int Square(int number)
        {
            return number * number;
        }

        /// <summary>
        /// 求一个数字的立方
        /// </summary>
        /// <param name="number">待求立方的数字</param>
        /// <returns>该数字的立方</returns>
        public static int Cube(int number)
        {
            return number * number * number;
        }
    }
}

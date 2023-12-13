using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.RandomCategoty
{
    public static class RandomUtil
    {
        private static readonly Random _random = new();
        /// <summary>
        /// 返回四位数的正整数字符串
        /// </summary>
        /// <returns></returns>
        public static string FourNum()
        {
            return _random.Next(1000, 10000).ToString();
        }
        /// <summary>
        /// 返回六位数的正整数字符串
        /// </summary>
        /// <returns></returns>
        public static string SixNum()
        {
            return _random.Next(1000000, 10000000).ToString();
        }
        /// <summary>
        /// 返回位数区间内的数字字符串
        /// <para></para>
        /// 左闭右开
        /// </summary>
        /// <param name="minBit">最小位数</param>
        /// <param name="maxBit">最大位数</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">当<see cref="minBit"/><=0，抛出异常</exception>
        /// <exception cref="NotSupportedException">当<see cref="maxBit"/><=0，抛出异常</exception>
        public static string RandomNum(int minBit, int maxBit)
        {
            if (minBit <= 0)
            {
                throw new NotSupportedException("minBit参数错误");
            }
            if (maxBit <= 0)
            {
                throw new NotSupportedException("maxBit参数错误");
            }
            int minValue = (int)Math.Pow(10, minBit);
            int maxValue = (int)Math.Pow(10, maxBit);
            return _random.Next(minValue, maxValue).ToString();
        }

        /// <summary>
        /// 返回区间内指定个数随机数
        /// 左闭右开
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="refCount">输出数量</param>
        /// <exception cref="NotSupportedException">当<see cref="refCount"/><0，抛出异常</exception>
        public static List<int> RandomNum(int minValue, int maxValue, int refCount)
        {
            var result = new List<int>();
            if (refCount < 0)
            {
                throw new NotSupportedException("refCount参数错误");
            }
            for (int i = 0; i < refCount; i++)
            {
                result.Add(_random.Next(minValue, maxValue));
            }

            return result;
        }

    }
}

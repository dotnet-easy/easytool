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
        /// <exception cref="NotSupportedException">当<see cref="minBit"/><0，抛出异常</exception>
        public static string RandomNum(int minBit, int maxBit)
        {
            if (minBit < 0)
            {
                throw new NotSupportedException("minBit参数错误");
            }
            int minValue = (int)Math.Pow(10, minBit);
            int maxValue = (int)Math.Pow(10, maxBit);
            return _random.Next(minValue, maxValue).ToString();
        }
        /// <summary>
        /// 返回区间内的数字字
        /// <para></para>
        /// 左闭右开
        /// </summary>
        /// <param name="minValue">最小数</param>
        /// <param name="maxValue">最大数</param>
        /// <returns></returns>
        public static int RandomInt(int minValue, int maxValue)
        {

            return _random.Next(minValue, maxValue);
        }
        /// <summary>
        /// 返回区间内指定个数随机数
        /// 左闭右开
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="refCount">输出数量</param>
        /// <returns></returns>
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
        /// <summary>
        /// 洗牌算法打乱List里面的顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        /// <summary>
        /// 返回列表里面确定数量的随机元素
        /// </summary>
        /// <typeparam name="T">源数据类型</typeparam>
        /// <param name="sourceList">源数据列表</param>
        /// <param name="needCount">需要返回数量</param>
        /// <param name="needOnly">是否需要不重复的数据</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static List<T> GetRandomElements<T>(this List<T> sourceList, int needCount, bool needOnly = true)
        {
            if (needCount<=0)
            {
                throw new NotSupportedException("needCount参数错误");
            }
            var length = sourceList.Count;
            if (needOnly)
            {
                if (needCount >= length)
                {
                    // 如果请求的元素数量大于等于列表的元素数量，返回整个列表
                    return new List<T>(sourceList);
                }

                List<T> resultList = new List<T>(needCount);

                HashSet<int> selectedIndices = new HashSet<int>();

                while (needCount-- > 0)
                {
                    int randomIndex = _random.Next(0, length);

                    // 如果该索引已经被选择过，则重新选择
                    if (selectedIndices.Contains(randomIndex))
                    {
                        continue;
                    }

                    resultList.Add(sourceList[randomIndex]);
                    selectedIndices.Add(randomIndex);
                }

                return resultList;
            }
            else
            {

                if (length <= 1)
                {
                    return sourceList;
                }
                List<T> resultList = new List<T>(needCount);

                for (int i = 0; i < needCount; i++)
                {
                    int randomIndex = _random.Next(0, sourceList.Count);
                    resultList.Add(sourceList[randomIndex]);
                }

                return resultList;
            }


        }

    }
}

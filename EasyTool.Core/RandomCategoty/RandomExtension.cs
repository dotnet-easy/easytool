using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.RandomCategoty
{
    public static class RandomExtension
    {
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
        public static string RandomNum(this Tuple<int, int> tuple) => RandomUtil.RandomNum(tuple.Item1, tuple.Item2);

        /// <summary>
        /// 返回区间内指定个数随机数
        /// 左闭右开
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="refCount">输出数量</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">当<see cref="refCount"/><0，抛出异常</exception>
        public static List<int> RandomNum(this Tuple<int, int,int> tuple) => RandomUtil.RandomNum(tuple.Item1, tuple.Item2,tuple.Item3);

    }
}

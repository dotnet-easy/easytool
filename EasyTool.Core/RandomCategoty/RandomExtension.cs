using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.RandomCategoty
{
    public static class RandomExtension
    {

        /// <summary>
        /// 返回四位数的正整数字符串
        /// </summary>
        /// <returns></returns>
        public static string FourNum() => RandomUtil.FourNum();

        /// <summary>
        /// 返回六位数的正整数字符串
        /// </summary>
        /// <returns></returns>
        public static string SixNum() => RandomUtil.SixNum();
        /// <summary>
        /// 返回位数区间内的数字字符串
        /// <para></para>
        /// 左闭右开
        /// </summary>
        /// <param name="minBit">最小位数</param>
        /// <param name="maxBit">最大位数</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">当<see cref="minBit"/><0，抛出异常</exception>
        public static string RandomNum(this Tuple<int, int> tuple) => RandomUtil.RandomNum(tuple.Item1, tuple.Item2);
        /// <summary>
        /// 返回区间内的数字字
        /// <para></para>
        /// 左闭右开
        /// </summary>
        /// <param name="minValue">最小数</param>
        /// <param name="maxValue">最大数</param>
        /// <returns></returns>
        public static int RandomInt(this Tuple<int, int> tuple) => RandomUtil.RandomInt(tuple.Item1, tuple.Item2);
        /// <summary>
        /// 返回区间内指定个数随机数
        /// 左闭右开
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="refCount">输出数量</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">当<see cref="refCount"/><0，抛出异常</exception>
        public static List<int> RandomNum(this Tuple<int, int, int> tuple) => RandomUtil.RandomNum(tuple.Item1, tuple.Item2, tuple.Item3);

        /// <summary>
        /// 洗牌算法打乱List里面的顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list) => RandomUtil.Shuffle(list);


        /// <summary>
        /// 返回列表里面确定数量的随机元素
        /// </summary>
        /// <typeparam name="T">源数据类型</typeparam>
        /// <param name="sourceList">源数据列表</param>
        /// <param name="needCount">需要返回数量</param>
        /// <param name="needOnly">是否需要不重复的数据</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static List<T> GetRandomElements<T>(this List<T> sourceList, int needCount, bool needOnly = true) => RandomUtil.GetRandomElements(sourceList, needCount, needOnly);

    }
}

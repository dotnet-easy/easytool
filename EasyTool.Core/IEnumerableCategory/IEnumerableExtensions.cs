using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool.IEnumerableCategory
{
    /// <summary>
    /// 通用拓展
    /// </summary>
    public static class IEnumerableExtensions
    {



        #region IEnumerable拓展
        /// <summary>
        /// 对List 等集合Foreach的时候不用在上层判空，直接加上这个就好
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<T> CheckNull<T>(this IEnumerable<T> values)
        {
            return values is null ? new List<T>(0) : values;
        }

        #region 集合运算
        /// <summary>
        /// 求集合的笛卡尔积
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Cartesian<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> tempProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(tempProduct,
                                          (accumulator, sequence) =>
                                             from accseq in accumulator
                                             from item in sequence
                                             select accseq.Concat(new[] { item
                                              }));
        }

        #endregion
        #endregion
    }
}

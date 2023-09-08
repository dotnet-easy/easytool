using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EasyTool.Extension
{
    /// <summary>
    /// 队列工具类
    /// </summary>
    public static class QueueExtension
    {

        /// <summary>
        /// 将指定集合中的元素添加到队列的末尾。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="collection">要添加到队列中的集合</param>
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> collection) => QueueUtil.EnqueueRange(queue, collection);

        /// <summary>
        /// 从队列中移除指定元素的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="item">要移除的元素</param>
        /// <returns>如果已成功移除元素，则为 true；否则为 false。</returns>
        public static bool Remove<T>(this Queue<T> queue, T item) => QueueUtil.Remove(queue, item);

    }
}

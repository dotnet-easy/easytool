using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 队列工具类
    /// </summary>
    public class QueueUtil
    {
        /// <summary>
        /// 将指定元素添加到队列的末尾。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="item">要添加的元素</param>
        public static void Enqueue<T>(Queue<T> queue, T item)
        {
            queue.Enqueue(item);
        }

        /// <summary>
        /// 将指定集合中的元素添加到队列的末尾。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="collection">要添加到队列中的集合</param>
        public static void EnqueueRange<T>(Queue<T> queue, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// 移除并返回位于队列开头的元素。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>队列开头的元素</returns>
        /// <exception cref="System.InvalidOperationException">队列为空时引发异常</exception>
        public static T Dequeue<T>(Queue<T> queue)
        {
            return queue.Dequeue();
        }

        /// <summary>
        /// 返回位于队列开头的元素而不将其移除。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>队列开头的元素</returns>
        /// <exception cref="System.InvalidOperationException">队列为空时引发异常</exception>
        public static T Peek<T>(Queue<T> queue)
        {
            return queue.Peek();
        }

        /// <summary>
        /// 确定队列中是否包含指定元素。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="item">要查找的元素</param>
        /// <returns>如果队列包含指定元素，则为 true；否则为 false。</returns>
        public static bool Contains<T>(Queue<T> queue, T item)
        {
            return queue.Contains(item);
        }

        /// <summary>
        /// 从队列中移除指定元素的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="item">要移除的元素</param>
        /// <returns>如果已成功移除元素，则为 true；否则为 false。</returns>
        public static bool Remove<T>(Queue<T> queue, T item)
        {
            if (queue.Contains(item))
            {
                queue = new Queue<T>(queue.Where(x => !x.Equals(item)));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将队列中的所有元素复制到新数组中。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>包含队列中所有元素的新数组</returns>
        public static T[] ToArray<T>(Queue<T> queue)
        {
            return queue.ToArray();
        }

        /// <summary>
        /// 将队列中的所有元素复制到新数组中，从指定的索引开始。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="array">要复制到的目标数组</param>
        /// <param name="arrayIndex">目标数组的起始索引</param>
        public static void CopyTo<T>(Queue<T> queue, T[] array, int arrayIndex)
        {
            queue.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 从队列中移除所有元素。
        /// </summary>
        /// <typeparam name="T">队列元素类型</typeparam>
        /// <param name="queue">队列</param>
        public static void Clear<T>(Queue<T> queue)
        {
            queue.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 双向链表工具类
    /// </summary>
    public class LinkedListUtil
    {
        /// <summary>
        /// 将指定元素添加到双向链表的结尾处。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="item">要添加的元素</param>
        public static void AddLast<T>(LinkedList<T> list, T item)
        {
            list.AddLast(item);
        }

        /// <summary>
        /// 将指定元素添加到双向链表的开头处。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="item">要添加的元素</param>
        public static void AddFirst<T>(LinkedList<T> list, T item)
        {
            list.AddFirst(item);
        }

        /// <summary>
        /// 将指定元素插入到双向链表中的指定位置之前。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要在其前面插入新元素的节点</param>
        /// <param name="item">要添加的元素</param>
        /// <returns>新节点</returns>
        public static LinkedListNode<T> AddBefore<T>(LinkedList<T> list, LinkedListNode<T> node, T item)
        {
            return list.AddBefore(node, item);
        }

        /// <summary>
        /// 将指定元素插入到双向链表中的指定位置之后。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要在其后面插入新元素的节点</param>
        /// <param name="item">要添加的元素</param>
        /// <returns>新节点</returns>
        public static LinkedListNode<T> AddAfter<T>(LinkedList<T> list, LinkedListNode<T> node, T item)
        {
            return list.AddAfter(node, item);
        }

        /// <summary>
        /// 将双向链表中的某个节点移动到链表的结尾处。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要移动的节点</param>
        public static void MoveLast<T>(LinkedList<T> list, LinkedListNode<T> node)
        {
            list.Remove(node);
            list.AddLast(node);
        }


        /// <summary>
        /// 将双向链表中移动到最前方
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要移动的节点</param>
        public static void MoveFirst<T>(LinkedList<T> list, LinkedListNode<T> node)
        {
            list.Remove(node);
            list.AddFirst(node);
        }

        /// <summary>
        /// 从双向链表中移除指定节点。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要移除的节点</param>
        public static void Remove<T>(LinkedList<T> list, LinkedListNode<T> node)
        {
            list.Remove(node);
        }

        /// <summary>
        /// 从双向链表中移除指定值的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="item">要移除的元素</param>
        /// <returns>如果成功移除了元素，则为 true；否则为 false。</returns>
        public static bool Remove<T>(LinkedList<T> list, T item)
        {
            return list.Remove(item);
        }

        /// <summary>
        /// 确定双向链表中是否包含特定值。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="item">要在双向链表中查找的元素</param>
        /// <returns>如果在双向链表中找到了 item，则为 true；否则为 false。</returns>
        public static bool Contains<T>(LinkedList<T> list, T item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// 从双向链表中移除所有节点。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        public static void Clear<T>(LinkedList<T> list)
        {
            list.Clear();
        }
    }
}

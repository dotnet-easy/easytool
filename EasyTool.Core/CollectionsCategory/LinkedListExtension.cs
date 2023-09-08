using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.Extension
{    
    
    /// <summary>
     /// 双向链表工具类
     /// </summary>
    public static class LinkedListExtension
    {
        /// <summary>
        /// 将双向链表中的某个节点移动到链表的结尾处。
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要移动的节点</param>
        public static void MoveLast<T>(this LinkedList<T> list, LinkedListNode<T> node) => LinkedListUtil.MoveLast(list,node);

        /// <summary>
        /// 将双向链表中移动到最前方
        /// </summary>
        /// <typeparam name="T">双向链表元素类型</typeparam>
        /// <param name="list">双向链表</param>
        /// <param name="node">要移动的节点</param>
        public static void MoveFirst<T>(this LinkedList<T> list, LinkedListNode<T> node) => LinkedListUtil.MoveFirst(list,node);

    }
}

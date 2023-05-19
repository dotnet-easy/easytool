using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 堆栈工具类
    /// </summary>
    public class StackUtil
    {
        /// <summary>
        /// 将指定元素推入堆栈的顶部。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <param name="item">要添加的元素</param>
        public static void Push<T>(Stack<T> stack, T item)
        {
            stack.Push(item);
        }

        /// <summary>
        /// 从堆栈的顶部移除并返回对象。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <returns>堆栈顶部的元素</returns>
        /// <exception cref="System.InvalidOperationException">堆栈为空时引发异常</exception>
        public static T Pop<T>(Stack<T> stack)
        {
            return stack.Pop();
        }

        /// <summary>
        /// 返回位于堆栈顶部的对象但不将其移除。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <returns>堆栈顶部的元素</returns>
        /// <exception cref="System.InvalidOperationException">堆栈为空时引发异常</exception>
        public static T Peek<T>(Stack<T> stack)
        {
            return stack.Peek();
        }

        /// <summary>
        /// 确定堆栈是否包含指定元素。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <param name="item">要查找的元素</param>
        /// <returns>如果堆栈包含指定元素，则为 true；否则为 false。</returns>
        public static bool Contains<T>(Stack<T> stack, T item)
        {
            return stack.Contains(item);
        }

        /// <summary>
        /// 从堆栈中移除指定元素的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <param name="item">要移除的元素</param>
        /// <returns>如果已成功移除元素，则为 true；否则为 false。</returns>
        public static bool Remove<T>(Stack<T> stack, T item)
        {
            if (stack.Contains(item))
            {
                stack = new Stack<T>(stack.Where(x => !x.Equals(item)).Reverse());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将堆栈中的所有元素复制到新数组中。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <returns>包含堆栈中所有元素的新数组</returns>
        public static T[] ToArray<T>(Stack<T> stack)
        {
            return stack.ToArray();
        }

        /// <summary>
        /// 将堆栈中的所有元素复制到新数组中，从指定的索引开始。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <param name="array">要复制到的目标数组</param>
        /// <param name="arrayIndex">目标数组的起始索引</param>
        public static void CopyTo<T>(Stack<T> stack, T[] array, int arrayIndex)
        {
            stack.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 从堆栈中移除所有元素。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        public static void Clear<T>(Stack<T> stack)
        {
            stack.Clear();
        }
    }
}

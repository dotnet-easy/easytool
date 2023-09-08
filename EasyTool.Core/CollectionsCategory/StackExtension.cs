using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool.CollectionsCategory
{
    public static class StackExtension
    {
        /// <summary>
        /// 从堆栈中移除指定元素的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">堆栈元素类型</typeparam>
        /// <param name="stack">堆栈</param>
        /// <param name="item">要移除的元素</param>
        /// <returns>如果已成功移除元素，则为 true；否则为 false。</returns>
        public static bool Remove<T>(this Stack<T> stack, T item)=> StackUtil.Remove(stack, item);
    }
}

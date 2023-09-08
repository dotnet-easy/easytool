using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool.Extension
{
    public static class ListExtension
    { 
        /// <summary>
        /// 判断两个列表是否相等。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list1">要比较的第一个列表</param>
        /// <param name="list2">要比较的第二个列表</param>
        /// <returns>如果两个列表相等，则返回 true；否则返回 false</returns>
        public static bool Equals<T>(this List<T> list1, List<T> list2)=> ListUtil.Equals(list1, list2);
    }
}

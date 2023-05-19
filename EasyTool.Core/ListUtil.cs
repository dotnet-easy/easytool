using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    public class ListUtil
    {
        /// <summary>
        /// 在列表中查找元素，并返回其索引。如果未找到，则返回 -1。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要查找的列表</param>
        /// <param name="item">要查找的元素</param>
        /// <returns>元素在列表中的索引，如果未找到则返回 -1</returns>
        public static int IndexOf<T>(List<T> list, T item)
        {
            return list.IndexOf(item);
        }

        /// <summary>
        /// 向列表中添加多个元素。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要添加元素的列表</param>
        /// <param name="items">要添加到列表中的元素</param>
        public static void AddRange<T>(List<T> list, IEnumerable<T> items)
        {
            list.AddRange(items);
        }

        /// <summary>
        /// 在列表中删除指定索引处的元素。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要删除元素的列表</param>
        /// <param name="index">要删除元素的索引</param>
        public static void RemoveAt<T>(List<T> list, int index)
        {
            list.RemoveAt(index);
        }

        /// <summary>
        /// 从列表中删除指定元素的第一个匹配项。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要删除元素的列表</param>
        /// <param name="item">要删除的元素</param>
        /// <returns>如果找到并成功删除元素，则返回 true；否则返回 false</returns>
        public static bool Remove<T>(List<T> list, T item)
        {
            return list.Remove(item);
        }

        /// <summary>
        /// 将指定的列表连接起来，形成一个新的列表。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="lists">要连接的列表</param>
        /// <returns>连接后的新列表</returns>
        public static List<T> Concat<T>(IEnumerable<List<T>> lists)
        {
            return lists.SelectMany(x => x).ToList();
        }

        /// <summary>
        /// 将指定的列表连接起来，形成一个新的列表。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="lists">要连接的列表</param>
        /// <returns>连接后的新列表</returns>
        public static List<T> Concat<T>(params List<T>[] lists)
        {
            return Concat((IEnumerable<List<T>>)lists);
        }

        /// <summary>
        /// 返回一个新的列表，其中包含指定列表中的元素，但不包括重复元素。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要去重的列表</param>
        /// <returns>去重后的新列表</returns>
        public static List<T> Distinct<T>(List<T> list)
        {
            return list.Distinct().ToList();
        }

        /// <summary>
        /// 根据指定的条件筛选出列表中符合条件的元素。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要筛选的列表</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns>符合条件的元素列表</returns>
        public static List<T> Where<T>(List<T> list, Func<T, bool> predicate)
        {
            return list.Where(predicate).ToList();
        }

        /// <summary>
        /// 将列表中的每个元素应用到指定的转换函数，并返回转换后的新列表。
        /// </summary>
        /// <typeparam name="TSource">列表元素类型</typeparam>
        /// <typeparam name="TResult">转换后的元素类型</typeparam>
        /// <param name="list">要转换的列表</param>
        /// <param name="selector">转换函数</param>
        /// <returns>转换后的新列表</returns>
        public static List<TResult> Select<TSource, TResult>(List<TSource> list, Func<TSource, TResult> selector)
        {
            return list.Select(selector).ToList();
        }

        /// <summary>
        /// 对列表中的每个元素应用指定的操作。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要应用操作的列表</param>
        /// <param name="action">要应用的操作</param>
        public static void ForEach<T>(List<T> list, Action<T> action)
        {
            list.ForEach(action);
        }

        /// <summary>
        /// 将列表中的元素排序。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要排序的列表</param>
        public static void Sort<T>(List<T> list)
        {
            list.Sort();
        }

        /// <summary>
        /// 将列表中的元素按指定的比较器排序。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要排序的列表</param>
        /// <param name="comparer">比较器</param>
        public static void Sort<T>(List<T> list, IComparer<T> comparer)
        {
            list.Sort(comparer);
        }

        /// <summary>
        /// 将列表中的元素分页显示。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要分页的列表</param>
        /// <param name="pageSize">每页显示的元素数量</param>
        /// <param name="pageIndex">要显示的页码，从 0 开始</param>
        /// <returns>指定页的元素列表</returns>
        public static List<T> Page<T>(List<T> list, int pageSize, int pageIndex)
        {
            return list.Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// 向列表中批量添加元素。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list">要添加元素的列表</param>
        /// <param name="items">要添加到列表中的元素</param>
        public static void AddRange<T>(List<T> list, params T[] items)
        {
            list.AddRange(items);
        }

        /// <summary>
        /// 判断两个列表是否相等。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list1">要比较的第一个列表</param>
        /// <param name="list2">要比较的第二个列表</param>
        /// <returns>如果两个列表相等，则返回 true；否则返回 false</returns>
        public static bool Equals<T>(List<T> list1, List<T> list2)
        {
            if (list1 == null && list2 == null)
            {
                return true;
            }
            else if (list1 == null || list2 == null)
            {
                return false;
            }
            else if (list1.Count != list2.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (!list1[i].Equals(list2[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// 返回两个列表的交集。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list1">要比较的第一个列表</param>
        /// <param name="list2">要比较的第二个列表</param>
        /// <returns>交集列表</returns>
        public static List<T> Intersect<T>(List<T> list1, List<T> list2)
        {
            return list1.Intersect(list2).ToList();
        }

        /// <summary>
        /// 返回两个列表的并集。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list1">要比较的第一个列表</param>
        /// <param name="list2">要比较的第二个列表</param>
        /// <returns>并集列表</returns>
        public static List<T> Union<T>(List<T> list1, List<T> list2)
        {
            return list1.Union(list2).ToList();
        }

        /// <summary>
        /// 返回两个列表的差集。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="list1">要比较的第一个列表</param>
        /// <param name="list2">要比较的第二个列表</param>
        /// <returns>差集列表</returns>
        public static List<T> Except<T>(List<T> list1, List<T> list2)
        {
            return list1.Except(list2).ToList();
        }

    }
}

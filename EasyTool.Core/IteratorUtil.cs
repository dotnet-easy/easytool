using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 迭代器工具类
    /// </summary>
    public static class IteratorUtil
    {
        /// <summary>
        /// 将一个数组转换为一个迭代器
        /// </summary>
        public static IEnumerable<T> AsIterator<T>(this T[] array)
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 过滤掉一个迭代器中不符合条件的元素
        /// </summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 对一个迭代器中的每个元素进行转换
        /// </summary>
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        /// <summary>
        /// 从一个迭代器中取出前 n 个元素
        /// </summary>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            foreach (var item in source)
            {
                if (count-- > 0)
                {
                    yield return item;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 跳过一个迭代器中的前 n 个元素
        /// </summary>
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
        {
            foreach (var item in source)
            {
                if (count-- > 0)
                {
                    continue;
                }
                else
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 将一个迭代器的元素分组
        /// </summary>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector, x => x);
        }

        /// <summary>
        /// 将一个迭代器的元素按照指定的方式分组
        /// </summary>
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var dictionary = new Dictionary<TKey, List<TElement>>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = new List<TElement>();
                }
                dictionary[key].Add(element);
            }
            foreach (var group in dictionary)
            {
                yield return new Grouping<TKey, TElement>(group.Key, group.Value);
            }
        }

        private class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            private readonly List<TElement> _elements;

            public Grouping(TKey key, List<TElement> elements)
            {
                Key = key;
                _elements = elements;
            }
            public TKey Key { get; }

            public IEnumerator<TElement> GetEnumerator()
            {
                return _elements.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// 对一个迭代器中的元素进行排序
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// 对一个迭代器中的元素按照指定的方式进行排序
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderBy(keySelector, comparer, false);
        }

        /// <summary>
        /// 对一个迭代器中的元素按照指定的方式进行排序，并指定排序方向
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return descending ? source.OrderByDescending(keySelector, comparer) : source.OrderBy(keySelector, comparer);
        }

        /// <summary>
        /// 对一个迭代器中的元素进行倒序排序
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderByDescending(keySelector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// 对一个迭代器中的元素按照指定的方式进行倒序排序
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.OrderByDescending(keySelector, comparer, false);
        }

        /// <summary>
        /// 对一个迭代器中的元素按照指定的方式进行倒序排序，并指定排序方向
        /// </summary>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return descending ? source.OrderBy(keySelector, comparer) : source.OrderByDescending(keySelector, comparer);
        }
    }
}

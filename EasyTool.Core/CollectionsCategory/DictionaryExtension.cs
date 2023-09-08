using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool.Extension
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// 获取字典中指定键的值，如果字典中不存在该键，则返回默认值
        /// </summary>
        /// <param name="dictionary">要获取值的字典</param>
        /// <param name="key">要获取值的键</param>
        /// <param name="defaultValue">如果字典中不存在该键，则返回的默认值</param>
        /// <returns>指定键的值，如果字典中不存在该键，则返回默认值</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
        {
            if (dictionary.TryGetValue(key, out TValue value))
            {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将一个字典的所有键值对添加到另一个字典中
        /// </summary>
        /// <param name="destination">要添加键值对的目标字典</param>
        /// <param name="source">包含要添加键值对的源字典</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> pair in source)
            {
                destination[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// 返回字典中键的集合
        /// </summary>
        /// <param name="dictionary">要获取键的字典</param>
        /// <returns>字典中所有键的集合</returns>
        public static IEnumerable<TKey> GetKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Keys;
        }

        /// <summary>
        /// 返回字典中值的集合
        /// </summary>
        /// <param name="dictionary">要获取值的字典</param>
        /// <returns>字典中所有值的集合</returns>
        public static IEnumerable<TValue> GetValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Values;
        }

        /// <summary>
        /// 从字典中删除指定的键
        /// </summary>
        /// <param name="dictionary">要删除键的字典</param>
        /// <param name="keys">要删除的键的集合</param>
        public static void RemoveKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            foreach (TKey key in keys)
            {
                dictionary.Remove(key);
            }
        }

        /// <summary>
        /// 返回字典中具有最大值的键
        /// </summary>
        /// <param name="dictionary">要查找键的字典</param>
        /// <returns>具有最大值的键</returns>
        public static TKey GetKeyWithMaxValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TValue : IComparable<TValue>
        {
            if (dictionary.Count == 0)
            {
                throw new InvalidOperationException("The dictionary is empty.");
            }

            KeyValuePair<TKey, TValue> max = dictionary.First();

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (pair.Value.CompareTo(max.Value) > 0)
                {
                    max = pair;
                }
            }

            return max.Key;
        }

        /// <summary>
        /// 返回字典中具有最小值的键
        /// </summary>
        /// <param name="dictionary">要查找键的字典</param>
        /// <returns>具有最小值的键</returns>
        public static TKey GetKeyWithMinValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TValue : IComparable<TValue>
        {
            if (dictionary.Count == 0)
            {
                throw new InvalidOperationException("The dictionary is empty.");
            }

            KeyValuePair<TKey, TValue> min = dictionary.First();

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (pair.Value.CompareTo(min.Value) < 0)
                {
                    min = pair;
                }
            }

            return min.Key;
        }
    }
}

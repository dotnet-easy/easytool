using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 数组工具类
    /// </summary>
    public class ArrayUtil
    {
        /// <summary>
        /// 判断数组是否为空
        /// </summary>
        /// <param name="array">要判断的数组</param>
        /// <returns>如果数组为空，则返回 true；否则返回 false</returns>
        public static bool IsEmpty(Array array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// 获取数组的长度
        /// </summary>
        /// <param name="array">要获取长度的数组</param>
        /// <returns>返回数组的长度</returns>
        public static int Length(Array array)
        {
            if (array == null)
            {
                return 0;
            }

            return array.Length;
        }

        /// <summary>
        /// 获取数组中的最大值
        /// </summary>
        /// <param name="array">要获取最大值的数组</param>
        /// <returns>返回数组中的最大值</returns>
        public static T Max<T>(T[] array) where T : IComparable<T>
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
            }

            return max;
        }

        /// <summary>
        /// 获取数组中的最小值
        /// </summary>
        /// <param name="array">要获取最小值的数组</param>
        /// <returns>返回数组中的最小值</returns>
        public static T Min<T>(T[] array) where T : IComparable<T>
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(min) < 0)
                {
                    min = array[i];
                }
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的和
        /// </summary>
        /// <param name="array">要获取和的数组</param>
        /// <returns>返回数组的和</returns>
        public static int Sum(int[] array)
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        /// <summary>
        /// 获取数组的平均值
        /// </summary>
        /// <param name="array">要获取平均值的数组</param>
        /// <returns>返回数组的平均值</returns>
        public static double Average(int[] array)
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            int sum = Sum(array);
            int length = Length(array);

            return (double)sum / length;
        }

        /// <summary>
        /// 数组排序
        /// </summary>
        /// <param name="array">要排序的数组</param>
        /// <returns>返回排序后的数组</returns>
        public static T[] Sort<T>(T[] array) where T : IComparable<T>
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            T[] sortedArray = new T[array.Length];
            array.CopyTo(sortedArray, 0);
            Array.Sort(sortedArray);

            return sortedArray;
        }

        /// <summary>
        /// 数组反转
        /// </summary>
        /// <param name="array">要反转的数组</param>
        /// <returns>返回反转后的数组</returns>
        public static T[] Reverse<T>(T[] array)
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }

            T[] reversedArray = new T[array.Length];
            array.CopyTo(reversedArray, 0);
            Array.Reverse(reversedArray);

            return reversedArray;
        }

        /// <summary>
        /// 判断数组是否包含某个元素
        /// </summary>
        /// <param name="array">要操作的数组</param>
        /// <param name="item">要判断的元素</param>
        /// <returns>如果数组中包含该元素，则返回 true；否则返回 false</returns>
        public static bool Contains<T>(T[] array, T item)
        {
            if (IsEmpty(array))
            {
                throw new ArgumentException("Array is empty.");
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <param name="array1">数组1</param>
        /// <param name="array2">数组2</param>
        /// <returns>返回合并后的数组</returns>
        public static T[] Concat<T>(T[] array1, T[] array2)
        {
            if (IsEmpty(array1))
            {
                return array2;
            }

            if (IsEmpty(array2))
            {
                return array1;
            }

            T[] concatedArray = new T[array1.Length + array2.Length];
            array1.CopyTo(concatedArray, 0);
            array2.CopyTo(concatedArray, array1.Length);

            return concatedArray;
        }

        /// <summary>
        /// 判断两个数组是否完全相等
        /// </summary>
        /// <param name="array1">数组1</param>
        /// <param name="array2">数组2</param>
        /// <returns>如果两个数组完全相等，则返回 true；否则返回 false</returns>
        public static bool Equals<T>(T[] array1, T[] array2)
        {
            if (IsEmpty(array1) && IsEmpty(array2))
            {
                return true;
            }

            if (IsEmpty(array1) || IsEmpty(array2))
            {
                return false;
            }

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

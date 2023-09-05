using System;

namespace EasyTool
{
    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public static class ConvertUtil
    {
        /// <summary>
        /// 将对象转换为指定类型，转换失败返回指定类型的默认值
        /// </summary>
        public static T To<T>(object value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将字符串转换为整型，转换失败返回0
        /// </summary>
        public static int ToInt32(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 将字符串转换为长整型，转换失败返回0
        /// </summary>
        public static long ToInt64(string value)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 将字符串转换为布尔型，转换失败返回默认值，默认值false
        /// </summary>
        public static bool ToBoolean(string data, bool defValue = false)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            bool temp = false;
            if (bool.TryParse(data, out temp))
            {
                return temp;
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将对象转换为布尔型，转换失败返回默认值，默认值false
        /// </summary>
        public static bool ToBoolean(object data, bool defValue = false)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }
        }

        /// <summary>
        /// 将字符串转换为单精度浮点型，转换失败返回0
        /// </summary>
        public static float ToSingle(string value)
        {
            float result;
            if (float.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 将字符串转换为双精度浮点型，转换失败返回0
        /// </summary>
        public static double ToDouble(string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 将字符串转换为十进制数，转换失败返回0
        /// </summary>
        public static decimal ToDecimal(string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 将字符串转换为日期时间，转换失败返回DateTime.MinValue
        /// </summary>
        public static DateTime ToDateTime(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 将字符串转换为枚举类型，转换失败返回默认值
        /// </summary>
        public static T ToEnum<T>(string value, T defaultValue = default(T)) where T : struct
        {
            T result;
            if (Enum.TryParse(value, out result))
            {
                return result;
            }
            return defaultValue;
        }


    }
}

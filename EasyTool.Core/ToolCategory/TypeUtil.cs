using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 泛型类型工具
    /// </summary>
    public class TypeUtil
    {
        /// <summary>
        /// 判断类型是否是可空类型
        /// </summary>
        /// <typeparam name="T">要判断的类型</typeparam>
        /// <returns>是否是可空类型</returns>
        public static bool IsNullable<T>() where T : struct
        {
            return Nullable.GetUnderlyingType(typeof(T)) != null;
        }

        /// <summary>
        /// 判断类型是否是枚举类型
        /// </summary>
        /// <typeparam name="T">要判断的类型</typeparam>
        /// <returns>是否是枚举类型</returns>
        public static bool IsEnum<T>()
        {
            return typeof(T).IsEnum;
        }

        /// <summary>
        /// 获取泛型类型的参数类型
        /// </summary>
        /// <typeparam name="T">要获取参数类型的泛型类型</typeparam>
        /// <returns>泛型类型的参数类型数组</returns>
        public static Type[] GetGenericArguments<T>()
        {
            return typeof(T).GetGenericArguments();
        }

        /// <summary>
        /// 获取类型的所有属性
        /// </summary>
        /// <typeparam name="T">要获取属性的类型</typeparam>
        /// <returns>属性数组</returns>
        public static PropertyInfo[] GetProperties<T>()
        {
            return typeof(T).GetProperties();
        }

        /// <summary>
        /// 获取类型的所有字段
        /// </summary>
        /// <typeparam name="T">要获取字段的类型</typeparam>
        /// <returns>字段数组</returns>
        public static FieldInfo[] GetFields<T>()
        {
            return typeof(T).GetFields();
        }

        /// <summary>
        /// 获取类型的所有方法
        /// </summary>
        /// <typeparam name="T">要获取方法的类型</typeparam>
        /// <returns>方法数组</returns>
        public static MethodInfo[] GetMethods<T>()
        {
            return typeof(T).GetMethods();
        }

        /// <summary>
        /// 获取类型的所有事件
        /// </summary>
        /// <typeparam name="T">要获取事件的类型</typeparam>
        /// <returns>事件数组</returns>
        public static EventInfo[] GetEvents<T>()
        {
            return typeof(T).GetEvents();
        }

        /// <summary>
        /// 获取类型的所有属性、字段、方法和事件
        /// </summary>
        /// <typeparam name="T">要获取成员的类型</typeparam>
        /// <returns>成员数组</returns>
        public static MemberInfo[] GetMembers<T>()
        {
            return typeof(T).GetMembers();
        }

        /// <summary>
        /// 获取类型的所有构造函数
        /// </summary>
        /// <typeparam name="T">要获取构造函数的类型</typeparam>
        /// <returns>构造函数数组</returns>
        public static ConstructorInfo[] GetConstructors<T>()
        {
            return typeof(T).GetConstructors();
        }

        /// <summary>
        /// 判断类型是否实现了指定的接口
        /// </summary>
        /// <typeparam name="T">要判断的类型</typeparam>
        /// <typeparam name="TInterface">要判断的接口类型</typeparam>
        /// <returns>是否实现了指定的接口</returns>
        public static bool ImplementsInterface<T, TInterface>()
        {
            return typeof(T).GetInterfaces().Any(i => i == typeof(TInterface));
        }

        /// <summary>
        /// 判断类型是否继承了指定的基类
        /// </summary>
        /// <typeparam name="T">要判断的类型</typeparam>
        /// <typeparam name="TBase">要判断的基类类型</typeparam>
        /// <returns>是否继承了指定的基类</returns>
        public static bool InheritsFrom<T, TBase>()
        {
            return typeof(T).IsSubclassOf(typeof(TBase));
        }

        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        /// <typeparam name="T">要创建实例的类型</typeparam>
        /// <returns>类型的实例</returns>
        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        /// <typeparam name="T">要创建实例的类型</typeparam>
        /// <param name="args">构造函数的参数</param>
        /// <returns>类型的实例</returns>
        public static T CreateInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// 获取枚举类型的所有值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举类型的所有值</returns>
        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (!IsEnum<T>())
            {
                throw new ArgumentException("Type is not an enum type");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// 将字符串转换为指定类型的值
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="value">要转换的字符串</param>
        /// <returns>转换后的值</returns>
        public static T ConvertFromString<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 将值转换为指定类型的字符串
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的字符串</returns>
        public static string ConvertToString<T>(T value)
        {
            return Convert.ToString(value);
        }
    }
}

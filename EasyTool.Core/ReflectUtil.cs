using System;
using System.Linq;
using System.Reflection;

namespace EasyTool
{
    /// <summary>
    /// 反射工具类
    /// </summary>
    public class ReflectUtil
    {
        /// <summary>
        /// 根据类型名称获取Type对象
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>Type对象</returns>
        public static Type GetType(string typeName)
        {
            return Type.GetType(typeName);
        }

        /// <summary>
        /// 获取指定程序集中的所有类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>类型数组</returns>
        public static Type[] GetTypes(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        /// <summary>
        /// 获取指定类型所在的程序集
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>程序集</returns>
        public static Assembly GetAssembly(Type type)
        {
            return type.Assembly;
        }

        /// <summary>
        /// 获取指定类型的指定类型的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>特性对象</returns>
        public static T GetAttribute<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>();
        }

        /// <summary>
        /// 获取指定类型的指定类型的特性数组
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>特性数组</returns>
        public static T[] GetAttributes<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttributes<T>().ToArray();
        }

        /// <summary>
        /// 获取指定类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 获取类型的基类
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>基类</returns>
        public static Type GetBaseType(Type type)
        {
            return type.BaseType;
        }

        /// <summary>
        /// 判断类型是否实现了某个接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>是否实现</returns>
        public static bool HasInterface(Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取方法的参数信息
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>参数信息数组</returns>
        public static ParameterInfo[] GetParameters(MethodInfo method)
        {
            return method.GetParameters();
        }

        /// <summary>
        /// 获取类型的所有构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>构造函数数组</returns>
        public static ConstructorInfo[] GetConstructors(Type type)
        {
            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的所有属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>属性数组</returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的所有字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>字段数组</returns>
        public static FieldInfo[] GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的所有方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>方法数组</returns>
        public static MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的所有事件
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>事件数组</returns>
        public static EventInfo[] GetEvents(Type type)
        {
            return type.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取类型的所有接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>接口数组</returns>
        public static Type[] GetInterfaces(Type type)
        {
            return type.GetInterfaces();
        }

        /// <summary>
        /// 获取类型的所有属性名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>属性名数组</returns>
        public static string[] GetPropertyNames(Type type)
        {
            return GetProperties(type).Select(p => p.Name).ToArray();
        }

        /// <summary>
        /// 获取类型的所有字段名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>字段名数组</returns>
        public static string[] GetFieldNames(Type type)
        {
            return GetFields(type).Select(f => f.Name).ToArray();
        }

        /// <summary>
        /// 获取类型的所有方法名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>方法名数组</returns>
        public static string[] GetMethodNames(Type type)
        {
            return GetMethods(type).Select(m => m.Name).ToArray();
        }

        /// <summary>
        /// 获取类型的所有事件名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>事件名数组</returns>
        public static string[] GetEventNames(Type type)
        {
            return GetEvents(type).Select(e => e.Name).ToArray();
        }

        /// <summary>
        /// 获取类型的所有接口名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>接口名数组</returns>
        public static string[] GetInterfaceNames(Type type)
        {
            return GetInterfaces(type).Select(i => i.Name).ToArray();
        }

        /// <summary>
        /// 创建类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>实例</returns>
        public static object CreateInstance(Type type, params object[] args)
        {
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, args.Select(a => a.GetType()).ToArray(), null);
            if (constructor == null)
            {
                throw new ArgumentException($"Type {type} does not have a constructor with specified arguments");
            }
            return constructor.Invoke(args);
        }

        /// <summary>
        /// 调用泛型方法
        /// </summary>
        /// <param name="obj">调用方法的对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="genericType">泛型参数类型</param>
        /// <param name="args">方法参数</param>
        /// <returns>方法返回值</returns>
        public static object InvokeGenericMethod(object obj, string methodName, Type genericType, params object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(methodName);
            MethodInfo genericMethod = method.MakeGenericMethod(genericType);
            return genericMethod.Invoke(obj, args);
        }
    }
}

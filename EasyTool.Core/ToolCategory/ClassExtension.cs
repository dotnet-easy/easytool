using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyTool.Extension
{
    /// <summary>
    /// ClassUtil 工具类提供了许多有用的方法，可以帮助您轻松处理和操作C#类
    /// </summary>
    public static class ClassExtension
    {
        /// <summary>
        /// 获取类的继承层次结构
        /// </summary>
        /// <param name="type">要获取继承层次结构的类</param>
        /// <returns>类的继承层次结构</returns>
        public static Type[] GetClassHierarchy(this Type type) => ClassUtil.GetClassHierarchy(type);




        /// <summary>
        /// 获取类的静态属性的值
        /// </summary>
        /// <param name="type">要获取静态属性的类</param>
        /// <param name="propertyName">要获取的静态属性的名称</param>
        /// <returns>静态属性的值</returns>
        public static object GetStaticPropertyValue(this Type type, string propertyName) => ClassUtil.GetStaticPropertyValue(type, propertyName);


        /// <summary>
        /// 设置类的静态属性的值
        /// </summary>
        /// <param name="type">要设置静态属性的类</param>
        /// <param name="propertyName">要设置的静态属性的名称</param>
        /// <param name="value">要设置的静态属性的值</param>
        public static void SetStaticPropertyValue(this Type type, string propertyName, object value) => ClassUtil.SetStaticPropertyValue(type, propertyName, value);


        /// <summary>
        /// 获取类的静态字段的值
        /// </summary>
        /// <param name="type">要获取静态字段的类</param>
        /// <param name="fieldName">要获取的静态字段的名称</param>
        /// <returns>静态字段的值</returns>
        public static object GetStaticFieldValue(this Type type, string fieldName) => ClassUtil.GetStaticFieldValue(type, fieldName);

        /// <summary>
        /// 设置类的静态字段的值
        /// </summary>
        /// <param name="type">要设置静态字段的类</param>
        /// <param name="fieldName">要设置的静态字段的名称</param>
        /// <param name="value">要设置的静态字段的值</param>
        public static void SetStaticFieldValue(this Type type, string fieldName, object value) => ClassUtil.SetStaticFieldValue(type, fieldName, value);


        /// <summary>
        /// 动态调用类的静态方法
        /// </summary>
        /// <param name="type">要调用静态方法的类</param>
        /// <param name="methodName">要调用的静态方法的名称</param>
        /// <param name="arguments">要传递给静态方法的参数</param>
        /// <returns>静态方法的返回值</returns>
        public static object InvokeStaticMethod(this Type type, string methodName, object[] arguments) => ClassUtil.InvokeStaticMethod(type, methodName, arguments);

        ///// <summary>
        ///// 动态调用类的实例方法
        ///// </summary>
        ///// <param name="instance">要调用实例方法的类实例</param>
        ///// <param name="methodName">要调用的实例方法的名称</param>
        ///// <param name="arguments">要传递给实例方法的参数</param>
        ///// <returns>实例方法的返回值</returns>
        //public static object InvokeMethod(object instance, string methodName, object[] arguments)
        //{
        //    Type type = instance.GetType();
        //    MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
        //    return method.Invoke(instance, arguments);
        //}

        /// <summary>
        /// 动态创建类的实例
        /// </summary>
        /// <param name="type">要创建实例的类</param>
        /// <param name="constructorArguments">要传递给构造函数的参数</param>
        /// <returns>类的新实例</returns>
        public static object CreateInstance(this Type type, object[] constructorArguments) => ClassUtil.CreateInstance(type, constructorArguments);
    }
}

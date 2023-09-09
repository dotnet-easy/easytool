using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// ClassUtil 工具类提供了许多有用的方法，可以帮助您轻松处理和操作C#类
    /// </summary>
    public class ClassUtil
    {
        /// <summary>
        /// 获取类的完全限定名
        /// </summary>
        /// <param name="type">要获取名称的类</param>
        /// <returns>类的完全限定名</returns>
        public static string GetClassName(Type type)
        {
            return type.FullName;
        }

        /// <summary>
        /// 获取类的命名空间
        /// </summary>
        /// <param name="type">要获取命名空间的类</param>
        /// <returns>类的命名空间</returns>
        public static string GetClassNamespace(Type type)
        {
            return type.Namespace;
        }

        /// <summary>
        /// 获取类的继承层次结构
        /// </summary>
        /// <param name="type">要获取继承层次结构的类</param>
        /// <returns>类的继承层次结构</returns>
        public static Type[] GetClassHierarchy(Type type)
        {
            Type[] hierarchy = new Type[0];
            Type currentType = type;
            while (currentType != null)
            {
                Array.Resize(ref hierarchy, hierarchy.Length + 1);
                hierarchy[hierarchy.Length - 1] = currentType;
                currentType = currentType.BaseType;
            }
            return hierarchy;
        }

        /// <summary>
        /// 获取类的所有方法
        /// </summary>
        /// <param name="type">要获取方法的类</param>
        /// <returns>类的所有方法</returns>
        public static MethodInfo[] GetClassMethods(Type type)
        {
            return type.GetMethods();
        }

        /// <summary>
        /// 获取类的所有属性
        /// </summary>
        /// <param name="type">要获取属性的类</param>
        /// <returns>类的所有属性</returns>
        public static PropertyInfo[] GetClassProperties(Type type)
        {
            return type.GetProperties();
        }

        /// <summary>
        /// 获取类的所有字段
        /// </summary>
        /// <param name="type">要获取字段的类</param>
        /// <returns>类的所有字段</returns>
        public static FieldInfo[] GetClassFields(Type type)
        {
            return type.GetFields();
        }

        /// <summary>
        /// 获取类的所有事件
        /// </summary>
        /// <param name="type">要获取事件的类</param>
        /// <returns>类的所有事件</returns>
        public static EventInfo[] GetClassEvents(Type type)
        {
            return type.GetEvents();
        }

        /// <summary>
        /// 获取类的所有构造函数
        /// </summary>
        /// <param name="type">要获取构造函数的类</param>
        /// <returns>类的所有构造函数</returns>
        public static ConstructorInfo[] GetClassConstructors(Type type)
        {
            return type.GetConstructors();
        }

        /// <summary>
        /// 获取类的默认构造函数
        /// </summary>
        /// <param name="type">要获取默认构造函数的类</param>
        /// <returns>类的默认构造函数</returns>
        public static ConstructorInfo GetDefaultClassConstructor(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes);
        }

        /// <summary>
        /// 获取类的静态属性的值
        /// </summary>
        /// <param name="type">要获取静态属性的类</param>
        /// <param name="propertyName">要获取的静态属性的名称</param>
        /// <returns>静态属性的值</returns>
        public static object GetStaticPropertyValue(Type type, string propertyName)
        {
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public);
            return property.GetValue(null);
        }

        /// <summary>
        /// 设置类的静态属性的值
        /// </summary>
        /// <param name="type">要设置静态属性的类</param>
        /// <param name="propertyName">要设置的静态属性的名称</param>
        /// <param name="value">要设置的静态属性的值</param>
        public static void SetStaticPropertyValue(Type type, string propertyName, object value)
        {
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public);
            property.SetValue(null, value);
        }

        /// <summary>
        /// 获取类的静态字段的值
        /// </summary>
        /// <param name="type">要获取静态字段的类</param>
        /// <param name="fieldName">要获取的静态字段的名称</param>
        /// <returns>静态字段的值</returns>
        public static object GetStaticFieldValue(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
            return field.GetValue(null);
        }

        /// <summary>
        /// 设置类的静态字段的值
        /// </summary>
        /// <param name="type">要设置静态字段的类</param>
        /// <param name="fieldName">要设置的静态字段的名称</param>
        /// <param name="value">要设置的静态字段的值</param>
        public static void SetStaticFieldValue(Type type, string fieldName, object value)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
            field.SetValue(null, value);
        }

        /// <summary>
        /// 动态调用类的静态方法
        /// </summary>
        /// <param name="type">要调用静态方法的类</param>
        /// <param name="methodName">要调用的静态方法的名称</param>
        /// <param name="arguments">要传递给静态方法的参数</param>
        /// <returns>静态方法的返回值</returns>
        public static object InvokeStaticMethod(Type type, string methodName, object[] arguments)
        {
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            return method.Invoke(null, arguments);
        }

        /// <summary>
        /// 动态调用类的实例方法
        /// </summary>
        /// <param name="instance">要调用实例方法的类实例</param>
        /// <param name="methodName">要调用的实例方法的名称</param>
        /// <param name="arguments">要传递给实例方法的参数</param>
        /// <returns>实例方法的返回值</returns>
        public static object InvokeMethod(object instance, string methodName, object[] arguments)
        {
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
            return method.Invoke(instance, arguments);
        }

        /// <summary>
        /// 动态创建类的实例
        /// </summary>
        /// <param name="type">要创建实例的类</param>
        /// <param name="constructorArguments">要传递给构造函数的参数</param>
        /// <returns>类的新实例</returns>
        public static object CreateInstance(Type type, params object[] constructorArguments)
        {
            ConstructorInfo constructor = type.GetConstructor(GetParameterTypes(constructorArguments));
            return constructor.Invoke(constructorArguments);
        }

        /// <summary>
        /// 获取构造函数参数类型的数组
        /// </summary>
        /// <param name="parameters">要获取参数类型的参数数组</param>
        /// <returns>参数类型的数组</returns>
        private static Type[] GetParameterTypes(object[] parameters)
        {
            if (parameters == null)
            {
                return Type.EmptyTypes;
            }
            Type[] parameterTypes = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    parameterTypes[i] = typeof(object);
                }
                else
                {
                    parameterTypes[i] = parameters[i].GetType();
                }
            }
            return parameterTypes;
        }
    }
}

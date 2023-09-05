using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// dll工具
    /// </summary>
    public class DLLUtil
    {
        /// <summary>
        /// 根据文件路径加载 DLL 程序集，并返回一个 Assembly 对象
        /// </summary>
        /// <param name="dllFilePath">DLL 文件路径</param>
        /// <returns>返回一个 Assembly 对象</returns>
        public static Assembly LoadAssembly(string dllFilePath)
        {
            return Assembly.LoadFile(dllFilePath);
        }

        /// <summary>
        /// 根据类型名称从程序集中获取 Type 对象
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="typeName">类型名称</param>
        /// <returns>返回 Type 对象</returns>
        public static Type GetTypeFromAssembly(Assembly assembly, string typeName)
        {
            return assembly.GetType(typeName);
        }

        /// <summary>
        /// 创建指定类型的实例，并返回一个 Object 对象
        /// </summary>
        /// <param name="type">要创建实例的类型</param>
        /// <param name="parameters">实例化类型所需要的参数</param>
        /// <returns>返回创建的实例对象</returns>
        public static object CreateInstance(Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters);
        }

        /// <summary>
        /// 根据类型名称创建实例，并返回一个 Object 对象
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="parameters">实例化类型所需要的参数</param>
        /// <returns>返回创建的实例对象</returns>
        public static object CreateInstanceFromAssembly(Assembly assembly, string typeName, params object[] parameters)
        {
            Type type = GetTypeFromAssembly(assembly, typeName);
            if (type != null)
            {
                return CreateInstance(type, parameters);
            }
            return null;
        }

        /// <summary>
        /// 调用对象的方法，并返回调用结果
        /// </summary>
        /// <param name="instance">要调用方法的对象</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameters">方法所需要的参数</param>
        /// <returns>返回调用结果</returns>
        public static object InvokeMethod(object instance, string methodName, params object[] parameters)
        {
            Type type = instance.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName);
            if (methodInfo != null)
            {
                return methodInfo.Invoke(instance, parameters);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取程序集中所有的类型信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>返回 Type[] 数组，数组中每个元素代表程序集中的一个类型</returns>
        public static Type[] GetAllTypesFromAssembly(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        /// <summary>
        /// 判断指定类型是否实现了指定的接口
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <param name="interfaceType">要判断的接口类型</param>
        /// <returns>返回布尔值，表示指定类型是否实现了指定的接口</returns>
        public static bool IsImplementInterface(Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 从指定目录中加载所有的 DLL 文件，并返回一个 Assembly[] 数组
        /// </summary>
        /// <param name="directory">要加载 DLL 文件的目录</param>
        /// <returns>返回一个 Assembly[] 数组，数组中每个元素代表一个 DLL 程序集</returns>
        public static Assembly[] LoadAllDllsFromDirectory(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    throw new Exception("LoadAllDllsFromDirectory Error: Directory not exist.");
                }

                string[] dllFiles = Directory.GetFiles(directory, "*.dll");
                if (dllFiles.Length == 0)
                {
                    throw new Exception("LoadAllDllsFromDirectory Error: No DLL file found.");
                }

                Assembly[] assemblies = new Assembly[dllFiles.Length];
                for (int i = 0; i < dllFiles.Length; i++)
                {
                    assemblies[i] = LoadAssembly(dllFiles[i]);
                }
                return assemblies;
            }
            catch (Exception ex)
            {
                throw new Exception("LoadAllDllsFromDirectory Error: " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Linq;
using System.IO;

namespace EasyTool
{
    /// <summary>
    /// MEF加载工具
    /// </summary>
    public class MEFUtil
    {
        // 默认扫描程序集的路径
        private static readonly string DefaultDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 从指定目录动态加载导出部件
        /// </summary>
        /// <typeparam name="T">导出部件的类型</typeparam>
        /// <param name="directory">目录路径</param>
        /// <returns>导出部件的列表</returns>
        public static IEnumerable<T> LoadExportParts<T>(string directory = null)
        {
            // 如果目录为空，则使用默认目录
            directory ??= DefaultDirectory;

            // 创建目录目录目录目录
            var catalog = new DirectoryCatalog(directory);
            // 创建容器并将目录添加到容器中
            var container = new CompositionContainer(catalog);

            // 从容器中获取导出的部件
            var parts = container.GetExportedValues<T>();
            return parts;
        }

        /// <summary>
        /// 从指定程序集动态加载导出部件
        /// </summary>
        /// <typeparam name="T">导出部件的类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>导出部件的列表</returns>
        public static IEnumerable<T> LoadExportPartsFromAssembly<T>(string assemblyName)
        {
            // 加载指定的程序集
            var assembly = Assembly.Load(assemblyName);
            // 创建程序集目录
            var catalog = new AssemblyCatalog(assembly);
            // 创建容器并将目录添加到容器中
            var container = new CompositionContainer(catalog);

            // 从容器中获取导出的部件
            var parts = container.GetExportedValues<T>();
            return parts;
        }

        /// <summary>
        /// 从指定文件夹中加载所有导出部件
        /// </summary>
        /// <typeparam name="T">导出部件类型</typeparam>
        /// <param name="folderPath">指定文件夹路径</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns>导出部件列表</returns>
        public static IEnumerable<T> LoadExportPartsFromFolder<T>(string folderPath, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var catalog = new DirectoryCatalog(folderPath, "*.dll");
            using var container = new CompositionContainer(catalog);
            return container.GetExportedValues<T>();
        }

        /// <summary>
        /// 从指定类型中加载所有导出部件
        /// </summary>
        /// <typeparam name="T">导出部件类型</typeparam>
        /// <param name="type">指定类型</param>
        /// <returns>导出部件列表</returns>
        public static IEnumerable<T> LoadExportPartsFromType<T>(Type type)
        {
            var catalog = new TypeCatalog(type);
            using var container = new CompositionContainer(catalog);
            return container.GetExportedValues<T>();
        }

        /// <summary>
        /// 加载多个目录中的导出部件
        /// </summary>
        /// <typeparam name="T">导出部件类型</typeparam>
        /// <param name="folderPaths">多个目录路径</param>
        /// <returns>导出部件列表</returns>
        public static IEnumerable<T> LoadExportPartsFromFolders<T>(IEnumerable<string> folderPaths)
        {
            var catalogs = folderPaths.Select(path => new DirectoryCatalog(path, "*.dll"));
            var aggregateCatalog = new AggregateCatalog(catalogs);
            using var container = new CompositionContainer(aggregateCatalog);
            return container.GetExportedValues<T>();
        }

        /// <summary>
        /// 从指定容器中获取导入部件
        /// </summary>
        /// <typeparam name="T">导入部件的类型</typeparam>
        /// <param name="container">容器</param>
        /// <returns>导入部件的实例</returns>
        public static T GetImportPart<T>(CompositionContainer container)
        {
            // 获取导入部件的实例
            var part = container.GetExportedValue<T>();
            return part;
        }

        /// <summary>
        /// 从指定容器中获取导入部件的列表
        /// </summary>
        /// <typeparam name="T">导入部件的类型</typeparam>
        /// <param name="container">容器</param>
        /// <returns>导入部件的列表</returns>
        public static IEnumerable<T> GetImportParts<T>(CompositionContainer container)
        {
            // 获取导入部件的列表
            var parts = container.GetExportedValues<T>();
            return parts;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 运行时工具
    /// </summary>
    public class RuntimeUtil
    {
        /// <summary>
        /// 获取当前运行的 .NET 版本
        /// </summary>
        /// <returns>.NET 版本</returns>
        public static string GetDotNetVersion()
        {
            return Environment.Version.ToString();
        }

        /// <summary>
        /// 获取当前操作系统版本
        /// </summary>
        /// <returns>操作系统版本</returns>
        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        /// <summary>
        /// 获取当前运行环境的处理器架构
        /// </summary>
        /// <returns>处理器架构</returns>
        public static string GetProcessArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
        }

        /// <summary>
        /// 获取当前应用程序内存使用量
        /// </summary>
        /// <returns>内存使用量（字节）</returns>
        public static long GetCurrentMemoryUsage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return GC.GetTotalMemory(true);
        }

        /// <summary>
        /// 获取当前运行时间
        /// </summary>
        /// <returns>运行时间（秒）</returns>
        public static int GetCurrentRunningTime()
        {
            return (int)Stopwatch.StartNew().Elapsed.TotalSeconds;
        }

        /// <summary>
        /// 关闭当前应用程序
        /// </summary>
        public static void ExitApplication()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 获取当前系统的物理内存总量
        /// </summary>
        /// <returns>物理内存总量（字节）</returns>
        public static long GetTotalPhysicalMemory()
        {
            PerformanceCounter pc = new PerformanceCounter("Memory", "Available Bytes");
            return pc.RawValue;
        }

        /// <summary>
        /// 获取当前系统的可用物理内存量
        /// </summary>
        /// <returns>可用物理内存量（字节）</returns>
        public static float GetAvailablePhysicalMemory()
        {
            PerformanceCounter pc = new PerformanceCounter("Memory", "Available Bytes");
            return pc.NextValue();
        }

        /// <summary>
        /// 获取当前系统的虚拟内存总量
        /// </summary>
        /// <returns>虚拟内存总量（字节）</returns>
        public static long GetTotalVirtualMemory()
        {
            PerformanceCounter pc = new PerformanceCounter("Memory", "Committed Bytes");
            return pc.RawValue;
        }

        /// <summary>
        /// 获取当前系统的可用虚拟内存量
        /// </summary>
        /// <returns>可用虚拟内存量（字节）</returns>
        public static float GetAvailableVirtualMemory()
        {
            PerformanceCounter pc = new PerformanceCounter("Memory", "Committed Bytes");
            return pc.NextValue();
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        /// <summary>
        /// 获取当前系统的实际物理内存总量
        /// </summary>
        /// <returns>实际物理内存总量（字节）</returns>
        public static long GetRealTotalPhysicalMemory()
        {
            GetPhysicallyInstalledSystemMemory(out long memoryInBytes);
            return memoryInBytes * 1024;
        }
    }
}

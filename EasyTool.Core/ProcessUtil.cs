using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 进程工具类
    /// </summary>
    public class ProcessUtil
    {
        /// <summary>
        /// 通过进程名称获取进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns>进程</returns>
        public static Process GetProcessByName(string processName)
        {
            // 获取当前运行的所有进程
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                return processes[0];
            }
            return null;
        }

        /// <summary>
        /// 获取进程的所有线程
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>线程集合</returns>
        public static ProcessThreadCollection GetProcessThreads(Process process)
        {
            return process.Threads;
        }

        /// <summary>
        /// 获取进程的主窗口句柄
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>窗口句柄</returns>
        public static IntPtr GetMainWindowHandle(Process process)
        {
            return process.MainWindowHandle;
        }

        /// <summary>
        /// 获取进程的主窗口标题
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>窗口标题</returns>
        public static string GetMainWindowTitle(Process process)
        {
            return process.MainWindowTitle;
        }

        /// <summary>
        /// 获取进程的所有模块
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>模块集合</returns>
        public static ProcessModuleCollection GetProcessModules(Process process)
        {
            return process.Modules;
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="process">进程</param>
        public static void KillProcess(Process process)
        {
            process.Kill();
        }

        /// <summary>
        /// 关闭进程并等待结束
        /// </summary>
        /// <param name="process">进程</param>
        public static void KillProcessAndWait(Process process)
        {
            process.Kill();
            process.WaitForExit();
        }

        /// <summary>
        /// 启动新进程
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>新进程</returns>
        public static Process StartProcess(string fileName)
        {
            return Process.Start(fileName);
        }

        /// <summary>
        /// 启动新进程并等待结束
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void StartProcessAndWait(string fileName)
        {
            var process = Process.Start(fileName);
            process.WaitForExit();
        }

        /// <summary>
        /// 判断进程是否存在
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns>是否存在</returns>
        public static bool IsProcessExists(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        /// <summary>
        /// 获取进程使用的内存大小
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>内存大小（字节）</returns>
        public static long GetProcessMemorySize(Process process)
        {
            return process.WorkingSet64;
        }

        /// <summary>
        /// 暂停进程
        /// </summary>
        /// <param name="process">进程</param>
        public static void SuspendProcess(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                SuspendThread(pOpenThread);
                CloseHandle(pOpenThread);
            }
        }

        /// <summary>
        /// 恢复进程
        /// </summary>
        /// <param name="process">进程</param>
        public static void ResumeProcess(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                ResumeThread(pOpenThread);
                CloseHandle(pOpenThread);
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern IntPtr CloseHandle(IntPtr hObject);
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

    }
}

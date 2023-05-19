using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 环境工具
    /// </summary>
    public class EnvUtil
    {
        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns>系统信息字符串</returns>
        public static string GetSystemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("操作系统版本：" + Environment.OSVersion.ToString());
            sb.AppendLine("系统位数：" + (Environment.Is64BitOperatingSystem ? "64 位" : "32 位"));
            sb.AppendLine("系统目录：" + Environment.SystemDirectory);
            sb.AppendLine("处理器数量：" + Environment.ProcessorCount);
            sb.AppendLine("计算机名：" + Environment.MachineName);
            sb.AppendLine("用户名：" + Environment.UserName);
            sb.AppendLine("用户域名：" + Environment.UserDomainName);
            sb.AppendLine("当前目录：" + Environment.CurrentDirectory);
            sb.AppendLine("CLR版本：" + Environment.Version.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 获取环境变量值
        /// </summary>
        /// <param name="name">环境变量名称</param>
        /// <returns>环境变量值</returns>
        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

        /// <summary>
        /// 设置环境变量值
        /// </summary>
        /// <param name="name">环境变量名称</param>
        /// <param name="value">环境变量值</param>
        public static void SetEnvironmentVariable(string name, string value)
        {
            Environment.SetEnvironmentVariable(name, value);
        }

        /// <summary>
        /// 获取环境变量列表
        /// </summary>
        /// <returns>环境变量列表</returns>
        public static IDictionary<string, string> GetEnvironmentVariables()
        {
            IDictionary<string, string> variables = new Dictionary<string, string>();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                variables.Add(de.Key.ToString(), de.Value.ToString());
            }
            return variables;
        }

        /// <summary>
        /// 获取当前目录下的文件列表
        /// </summary>
        /// <returns>当前目录下的文件列表</returns>
        public static List<string> GetFilesInCurrentDirectory()
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory))
            {
                files.Add(file);
            }
            return files;
        }

        /// <summary>
        /// 获取指定目录下的文件列表
        /// </summary>
        /// <param name="path">指定目录路径</param>
        /// <returns>指定目录下的文件列表</returns>
        public static List<string> GetFilesInDirectory(string path)
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            return files;
        }

        /// <summary>
        /// 获取当前目录下的目录列表
        /// </summary>
        /// <returns>当前目录下的目录列表</returns>
        public static List<string> GetDirectoriesInCurrentDirectory()
        {
            List<string> directories = new List<string>();
            foreach (string directory in Directory.GetDirectories(Environment.CurrentDirectory))
            {
                directories.Add(directory);
            }
            return directories;
        }

        /// <summary>
        /// 获取指定目录下的目录列表
        /// </summary>
        /// <param name="path">指定目录路径</param>
        /// <returns>指定目录下的目录列表</returns>
        public static List<string> GetDirectoriesInDirectory(string path)
        {
            List<string> directories = new List<string>();
            foreach (string directory in Directory.GetDirectories(path))
            {
                directories.Add(directory);
            }
            return directories;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void CreateFile(string path)
        {
            File.Create(path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录是否存在</returns>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件是否存在</returns>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetFileSize(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }

        /// <summary>
        /// 获取文件的创建时间
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的创建时间</returns>
        public static DateTime GetFileCreationTime(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.CreationTime;
        }

        /// <summary>
        /// 获取文件的修改时间
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的修改时间</returns>
        public static DateTime GetFileLastWriteTime(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.LastWriteTime;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已有文件</param>
        public static void CopyFile(string sourcePath, string destinationPath, bool overwrite)
        {
            File.Copy(sourcePath, destinationPath, overwrite);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        public static void MoveFile(string sourcePath, string destinationPath)
        {
            File.Move(sourcePath, destinationPath);
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns>网络时间</returns>
        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "time.windows.com";
            byte[] ntpData = new byte[48];
            ntpData[0] = 0x1B;
            IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
            IPEndPoint ipEndPoint = new IPEndPoint(addresses[0], 123);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);
            }
            const byte offsetTransmitTime = 40;
            ulong intPart = BitConverter.ToUInt32(ntpData, offsetTransmitTime);
            ulong fractPart = BitConverter.ToUInt32(ntpData, offsetTransmitTime + 4);
            ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            return new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds).ToLocalTime();
        }

        /// <summary>
        /// 判断当前系统是否为Windows操作系统
        /// </summary>
        /// <returns>当前系统是否为Windows操作系统</returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// 判断当前系统是否为Linux操作系统
        /// </summary>
        /// <returns>当前系统是否为Linux操作系统</returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// 判断当前系统是否为macOS操作系统
        /// </summary>
        /// <returns>当前系统是否为macOS操作系统</returns>
        public static bool IsMacOS()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }
}

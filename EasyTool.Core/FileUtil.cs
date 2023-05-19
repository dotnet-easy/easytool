using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EasyTool
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileUtil
    {

        /// <summary>
        /// 判断当前操作系统是否为 Windows
        /// </summary>
        public static bool IsWindows()
        {
            // 判断当前操作系统的 PlatformID 是否为 Win32S、Win32Windows、Win32NT 或 WinCE
            return Environment.OSVersion.Platform == PlatformID.Win32S
                || Environment.OSVersion.Platform == PlatformID.Win32Windows
                || Environment.OSVersion.Platform == PlatformID.Win32NT
                || Environment.OSVersion.Platform == PlatformID.WinCE;
        }

        /// <summary>
        /// 判断当前操作系统是否为 Unix
        /// </summary>
        public static bool IsUnix()
        {
            // 判断当前操作系统的 PlatformID 是否为 Unix
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        /// <summary>
        /// 判断当前操作系统是否为 Xbox
        /// </summary>
        public static bool IsXbox()
        {
            // 判断当前操作系统的 PlatformID 是否为 Xbox
            return Environment.OSVersion.Platform == PlatformID.Xbox;
        }

        /// <summary>
        /// 判断当前操作系统是否为 macOS
        /// </summary>
        public static bool IsMacOSX()
        {
            // 判断当前操作系统的 PlatformID 是否为 macOSX
            return Environment.OSVersion.Platform == PlatformID.MacOSX;
        }

        /// <summary>
        /// 判断文件或目录是否为空
        /// 
        /// 目录：里面没有文件时为空 
        /// 文件：文件大小为0时为空
        /// </summary>
        /// <param name="path">文件或目录的路径</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty(string path)
        {
            // 判断是否为目录
            if (Directory.Exists(path))
            {
                // 如果是目录，遍历目录下的所有文件，判断是否有文件
                return Directory.GetFiles(path).Length>0;
            }
            else
            {
                // 如果是文件，判断文件大小是否为 0
                return new FileInfo(path).Length == 0;
            }
        }


        /// <summary>
        /// 递归遍历目录以及子目录中的所有文件
        /// </summary>
        /// <param name="path">要遍历的目录路径</param>
        /// <param name="searchPattern">文件过滤规则，例如 "*.txt"，默认为 "*"</param>
        /// <returns>所有符合过滤规则的文件完整路径的列表</returns>
        public static List<string> LoopFiles(string path, string searchPattern = "*")
        {
            return LoopFiles(path,-1, searchPattern);
        }

        /// <summary>
        /// 递归遍历目录以及子目录中的所有文件
        /// </summary>
        /// <param name="path">要遍历的目录路径</param>
        /// <param name="maxDepth">遍历的最大深度，-1 表示遍历到没有目录为止</param>
        /// <param name="searchPattern">文件过滤规则，例如 "*.txt"，默认为 "*"</param>
        /// <returns>所有符合过滤规则的文件完整路径的列表</returns>
        public static List<string> LoopFiles(string path, int maxDepth = -1, string searchPattern = "*")
        {
            List<string> files = new List<string>();

            try
            {
                // 判断目录是否存在
                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"目录 {path} 不存在");
                }

                // 如果已经达到最大深度，则直接返回结果
                if (maxDepth == 0)
                {
                    return files;
                }

                // 获取目录中的所有文件和子目录
                string[] subdirectories = Directory.GetDirectories(path);
                string[] allFiles = Directory.GetFiles(path, searchPattern);

                // 将所有符合过滤规则的文件添加到结果列表中
                foreach (string file in allFiles)
                {
                    files.Add(file);
                }

                // 递归遍历子目录
                foreach (string subdirectory in subdirectories)
                {
                    List<string> subdirectoryFiles = LoopFiles(subdirectory, maxDepth - 1, searchPattern);
                    files.AddRange(subdirectoryFiles);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"遍历目录 {path} 中的文件时发生错误：{ex.Message}", ex);
            }

            return files;
        }

        /// <summary>
        /// 清空文件夹
        /// 注意：清空文件夹时不会判断文件夹是否为空，如果不空则递归删除子文件或文件夹
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns>是否成功清空文件夹</returns>
        public static bool Clean(string dirPath)
        {
            try
            {
                // 判断文件夹是否存在
                if (!Directory.Exists(dirPath))
                {
                    throw new DirectoryNotFoundException($"文件夹 {dirPath} 不存在");
                }

                // 遍历文件夹中的所有文件和子目录
                string[] files = Directory.GetFiles(dirPath);
                string[] subdirectories = Directory.GetDirectories(dirPath);

                // 删除所有文件
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                // 递归删除子目录
                foreach (string subdirectory in subdirectories)
                {
                    Clean(subdirectory);
                    Directory.Delete(subdirectory);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"清空文件夹 {dirPath} 时发生错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 清理空文件夹
        /// 此方法用于递归删除空的文件夹，不删除文件
        /// 如果传入的文件夹本身就是空的，删除这个文件夹
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns>是否成功清理空文件夹</returns>
        public static bool CleanEmpty(string dirPath)
        {
            try
            {
                // 判断文件夹是否存在
                if (!Directory.Exists(dirPath))
                {
                    throw new DirectoryNotFoundException($"文件夹 {dirPath} 不存在");
                }

                // 遍历文件夹中的所有子目录
                string[] subdirectories = Directory.GetDirectories(dirPath);

                // 递归处理子目录
                foreach (string subdirectory in subdirectories)
                {
                    CleanEmpty(subdirectory);
                }

                // 删除空的子目录和文件夹本身
                if (subdirectories.Length == 0 && Directory.GetFiles(dirPath).Length == 0)
                {
                    Directory.Delete(dirPath);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"清空文件夹 {dirPath} 时发生错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 在默认临时文件目录下创建临时文件
        /// </summary>
        /// <returns>临时文件全路径</returns>
        public static string CreateTempFile()
        {
            // 获取默认临时文件目录
            string tempDir = Path.GetTempPath();

            // 创建临时文件
            string tempFile = Path.Combine(tempDir, Path.GetRandomFileName());
            File.Create(tempFile).Close();

            return tempFile;
        }


        /// <summary>
        /// 列出指定路径下的目录和文件
        /// </summary>
        /// <param name="path">目录绝对路径或者相对路径</param>
        /// <returns>文件列表（包含目录）</returns>
        public static List<string> Ls(string path)
        {
            // 如果输入的是相对路径，转化为绝对路径
            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(path);
            }

            List<string> result = new List<string>();

            // 列出目录中的文件和子目录
            foreach (string file in Directory.GetFiles(path))
            {
                result.Add(new FileInfo(file).Name);
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                result.Add(new DirectoryInfo(directory).Name + "/");
            }

            return result;
        }

        /// <summary>
        /// 创建文件及其父目录
        /// 
        /// 如果这个文件存在，直接返回这个文件
        /// </summary>
        /// <param name="path">文件绝对路径或者相对路径</param>
        /// <returns>创建或者获取到的文件对象</returns>
        public static FileInfo Touch(string path)
        {
            // 如果输入的是相对路径，转化为绝对路径
            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(path);
            }

            FileInfo file = new FileInfo(path);

            // 如果文件存在，则直接返回这个文件对象
            if (file.Exists)
            {
                return file;
            }

            // 创建文件父目录
            Directory.CreateDirectory(file.Directory.FullName);

            // 创建文件并返回文件对象
            using (FileStream fs = file.Create())
            {
                fs.Close();
            }

            return file;
        }


        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="src">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        public static void Cp(string src, string dest)
        {
            try
            {
                File.Copy(src, dest);
            }
            catch (Exception ex)
            {
                throw new Exception($"拷贝文件 {src} 到 {dest} 失败：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 复制文件或目录
        /// 
        /// 描述：
        ///     1、src和dest都为目录，则将src目录及其目录下所有文件目录拷贝到dest下
        ///     2、src和dest都为文件，直接复制，名字为dest
        ///     3、src为文件，dest为目录，将src拷贝到dest目录下
        /// </summary>
        /// <param name="src">源文件或目录</param>
        /// <param name="dest">目标文件或目录，目标不存在会自动创建（目录、文件都创建）</param>
        /// <param name="isOverride">是否覆盖</param>
        /// <returns>是否成功复制</returns>
        public static bool Copy(string src, string dest, bool isOverride)
        {
            try
            {
                if (File.Exists(src))
                {
                    // 如果源文件存在，处理文件复制
                    FileInfo fileInfo = new FileInfo(src);

                    if (!Directory.Exists(Path.GetDirectoryName(dest)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    }

                    if (File.Exists(dest))
                    {
                        if (isOverride)
                        {
                            File.Delete(dest);
                        }
                        else
                        {
                            return false;
                        }
                    }

                    fileInfo.CopyTo(dest);

                    return true;
                }
                else if (Directory.Exists(src))
                {
                    // 如果源目录存在，处理目录复制
                    DirectoryInfo srcDir = new DirectoryInfo(src);
                    DirectoryInfo destDir = new DirectoryInfo(dest);

                    if (!destDir.Exists)
                    {
                        destDir.Create();
                    }

                    foreach (FileInfo file in srcDir.GetFiles())
                    {
                        file.CopyTo(Path.Combine(destDir.FullName, file.Name), isOverride);
                    }

                    foreach (DirectoryInfo subDir in srcDir.GetDirectories())
                    {
                        Copy(subDir.FullName, Path.Combine(destDir.FullName, subDir.Name), isOverride);
                    }

                    return true;
                }
                else
                {
                    // 如果源既不是文件也不是目录，抛出异常
                    throw new ArgumentException($"复制源 {src} 不是文件也不是目录");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"复制 {src} 到 {dest} 时发生错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 移动文件或重命名文件
        /// </summary>
        /// <param name="src">源文件路径</param>
        /// <param name="dest">目标文件路径</param>
        public static void Mv(string src, string dest)
        {
            try
            {
                File.Move(src, dest);
            }
            catch (Exception ex)
            {
                throw new Exception($"移动/重命名文件 {src} 到 {dest} 失败：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 移动文件或者目录
        /// </summary>
        /// <param name="src">源文件或者目录</param>
        /// <param name="dest">目标文件或者目录</param>
        /// <param name="isOverride">是否覆盖目标，只有目标为文件才覆盖</param>
        /// <returns></returns>
        public static bool Move(string src, string dest, bool isOverride)
        {
            try
            {
                if (File.Exists(src))
                {
                    // 如果源为文件，直接移动
                    if (File.Exists(dest))
                    {
                        // 如果目标已存在
                        if (isOverride)
                        {
                            File.Delete(dest);
                        }
                        else
                        {
                            return false;
                        }
                    }

                    FileInfo fileInfo = new FileInfo(src);
                    fileInfo.MoveTo(dest);
                }
                else if (Directory.Exists(src))
                {
                    // 如果源为目录，先复制，再删除源
                    if (Directory.Exists(dest))
                    {
                        // 如果目标已存在
                        if (isOverride)
                        {
                            Directory.Delete(dest, true);
                        }
                        else
                        {
                            return false;
                        }
                    }

                    Directory.Move(src, dest);
                }
                else
                {
                    // 如果既不是文件也不是目录，抛出异常
                    throw new ArgumentException($"移动源 {src} 不是文件也不是目录");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"移动 {src} 到 {dest} 时发生错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 修改文件或目录的文件名，不变更路径，只是简单修改文件名
        /// 
        /// 说明：
        ///     1、isRetainExt为true时，保留原扩展名。如 FileUtil.Rename(file, "aaa", true, false) xx/xx.png =》xx/aaa.png
        ///     2、isRetainExt为false时，不保留原扩展名，需要在newName中说明扩展名。如 FileUtil.Rename(file, "aaa.jpg", false, false) xx/xx.png =》xx/aaa.jpg
        /// </summary>
        /// <param name="file">被修改的文件</param>
        /// <param name="newName">新的文件名</param>
        /// <param name="isRetainExt">是否保留原文件的扩展名，如果保留，则newName不需要加扩展名</param>
        /// <param name="isOverride">是否覆盖目标文件</param>
        /// <returns>目标文件</returns>
        /// <summary>
        /// 修改文件或目录的文件名，不变更路径，只是简单修改文件名
        /// 
        /// 说明：
        ///     1、isRetainExt为true时，保留原扩展名。如 FileUtil.Rename(file, "aaa", true, false) xx/xx.png =》xx/aaa.png
        ///     2、isRetainExt为false时，不保留原扩展名，需要在newName中说明扩展名。如 FileUtil.Rename(file, "aaa.jpg", false, false) xx/xx.png =》xx/aaa.jpg
        /// </summary>
        /// <param name="file">被修改的文件</param>
        /// <param name="newName">新的文件名</param>
        /// <param name="isRetainExt">是否保留原文件的扩展名，如果保留，则newName不需要加扩展名</param>
        /// <param name="isOverride">是否覆盖目标文件</param>
        /// <returns>目标文件</returns>
        public static FileInfo Rename(FileInfo file, string newName, bool isRetainExt, bool isOverride)
        {
            try
            {
                string dir = file.Directory.FullName;
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.Name);
                string ext = Path.GetExtension(file.Name);

                if (isRetainExt)
                {
                    newName += ext;
                }

                string dest = Path.Combine(dir, newName);

                if (File.Exists(dest))
                {
                    if (isOverride)
                    {
                        File.Delete(dest);
                    }
                    else
                    {
                        throw new ArgumentException($"目标文件 {dest} 已存在，无法重命名");
                    }
                }

                file.MoveTo(dest);
                return new FileInfo(dest);
            }
            catch (Exception ex)
            {
                throw new Exception($"重命名文件 {file.FullName} 为 {newName} 时发生错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetAbsolutePath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 判断给定路径是否是绝对路径
        /// </summary>
        /// <param name="path">给定路径</param>
        /// <returns>是否是绝对路径</returns>
        public static bool IsAbsolutePath(string path)
        {
            // 如果是 Windows 系统
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // 判断路径是否是以盘符开头，例如：C:\ 或者 D:\ 这样的路径就是绝对路径
                return path.Length > 1 && path[1] == ':' && Path.IsPathRooted(path);
            }
            else
            {
                // 如果是 Linux 或者 macOS 系统，则判断路径是否以 / 开头
                return path.StartsWith("/");
            }
        }

        /// <summary>
        /// 判断是否为目录，如果 path 为 null，则返回 false
        /// </summary>
        /// <param name="path">给定路径</param>
        /// <returns>是否为目录</returns>
        public static bool IsDirectory(string path)
        {
            if (path == null)
            {
                return false;
            }

            FileAttributes attr = File.GetAttributes(path);
            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        /// <summary>
        /// 判断是否为文件，如果 path 为 null，则返回 false
        /// </summary>
        /// <param name="path">给定路径</param>
        /// <returns>是否为文件</returns>
        public static bool IsFile(string path)
        {
            if (path == null)
            {
                return false;
            }

            FileAttributes attr = File.GetAttributes(path);
            return (attr & FileAttributes.Directory) != FileAttributes.Directory;
        }

        /// <summary>
        /// 比较两个文件内容是否相同
        /// 
        /// 首先比较长度，长度一致再比较内容，比较内容采用按行读取，每行比较
        /// </summary>
        /// <param name="file1">文件1</param>
        /// <param name="file2">文件2</param>
        /// <returns>是否相同</returns>
        public static bool Equals(FileInfo file1, FileInfo file2)
        {
            if (file1.Length != file2.Length)
            {
                return false;
            }

            using (var reader1 = new StreamReader(file1.OpenRead()))
            using (var reader2 = new StreamReader(file2.OpenRead()))
            {
                string line1 = null;
                string line2 = null;
                while ((line1 = reader1.ReadLine()) != null)
                {
                    line2 = reader2.ReadLine();
                    if (line2 == null || !line1.Equals(line2))
                    {
                        return false;
                    }
                }

                // 检查第二个文件是否还有剩余行
                return reader2.ReadLine() == null;
            }
        }

        /// <summary>
        /// 修复路径
        /// 
        /// 如果原路径尾部有分隔符，则保留为标准分隔符（/），否则不保留
        /// 
        /// 说明：
        ///     1. 统一用 /
        ///     2. 多个 / 转换为一个 /
        ///     3. 去除左边空格
        ///     4. .. 和.转换为绝对路径，当..多于已有路径时，直接返回根路径
        ///     
        /// 示例：
        ///     "/foo//" =》 "/foo/"
        ///     "/foo/./" =》 "/foo/"
        ///     "/foo/../bar" =》 "/bar"
        ///     "/foo/../bar/" =》 "/bar/"
        ///     "/foo/../bar/../baz" =》 "/baz"
        ///     "foo/bar/.." =》 "foo"
        ///     "foo/../bar" =》 "bar"
        ///     "foo/../../bar" =》 "bar"
        /// </summary>
        /// <param name="path">原路径</param>
        /// <returns>修复后的路径</returns>
        public static string Normalize(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            // 统一使用 '/'
            path = path.Replace("\\", "/");

            // 去除左边空格
            path = path.TrimStart();

            // 多个 / 转换为一个 /
            while (path.Contains("//"))
            {
                path = path.Replace("//", "/");
            }

            // 以 / 结尾的路径是目录
            bool isDir = path.EndsWith("/");

            // 将路径拆分为若干段
            string[] parts = path.Split('/');
            List<string> list = new List<string>();

            // 根据 .. 和 . 进行路径转换
            foreach (string part in parts)
            {
                if (part == ".")
                {
                    continue;
                }
                else if (part == "..")
                {
                    if (list.Count > 0)
                    {
                        list.RemoveAt(list.Count - 1);
                    }
                    else
                    {
                        // .. 多于已有路径时，直接返回根路径
                        return "/";
                    }
                }
                else
                {
                    list.Add(part);
                }
            }

            string result = string.Join("/", list);

            // 如果原路径尾部有分隔符，则保留为标准分隔符（/）
            if (isDir && !result.EndsWith("/"))
            {
                result += "/";
            }

            return result;
        }

        /// <summary>
        /// 获得相对子路径，忽略大小写
        /// 
        /// 示例：
        ///     dirPath: d:/aaa/bbb    filePath: d:/aaa/bbb/ccc     =》    ccc
        ///     dirPath: d:/Aaa/bbb    filePath: d:/aaa/bbb/ccc.txt     =》    ccc.txt
        ///     dirPath: d:/Aaa/bbb    filePath: d:/aaa/bbb/     =》    ""
        /// </summary>
        /// <param name="dirPath">绝对父路径</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>相对子路径</returns>
        public static string SubPath(string dirPath, string filePath)
        {
            if (dirPath == null || filePath == null)
            {
                throw new ArgumentNullException("dirPath or filePath is null");
            }

            dirPath = dirPath.Trim();
            filePath = filePath.Trim();

            if (dirPath.Length == 0 || filePath.Length == 0)
            {
                return "";
            }

            dirPath = Normalize(dirPath);
            filePath = Normalize(filePath);

            if (!filePath.StartsWith(dirPath, StringComparison.OrdinalIgnoreCase))
            {
                return "";
            }

            int startIndex = dirPath.Length;
            if (dirPath.Length < filePath.Length && filePath[startIndex] == Path.DirectorySeparatorChar)
            {
                startIndex++;
            }

            return filePath.Substring(startIndex);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void Rm(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw new Exception($"删除文件 {path} 失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void Mkdir(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception($"创建目录 {path} 失败：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void Rmdir(string path)
        {
            try
            {
                Directory.Delete(path);
                Console.WriteLine($"目录 {path} 已成功删除");
            }
            catch (Exception ex)
            {
                throw new Exception($"删除目录 {path} 失败：{ex.Message}",ex);
            }
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件名</returns>
        public static string GetFileName(FileInfo file)
        {
            if (file == null)
            {
                return null;
            }
            return file.Name;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="filePath">文件</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }

            int index = filePath.LastIndexOf('/');
            if (index == -1)
            {
                return filePath;
            }

            return filePath.Substring(index + 1);
        }

        /// <summary>
        /// 获取后缀名，扩展名不带“.”
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件后缀名</returns>
        public static string GetFileSuffix(FileInfo file)
        {
            return file.Extension.TrimStart('.');
        }

        /// <summary>
        /// 获取后缀名，扩展名不带“.”
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件后缀名</returns>
        public static string GetFileSuffix(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            int lastDotIndex = filePath.LastIndexOf('.');
            if (lastDotIndex < 0 || lastDotIndex >= filePath.Length - 1)
            {
                return string.Empty;
            }

            return filePath.Substring(lastDotIndex + 1);
        }


        /// <summary>
        /// 获取文件名，不带扩展名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件名</returns>
        public static string GetFilePrefix(FileInfo file)
        {
            if (file == null)
            {
                return null;
            }
            string fileName = file.Name;
            int lastIndex = fileName.LastIndexOf('.');
            if (lastIndex <= 0)
            {
                return fileName;
            }
            return fileName.Substring(0, lastIndex);
        }


        /// <summary>
        /// 获取文件名，不带扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名</returns>
        public static string GetFilePrefix(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            return file.Name.Substring(0, file.Name.LastIndexOf('.'));
        }

        /// <summary>
        /// 文件流头部信息获得文件类型
        /// 
        /// 说明：
        ///     1、无法识别类型默认按照扩展名识别
        ///     2、xls、doc、msi、ppt、vsd头信息无法区分，按照扩展名区分
        ///     3、zip可能为docx、xlsx、pptx、jar、war头信息无法区分，按照扩展名区分
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>类型，文件的扩展名，未找到为null</returns>
        public static string GetType(FileInfo file)
        {
            return FileTypeUtil.GetType(file);
        }

        /// <summary>
        /// 获得输入流
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>输入流</returns>
        public static Stream GetInputStream(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            return fs;
        }

        /// <summary>
        /// 获得输入流
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>输入流</returns>
        public static Stream GetInputStream(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            return fs;
        }

        /// <summary>
        /// 获得BOM输入流，用于处理带BOM头的文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>BOM输入流</returns>
        public static StreamReader GetBOMInputStream(FileInfo file, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new StreamReader(stream, encoding, true);
            reader.Peek();
            if (reader.CurrentEncoding.Equals(Encoding.UTF8))
            {
                byte[] preamble = reader.CurrentEncoding.GetPreamble();
                bool bomPresent = true;
                if (preamble.Length > 0)
                {
                    byte[] header = new byte[preamble.Length];
                    int readCount = reader.BaseStream.Read(header, 0, header.Length);
                    for (int i = 0; i < header.Length; i++)
                    {
                        if (header[i] != preamble[i])
                        {
                            bomPresent = false;
                            break;
                        }
                    }
                }
                else
                {
                    bomPresent = false;
                }
                if (!bomPresent)
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                }
            }
            return reader;
        }

        /// <summary>
        /// 获得一个文件读取流
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>文件读取流</returns>
        public static StreamReader GetReader(FileInfo file, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return new StreamReader(file.FullName, encoding);
        }


        /// <summary>
        /// 读取文件所有数据
        /// 
        /// 文件的长度不能超过int.MaxValue
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>字节码</returns>
        public static byte[] ReadBytes(FileInfo file)
        {
            // 获取文件长度
            long fileSize = file.Length;
            if (fileSize > int.MaxValue)
            {
                throw new IOException("File is too large");
            }

            // 打开文件流
            using (FileStream fs = file.OpenRead())
            {
                byte[] buffer = new byte[fileSize];
                int readLength = fs.Read(buffer, 0, (int)fileSize);
                if (readLength != fileSize)
                {
                    throw new IOException("Read file failed");
                }
                return buffer;
            }
        }


        /// <summary>
        /// 读取文件所有数据
        /// 
        /// 文件的长度不能超过int.MaxValue
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>字节码</returns>
        public static byte[] ReadBytes(string path)
        {
            // 打开文件，获得文件流
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // 检查文件长度是否超过int.MaxValue
                if (fs.Length > int.MaxValue)
                {
                    throw new ArgumentException($"File length {fs.Length} exceeds the maximum value {int.MaxValue}.");
                }

                // 读取文件数据到内存缓冲区
                byte[] buffer = new byte[fs.Length];
                int bytesRead = fs.Read(buffer, 0, buffer.Length);

                // 如果读取的字节数不等于文件长度，说明读取不完整，抛出异常
                if (bytesRead != fs.Length)
                {
                    throw new IOException($"Could not read entire file, expected {fs.Length} bytes, but only {bytesRead} bytes read.");
                }

                // 返回读取的数据
                return buffer;
            }
        }


        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>内容</returns>
        public static string ReadString(FileInfo file, Encoding encoding = null)
        {

            if (encoding == null)
            {
                encoding = Encoding.UTF8; // 如果未指定编码格式，则使用默认的UTF-8编码
            }

            using (var stream = file.OpenRead()) // 打开文件输入流
            {
                using (var reader = new StreamReader(stream, encoding)) // 创建文件读取器
                {
                    return reader.ReadToEnd(); // 读取全部内容并返回
                }
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>内容</returns>
        public static string ReadString(string path, Encoding encoding = null)
        {
            return ReadString(new FileInfo(path), encoding); // 直接调用另一个重载方法
        }


        /// <summary>
        /// 读取网络文件内容
        /// </summary>
        /// <param name="url">网络文件地址</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>内容</returns>
        public static string ReadString(Uri url, Encoding encoding = null)
        {
            // 如果未指定编码格式，则默认为UTF-8
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string result;
            try
            {
                // 创建WebClient对象
                using (WebClient client = new WebClient())
                {
                    // 下载指定地址的文件，并转换为字节数组
                    byte[] data = client.DownloadData(url);
                    // 将字节数组转换为字符串，并使用指定编码格式解码
                    result = encoding.GetString(data);
                }
            }
            catch
            {
                // 如果发生异常，则返回空字符串
                result = "";
            }

            return result;
        }

        /// <summary>
        /// 从文件中读取每一行数据
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path, Encoding encoding = null)
        {
            // 如果未指定编码格式，则默认为 UTF-8
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            // 读取文件所有行数据
            string[] lines = File.ReadAllLines(path, encoding);

            return lines;
        }


        /// <summary>
        /// 获得一个输出流对象
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>输出流对象</returns>
        public static Stream GetOutputStream(FileInfo file)
        {
            if (!file.Exists)
            {
                file.Create().Dispose();
            }

            return file.OpenWrite();
        }


        /// <summary>
        /// 获得一个输出流对象
        /// </summary>
        /// <param name="file">输出到的文件路径，绝对路径</param>
        /// <returns>输出流对象</returns>
        public static Stream GetOutputStream(string path)
        {
            return GetOutputStream(new FileInfo(path));
        }


        /// <summary>
        /// 获取当前系统的换行分隔符
        /// </summary>
        /// <returns>换行分隔符</returns>
        public static string GetLineSeparator()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// 将string写入文件，覆盖模式
        /// </summary>
        /// <param name="content">写入的内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>文件信息</returns>
        public static FileInfo WriteString(string content, string path, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamWriter writer = new StreamWriter(path, false, encoding))
            {
                writer.Write(content);
            }

            return new FileInfo(path);
        }


        /// <summary>
        /// 将string写入文件，追加模式
        /// </summary>
        /// <param name="content">写入的内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>文件信息</returns>
        public static FileInfo AppendString(string content, string path, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (StreamWriter writer = new StreamWriter(path, true, encoding))
            {
                writer.Write(content);
            }
            return new FileInfo(path);
        }

        /// <summary>
        /// 将列表写入文件，覆盖模式
        /// </summary>
        /// <param name="list">写入的内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>文件信息</returns>
        public static FileInfo WriteLines(List<string> list, string path, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamWriter writer = new StreamWriter(path, false, encoding))
            {
                list.ForEach(content =>
                {
                    writer.Write(content);
                });
            }

            return new FileInfo(path);
        }

        /// <summary>
        /// 将列表写入文件，追加模式
        /// </summary>
        /// <param name="list">写入的内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>文件信息</returns>
        public static FileInfo AppendLines(List<string> list, string path, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamWriter writer = new StreamWriter(path, true, encoding))
            {
                list.ForEach(content =>
                {
                    writer.Write(content);
                });
            }

            return new FileInfo(path);
        }

        /// <summary>
        /// 写入数据到文件
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="file">目标文件</param>
        /// <param name="offset">数据开始位置</param>
        /// <param name="len">数据长度</param>
        /// <param name="isAppend">是否追加模式</param>
        /// <returns>文件信息</returns>
        public static FileInfo WriteBytes(byte[] data, FileInfo file, int offset, int len, bool isAppend)
        {
            if (data == null || data.Length == 0)
            {
                return file;
            }
            if (file == null)
            {
                return null;
            }
            if (offset < 0 || len < 0 || offset + len > data.Length)
            {
                throw new ArgumentOutOfRangeException("offset 或 len 不在 data 的范围内");
            }

            using (FileStream stream = new FileStream(file.FullName, isAppend ? FileMode.Append : FileMode.Create, FileAccess.Write))
            {
                stream.Write(data, offset, len);
            }

            return file;
        }

        /// <summary>
        /// 清除文件名中的在Windows下不支持的非法字符，包括： \ / : * ? " &lt; &gt; |
        /// </summary>
        /// <param name="fileName">文件名（必须不包括路径，否则路径符将被替换）</param>
        /// <returns>清理后的文件名</returns>
        public static string CleanInvalid(string fileName)
        {
            // 获取非法字符数组
            var invalidChars = Path.GetInvalidFileNameChars();

            // 循环替换非法字符
            foreach (var c in invalidChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }

        /// <summary>
        /// 文件名中是否包含在Windows下不支持的非法字符，包括： \ / : * ? " &lt; &gt; |
        /// </summary>
        /// <param name="fileName">文件名（必须不包括路径，否则路径符将被替换）</param>
        /// <returns>是否包含非法字符</returns>
        public static bool ContainsInvalid(string fileName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                if (fileName.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 根据文件扩展名获得MimeType
        /// </summary>
        /// <param name="filePath">文件路径或文件名</param>
        /// <returns>MimeType</returns>
        public static string GetMimeType(string filePath)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(filePath).ToLower();
            switch (ext)
            {
                case ".png":
                    mimeType = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".bmp":
                    mimeType = "image/bmp";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                case ".txt":
                case ".cs":
                    mimeType = "text/plain";
                    break;
                case ".pdf":
                    mimeType = "application/pdf";
                    break;
                default:
                    break;
            }
            return mimeType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 压缩工具
    /// </summary>
    public class ZipUtil
    {
        /// <summary>
        /// 压缩文件或目录
        /// </summary>
        /// <param name="sourcePath">要压缩的文件或目录路径</param>
        /// <param name="destinationPath">压缩文件保存的路径</param>
        public static void Zip(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                ZipFile.CreateFromDirectory(Path.GetDirectoryName(sourcePath), destinationPath);
            }
            else if (Directory.Exists(sourcePath))
            {
                ZipFile.CreateFromDirectory(sourcePath, destinationPath);
            }
            else
            {
                throw new FileNotFoundException("Source file or directory not found");
            }
        }

        /// <summary>
        /// 解压缩文件到指定目录
        /// </summary>
        /// <param name="sourcePath">要解压的文件路径</param>
        /// <param name="destinationPath">解压后文件保存的目录</param>
        public static void Unzip(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                ZipFile.ExtractToDirectory(sourcePath, destinationPath);
            }
            else
            {
                throw new FileNotFoundException("Source file not found");
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="source">要压缩的字符串</param>
        /// <returns>压缩后的字符串</returns>
        public static string ZipString(string source)
        {
            byte[] sourceBytes = System.Text.Encoding.UTF8.GetBytes(source);

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress))
                {
                    zs.Write(sourceBytes, 0, sourceBytes.Length);
                }

                byte[] compressedBytes = ms.ToArray();
                return System.Convert.ToBase64String(compressedBytes);
            }
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="source">要解压的字符串</param>
        /// <returns>解压后的字符串</returns>
        public static string UnzipString(string source)
        {
            byte[] compressedBytes = System.Convert.FromBase64String(source);

            using (MemoryStream ms = new MemoryStream(compressedBytes))
            {
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(zs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}

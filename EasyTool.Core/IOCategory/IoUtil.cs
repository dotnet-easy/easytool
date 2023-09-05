using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Io流处理工具类
    /// </summary>
    public static class IoUtil
    {
        /// <summary>
        /// 读取文件的所有行到一个字符串数组中
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>字符串数组，其中包含文件的所有行。</returns>
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// 将字符串数组写入文件，覆盖原有内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="lines">待写入的字符串数组</param>
        public static void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// 读取整个文件到一个字符串中
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的所有内容</returns>
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// 将字符串写入文件，覆盖原有内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="text">待写入的字符串</param>
        public static void WriteAllText(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// 读取二进制数据到一个字节数组中
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// 将字节数组写入二进制文件，覆盖原有内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="bytes">待写入的字节数组</param>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// 读取指定 URL 的文本内容
        /// </summary>
        /// <param name="url">URL 地址</param>
        /// <returns>URL 返回的文本内容</returns>
        public static string ReadUrl(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        /// <summary>
        /// 将字符串写入指定 URL
        /// </summary>
        /// <param name="url">URL 地址</param>
        /// <param name="text">待写入的字符串</param>
        public static void WriteUrl(string url, string text)
        {
            WebClient client = new WebClient();
            client.UploadString(url, text);
        }

        /// <summary>
        /// 读取网络流到一个字符串中
        /// </summary>
        /// <param name="stream">网络流</param>
        /// <returns>网络流的所有内容</returns>
        public static string ReadStream(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 将字符串写入网络流
        /// </summary>
        /// <param name="stream">网络流</param>
        /// <param name="text">待写入的字符串</param>
        public static void WriteStream(Stream stream, string text)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(text);
            }
        }

        /// <summary>
        /// 读取二进制数据到一个内存流中
        /// </summary>
        /// <param name="bytes">二进制数据</param>
        /// <returns>内存流，其中包含输入的二进制数据</returns>
        public static MemoryStream ReadMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 将二进制数据写入一个内存流中
        /// </summary>
        /// <param name="stream">内存流</param>
        /// <param name="bytes">待写入的字节数组</param>
        public static void WriteMemoryStream(MemoryStream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 将一个字符串转换为字节数组
        /// </summary>
        /// <param name="text">待转换的字符串</param>
        /// <returns>字节数组，其中包含输入字符串的编码数据</returns>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// 将一个字节数组转换为字符串
        /// </summary>
        /// <param name="bytes">待转换的字节数组</param>
        /// <returns>字符串，其中包含输入字节数组的编码数据</returns>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// IP地址工具类
    /// </summary>
    public class IpUtil
    {
        /// <summary>
        /// 判断是否是ipv4格式
        /// </summary>
        /// <param name="str">ip地址</param>
        /// <returns>如果是ipv4地址，则为true，否则为false</returns>
        public static bool IsIpv4(string str)
        {
            return Regex.IsMatch(str, @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$");
        }

        /// <summary>
        /// 判断是否是ipv6格式
        /// </summary>
        /// <param name="str">ip地址</param>
        /// <returns>如果是ipv6地址，则为true，否则为false</returns>
        public static bool IsIpv6(string str)
        {
            return Regex.IsMatch(str,
                @"^([\da-fA-F]{1,4}:){6}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^::([\da-fA-F]{1,4}:){0,4}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:):([\da-fA-F]{1,4}:){0,3}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){2}:([\da-fA-F]{1,4}:){0,2}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){3}:([\da-fA-F]{1,4}:){0,1}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){4}:((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){7}[\da-fA-F]{1,4}$|^:((:[\da-fA-F]{1,4}){1,6}|:)$|^[\da-fA-F]{1,4}:((:[\da-fA-F]{1,4}){1,5}|:)$|^([\da-fA-F]{1,4}:){2}((:[\da-fA-F]{1,4}){1,4}|:)$|^([\da-fA-F]{1,4}:){3}((:[\da-fA-F]{1,4}){1,3}|:)$|^([\da-fA-F]{1,4}:){4}((:[\da-fA-F]{1,4}){1,2}|:)$|^([\da-fA-F]{1,4}:){5}:([\da-fA-F]{1,4})?$|^([\da-fA-F]{1,4}:){6}:$");
        }

        /// <summary>
        /// 判断是否是ip格式
        /// </summary>
        /// <param name="str">ip地址</param>
        /// <returns>如果是ip地址，则为true，否则为false</returns>
        public static bool IsIp(string str)
        {
            return Regex.IsMatch(str,
                @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){6}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^::([\da-fA-F]{1,4}:){0,4}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:):([\da-fA-F]{1,4}:){0,3}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){2}:([\da-fA-F]{1,4}:){0,2}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){3}:([\da-fA-F]{1,4}:){0,1}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){4}:((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){7}[\da-fA-F]{1,4}$|^:((:[\da-fA-F]{1,4}){1,6}|:)$|^[\da-fA-F]{1,4}:((:[\da-fA-F]{1,4}){1,5}|:)$|^([\da-fA-F]{1,4}:){2}((:[\da-fA-F]{1,4}){1,4}|:)$|^([\da-fA-F]{1,4}:){3}((:[\da-fA-F]{1,4}){1,3}|:)$|^([\da-fA-F]{1,4}:){4}((:[\da-fA-F]{1,4}){1,2}|:)$|^([\da-fA-F]{1,4}:){5}:([\da-fA-F]{1,4})?$|^([\da-fA-F]{1,4}:){6}:$");
        }

        /// <summary>
        /// 判断IPv4是否是私有地址
        /// </summary>
        /// <param name="ip">IPv4字符串</param>
        /// <returns>如果是私有地址返回true，否则返回false</returns>
        public static bool IsPrivateIpv4(string ip)
        {
            if (!IsIpv4(ip)) throw new ArgumentException("不是有效的IPv4地址");
            var ipParts = Array.ConvertAll(ip.Split('.'), s => int.Parse(s));
            if (ipParts[0] == 10 ||
                (ipParts[0] == 172 && (ipParts[1] >= 16 && ipParts[1] <= 31)) ||
                (ipParts[0] == 192 && ipParts[1] == 168))
                return true;

            return false;

            // 10.0.0.0 – 10.255.255.255
            // 172.16.0.0 – 172.31.255.255
            // 192.168.0.0 – 192.168.255.255
        }

        /// <summary>
        /// 判断IPv6是否是私有地址
        /// </summary>
        /// <param name="ip">IPv6字符串</param>
        /// <returns>如果是私有地址返回true，否则返回false</returns>
        public static bool IsPrivateIpv6(string ip)
        {
            if (!IsIpv6(ip)) throw new ArgumentException("不是有效的IPv6地址");
            // fd00::/8
            return ip.StartsWith("fd", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 将IPv4转换为整数
        /// </summary>
        /// <param name="ip">IPv4字符串</param>
        /// <returns>代表IPv4的整数</returns>
        public static uint Ipv4ToInt(string ip)
        {
            if (!IsIpv4(ip)) throw new ArgumentException("不是有效的IPv4地址");
            var ipParts = ip.Split('.');
            uint intIP = 0;
            for (int i = 0; i < 4; i++)
            {
                intIP = intIP << 8;
                intIP += uint.Parse(ipParts[i]);
            }

            return intIP;
        }

        /// <summary>
        /// 将整数转换为IPv4
        /// </summary>
        /// <param name="intIP">代表IPv4的整数</param>
        /// <returns>IPv4字符串</returns>
        public static string IntToIpv4(uint intIP)
        {
            var ipParts = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                ipParts[3 - i] = (byte)(intIP & 255);
                intIP = intIP >> 8;
            }

            return string.Join('.', ipParts);
        }

        /// <summary>
        /// 将IPv6转换为两个ulong
        /// </summary>
        /// <param name="ip">IPv6字符串</param>
        /// <returns>代表IPv6的两个ulong元组</returns>
        public static (ulong High, ulong Low) Ipv6ToUlongs(string ip)
        {
            if (!IsIpv6(ip)) throw new ArgumentException("不是有效的IPv6地址");
            var ipBytes = IPAddress.Parse(ip).GetAddressBytes();
            var high = BitConverter.ToUInt64(ipBytes, 0);
            var low = BitConverter.ToUInt64(ipBytes, 8);
            return (high, low);
        }

        /// <summary>
        /// 将两个ulong转换为IPv6
        /// </summary>
        /// <param name="high">IPv6的高64位</param>
        /// <param name="low">IPv6的低64位</param>
        /// <returns>IPv6字符串</returns>
        public static string UlongsToIpv6(ulong high, ulong low)
        {
            byte[] ipBytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(high), 0, ipBytes, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(low), 0, ipBytes, 8, 8);
            return new IPAddress(ipBytes).ToString();
        }
    }
}
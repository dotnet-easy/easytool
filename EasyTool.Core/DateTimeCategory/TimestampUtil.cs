using System;

namespace EasyTool
{
    /// <summary>
    /// 时间戳处理工具类
    /// </summary>
    public static class TimestampUtil
    {
        /// <summary>
        /// 获取当前时间戳（毫秒级）
        /// </summary>
        /// <returns>当前时间戳（毫秒级）</returns>
        public static long GetCurrentTimestamp()
        {
            DateTime dt = DateTime.UtcNow;
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 将时间戳（毫秒级）转换为 DateTime 类型
        /// </summary>
        /// <param name="timestamp">时间戳（毫秒级）</param>
        /// <returns>转换后的 DateTime 类型</returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 将 DateTime 类型转换为时间戳（毫秒级）
        /// </summary>
        /// <param name="dateTime">DateTime 类型</param>
        /// <returns>转换后的时间戳（毫秒级）</returns>
        public static long ConvertToTimestamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 获取当前时间戳（秒级）
        /// </summary>
        /// <returns>当前时间戳（秒级）</returns>
        public static long GetCurrentTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// 将时间戳（秒级）转换为 DateTime 类型
        /// </summary>
        /// <param name="timestamp">时间戳（秒级）</param>
        /// <returns>转换后的 DateTime 类型</returns>
        public static DateTime ConvertToDateTimeSeconds(long timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
        }

        /// <summary>
        /// 将 DateTime 类型转换为时间戳（秒级）
        /// </summary>
        /// <param name="dateTime">DateTime 类型</param>
        /// <returns>转换后的时间戳（秒级）</returns>
        public static long ConvertToTimestampSeconds(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}

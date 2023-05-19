using System;
using System.Diagnostics;

namespace EasyTool
{
    /// <summary>
    /// 计时器工具类，提供各种计时和时间间隔计算的方法。
    /// </summary>
    public class TimerUtil
    {
        /// <summary>
        /// 记录程序启动时间。
        /// </summary>
        private static readonly DateTime _startTime = DateTime.Now;

        /// <summary>
        /// 获取当前时间戳，即 Unix 时间戳，精确到毫秒。
        /// </summary>
        /// <returns>当前时间戳。</returns>
        public static long GetCurrentTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 获取程序启动时间。
        /// </summary>
        /// <returns>程序启动时间。</returns>
        public static DateTime GetStartTime()
        {
            return _startTime;
        }

        /// <summary>
        /// 获取当前时间距离程序启动时间的时间间隔。
        /// </summary>
        /// <returns>当前时间距离程序启动时间的时间间隔。</returns>
        public static TimeSpan GetElapsedTime()
        {
            return DateTime.Now - _startTime;
        }

        /// <summary>
        /// 创建一个新的 Stopwatch 并启动计时。
        /// </summary>
        /// <returns>一个新的 Stopwatch。</returns>
        public static Stopwatch StartNew()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        /// <summary>
        /// 计算指定操作的执行时间。
        /// </summary>
        /// <param name="action">要执行的操作。</param>
        /// <returns>操作执行的时间。</returns>
        public static TimeSpan Measure(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        /// <summary>
        /// 计算指定操作的执行时间，并输出执行结果。
        /// </summary>
        /// <param name="action">要执行的操作。</param>
        /// <param name="description">执行结果的描述。</param>
        public static void MeasureAndPrint(Action action, string description)
        {
            TimeSpan elapsedTime = Measure(action);
            Console.WriteLine($"{description}: {elapsedTime.TotalMilliseconds}ms");
        }

        /// <summary>
        /// 计算指定操作的执行时间，并输出执行结果到指定文件。
        /// </summary>
        /// <param name="action">要执行的操作。</param>
        /// <param name="fileName">输出结果的文件名。</param>
        public static void MeasureAndSave(Action action, string fileName)
        {
            TimeSpan elapsedTime = Measure(action);
            System.IO.File.WriteAllText(fileName, elapsedTime.TotalMilliseconds.ToString());
        }

        /// <summary>
        /// 计算指定操作的执行时间，并将执行结果添加到指定日志文件的末尾。
        /// </summary>
        /// <param name="action">要执行的操作。</param>
        /// <param name="fileName">日志文件名。</param>
        public static void MeasureAndLog(Action action, string fileName)
        {
            TimeSpan elapsedTime = Measure(action);
            System.IO.File.AppendAllText(fileName, $"{DateTime.Now}: {elapsedTime.TotalMilliseconds}ms{Environment.NewLine}");
        }

        /// <summary>
        /// 等待指定的时间
        /// </summary>
        /// <param name="milliseconds">要等待的毫秒数。</param>
        public static void Wait(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// 计算两个时间的时间间隔。
        /// </summary>
        /// <param name="time1">第一个时间。</param>
        /// <param name="time2">第二个时间。</param>
        /// <returns>两个时间的时间间隔。</returns>
        public static TimeSpan GetTimeSpan(DateTime time1, DateTime time2)
        {
            return time1 - time2;
        }

        /// <summary>
        /// 计算两个时间戳的时间间隔。
        /// </summary>
        /// <param name="timestamp1">第一个时间戳。</param>
        /// <param name="timestamp2">第二个时间戳。</param>
        /// <returns>两个时间戳的时间间隔。</returns>
        public static TimeSpan GetTimeSpan(long timestamp1, long timestamp2)
        {
            DateTime time1 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp1).LocalDateTime;
            DateTime time2 = DateTimeOffset.FromUnixTimeMilliseconds(timestamp2).LocalDateTime;
            return GetTimeSpan(time1, time2);
        }

        /// <summary>
        /// 将时间间隔格式化为友好的字符串，例如 1h 20m 30s。
        /// </summary>
        /// <param name="timeSpan">要格式化的时间间隔。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string FormatTimeSpan(TimeSpan timeSpan)
        {
            int hours = timeSpan.Days * 24 + timeSpan.Hours;
            string formattedTimeSpan = $"{hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
            return formattedTimeSpan;
        }

    }
}

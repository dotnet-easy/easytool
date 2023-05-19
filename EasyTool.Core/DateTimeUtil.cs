using System;
using System.Collections.Generic;
using System.Globalization;

namespace EasyTool
{
    /// <summary>
    /// 提供各种日期操作和计算的工具类。
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 获取当前日期的星期几。
        /// </summary>
        /// <returns>星期几的枚举值。</returns>
        public static DayOfWeek GetDayOfWeek()
        {
            return DateTime.Now.DayOfWeek;
        }

        /// <summary>
        /// 获取当前日期所在周的第一天的日期。
        /// </summary>
        /// <returns>当前日期所在周的第一天的日期。</returns>
        public static DateTime GetFirstDayOfWeek()
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = firstDayOfWeek - DateTime.Now.DayOfWeek;
            if (offset > 0)
            {
                offset -= 7;
            }
            return DateTime.Now.AddDays(offset).Date;
        }

        /// <summary>
        /// 获取当前日期所在月份的第一天的日期。
        /// </summary>
        /// <returns>当前日期所在月份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfMonth()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        /// <summary>
        /// 获取当前日期所在季度的第一天的日期。
        /// </summary>
        /// <returns>当前日期所在季度的第一天的日期。</returns>
        public static DateTime GetFirstDayOfQuarter()
        {
            int quarter = (DateTime.Now.Month - 1) / 3 + 1;
            return new DateTime(DateTime.Now.Year, (quarter - 1) * 3 + 1, 1);
        }

        /// <summary>
        /// 获取当前日期所在年份的第一天的日期。
        /// </summary>
        /// <returns>当前日期所在年份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定日期所在周的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在周的第一天的日期。</returns>
        public static DateTime GetFirstDayOfWeek(DateTime date)
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = firstDayOfWeek - date.DayOfWeek;
            if (offset > 0)
            {
                offset -= 7;
            }
            return date.AddDays(offset).Date;
        }

        /// <summary>
        /// 获取指定日期所在月份的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在月份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获取指定日期所在季度的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在季度的第一天的日期。</returns>
        public static DateTime GetFirstDayOfQuarter(DateTime date)
        {
            int quarter = (date.Month - 1) / 3 + 1;
            return new DateTime(date.Year, (quarter - 1) * 3 + 1, 1);
        }

        /// <summary>
        /// 获取指定日期所在年份的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在年份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 计算指定日期和当前日期之间的天数差。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期和当前日期之间的天数差。</returns>
        public static int GetDaysBetween(DateTime date)
        {
            TimeSpan span = date - DateTime.Now;
            return span.Days;
        }

        /// <summary>
        /// 计算两个日期之间的天数差。
        /// </summary>
        /// <param name="date1">第一个日期。</param>
        /// <param name="date2">第二个日期。</param>
        /// <returns>两个日期之间的天数差。</returns>
        public static int GetDaysBetween(DateTime date1, DateTime date2)
        {
            TimeSpan span = date2 - date1;
            return span.Days;
        }

        /// <summary>
        /// 计算指定日期和当前日期之间的工作日数差。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期和当前日期之间的工作日数差。</returns>
        public static int GetWorkDaysBetween(DateTime date)
        {
            int count = 0;
            DateTime temp = DateTime.Now;
            while (temp.Date != date.Date)
            {
                if (temp.DayOfWeek != DayOfWeek.Saturday && temp.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                temp = temp.AddDays(1);
            }
            return count;
        }

        /// <summary>
        /// 计算两个日期之间的工作日数差。
        /// </summary>
        /// <param name="date1">第一个日期。</param>
        /// <param name="date2">第二个日期。</param>
        /// <returns>两个日期之间的工作日数差。</returns>
        public static int GetWorkDaysBetween(DateTime date1, DateTime date2)
        {
            int count = 0;
            DateTime temp = date1;
            while (temp.Date != date2.Date)
            {
                if (temp.DayOfWeek != DayOfWeek.Saturday && temp.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                temp = temp.AddDays(1);
            }
            return count;
        }

        /// <summary>
        /// 判断指定日期是否是工作日。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>如果是工作日，则返回 true；否则返回 false。</returns>
        public static bool IsWorkDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// 获取指定日期所在周的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在周的所有日期。</returns>
        public static List<DateTime> GetWeekDays(DateTime date)
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = firstDayOfWeek - date.DayOfWeek;
            if (offset > 0)
            {
                offset -= 7;
            }
            DateTime firstDay = date.AddDays(offset).Date;
            List<DateTime> days = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(firstDay.AddDays(i));
            }
            return days;
        }

        /// <summary>
        /// 获取指定日期所在月份的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在月份的所有日期。</returns>
        public static List<DateTime> GetMonthDays(DateTime date)
        {
            DateTime firstDay = new DateTime(date.Year, date.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            List<DateTime> days = new List<DateTime>();
            for (DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
            {
                days.Add(i);
            }
            return days;
        }

        /// <summary>
        /// 获取指定日期所在季度的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在季度的所有日期。</returns>
        public static List<DateTime> GetQuarterDays(DateTime date)
        {
            DateTime firstDay = GetFirstDayOfQuarter(date);
            DateTime lastDay = firstDay.AddMonths(3).AddDays(-1);
            List<DateTime> days = new List<DateTime>();
            for (DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
            {
                days.Add(i);
            }
            return days;
        }

        /// <summary>
        /// 获取指定日期所在年份的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在年份的所有日期。</returns>
        public static List<DateTime> GetYearDays(DateTime date)
        {
            DateTime firstDay = new DateTime(date.Year, 1, 1);
            DateTime lastDay = new DateTime(date.Year, 12, 31);
            List<DateTime> days = new List<DateTime>();
            for (DateTime i = firstDay; i <= lastDay; i = i.AddDays(1))
            {
                days.Add(i);
            }
            return days;
        }
    }
}

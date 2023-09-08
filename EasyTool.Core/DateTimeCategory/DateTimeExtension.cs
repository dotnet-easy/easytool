using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EasyTool.Extension
{
    /// <summary>
    /// 提供各种日期操作和计算的工具类。
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 获取指定日期所在周的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在周的第一天的日期。</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date) => DateTimeUtil.GetFirstDayOfWeek(date);

        /// <summary>
        /// 获取指定日期所在月份的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在月份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date) => DateTimeUtil.GetFirstDayOfMonth(date);


        /// <summary>
        /// 获取指定日期所在季度的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在季度的第一天的日期。</returns>
        public static DateTime GetFirstDayOfQuarter(this DateTime date) => DateTimeUtil.GetFirstDayOfQuarter(date);

        /// <summary>
        /// 获取指定日期所在年份的第一天的日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在年份的第一天的日期。</returns>
        public static DateTime GetFirstDayOfYear(this DateTime date) => DateTimeUtil.GetFirstDayOfYear(date);

        /// <summary>
        /// 计算指定日期和当前日期之间的天数差。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期和当前日期之间的天数差。</returns>
        public static int GetDaysBetween(this DateTime date) => DateTimeUtil.GetDaysBetween(date);

        /// <summary>
        /// 计算两个日期之间的天数差。
        /// </summary>
        /// <param name="date1">第一个日期。</param>
        /// <param name="date2">第二个日期。</param>
        /// <returns>两个日期之间的天数差。</returns>
        public static int GetDaysBetween(this DateTime date1, DateTime date2) => DateTimeUtil.GetDaysBetween(date1, date2);

        /// <summary>
        /// 计算指定日期和当前日期之间的工作日数差。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期和当前日期之间的工作日数差。</returns>
        public static int GetWorkDaysBetween(this DateTime date) => DateTimeUtil.GetWorkDaysBetween(date);

        /// <summary>
        /// 计算两个日期之间的工作日数差。
        /// </summary>
        /// <param name="date1">第一个日期。</param>
        /// <param name="date2">第二个日期。</param>
        /// <returns>两个日期之间的工作日数差。</returns>
        public static int GetWorkDaysBetween(this DateTime date1, DateTime date2) => DateTimeUtil.GetWorkDaysBetween(date1, date2);

        /// <summary>
        /// 判断指定日期是否是工作日。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>如果是工作日，则返回 true；否则返回 false。</returns>
        public static bool IsWorkDay(this DateTime date) => DateTimeUtil.IsWorkDay(date);

        /// <summary>
        /// 获取指定日期所在周的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在周的所有日期。</returns>
        public static List<DateTime> GetWeekDays(this DateTime date) => DateTimeUtil.GetWeekDays(date);

        /// <summary>
        /// 获取指定日期所在月份的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在月份的所有日期。</returns>
        public static List<DateTime> GetMonthDays(this DateTime date) => DateTimeUtil.GetMonthDays(date);

        /// <summary>
        /// 获取指定日期所在季度的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在季度的所有日期。</returns>
        public static List<DateTime> GetQuarterDays(this DateTime date) => DateTimeUtil.GetQuarterDays(date);

        /// <summary>
        /// 获取指定日期所在年份的所有日期。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns>指定日期所在年份的所有日期。</returns>
        public static List<DateTime> GetYearDays(this DateTime date) => DateTimeUtil.GetYearDays(date);
    }
}

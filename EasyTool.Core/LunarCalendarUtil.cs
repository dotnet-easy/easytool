using System;

namespace EasyTool
{
    /// <summary>
    /// 农历日期工具类
    /// </summary>
    public class LunarCalendarUtil
    {

        #region 基础数据

        /// <summary>
        /// 中文数字
        /// </summary>
        private static readonly string[] ChineseNumbers = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        /// <summary>
        /// 天干
        /// </summary>
        private static readonly string[] Gan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        /// <summary>
        /// 地支
        /// </summary>
        private static readonly string[] Zhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        /// <summary>
        /// 生肖
        /// </summary>
        private static readonly string[] Animal = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
        /// <summary>
        /// 农历月份
        /// </summary>
        private static readonly string[] MonthNames = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
        /// <summary>
        /// 农历日期头
        /// </summary>
        private static readonly string[] DayNames = { "初", "十", "廿", "三" };
        private static readonly string[] SolarTerm = {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨",
            "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑",
            "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
        };

        /// <summary>
        /// 支持查询的最小农历年份
        /// </summary>
        private const int MinYear = 1900;

        private static readonly int[] LunarMonthDays =
        {
            0x04bd8, 0x04ae0, 0x0a570, 0x054d5, 0x0d260, 0x0d950, 0x16554, 0x056a0, 0x09ad0, 0x055d2, // 1901-1910
            0x04ae0, 0x0a5b6, 0x0a4d0, 0x0d250, 0x1d255, 0x0b540, 0x0d6a0, 0x0ada2, 0x095b0, 0x14977, // 1911-1920
            0x04970, 0x0a4b0, 0x0b4b5, 0x06a50, 0x06d40, 0x1ab54, 0x02b60, 0x09570, 0x052f2, 0x04970, // 1921-1930
            0x06566, 0x0d4a0, 0x0ea50, 0x06e95, 0x05ad0, 0x02b60, 0x186e3, 0x092e0, 0x1c8d7, 0x0c950, // 1931-1940
            0x0d4a0, 0x1d8a6, 0x0b550, 0x056a0, 0x1a5b4, 0x025d0, 0x092d0, 0x0d2b2, 0x0a950, 0x0b557, // 1941-1950
            0x0a5b0, 0x14573, 0x052b0, 0x0a9a8, 0x0e950, 0x06aa0, 0x0aea6, 0x0ab50, 0x04b60, 0x0aae4, // 1951-1960
            0x0a570, 0x05260, 0x0f263, 0x0d950, 0x05b57, 0x056a0, 0x096d0, 0x04dd5, 0x04ad0, 0x0a4d0, // 1961-1970
            0x0d4d4, 0x0d250, 0x0d558, 0x0b540, 0x0b5a0, 0x195a6, 0x095b0, 0x049b0, 0x0a974, 0x0a4b0, // 1971-1980
            0x0b27a, 0x06a50, 0x06d40, 0x0af46, 0x0ab60, 0x09570, 0x04af5, 0x04970, 0x064b0, 0x074a3, // 1981-1990
            0x0ea50, 0x06b58, 0x055c0, 0x0ab60, 0x096d5, 0x092e0, 0x0c960, 0x0d954, 0x0d4a0, 0x0da50, // 1991-2000
            0x07552, 0x056a0, 0x0abb7, 0x025d0, 0x092d0, 0x0cab5, 0x0a950, 0x0b4a0, 0x0baa4, 0x0ad50, // 2001-2010
            0x055d9, 0x04ba0, 0x0a5b0, 0x15176, 0x052b0, 0x0a930, 0x07954, 0x06aa0, 0x0ad50, 0x05b52, // 2011-2020
            0x04b60, 0x0a6e6, 0x0a4e0, 0x0d260, 0x0ea65, 0x0d530, 0x05aa0, 0x076a3, 0x096d0, 0x04bd7, // 2021-2030
            0x0d520, 0x0dd45, 0x0b5a0, 0x056d0, 0x055b2, 0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, // 2031-2040
            0x06d20, 0x0ada0, 0x14b63, 0x09370, 0x04970, 0x064b0, 0x168a6, 0x0ea50, 0x06b20, 0x1a6c4, // 2041-2050
            0x0aae0, 0x0a2e0, 0x0d2e3, 0x0c960, 0x0d557, 0x0d4a0, 0x0da50, 0x05d55, 0x056a0, 0x0a6d0, // 2051-2060
            0x055d4, 0x052d0, 0x0a9b8, 0x0a950, 0x0b4a0, 0x0b6a6, 0x0ad50, 0x055a0, 0x0aba4, 0x0a5b0, // 2061-2070
            0x052b0, 0x0b273, 0x0d950, 0x05b57, 0x056d0, 0x0a9a8, 0x0e950, 0x06aa0, 0x0aea6, 0x0ab50, // 2071-2080
            0x04b60, 0x0aae4, 0x0a570, 0x05260, 0x0f263, 0x0d950, 0x05b57, 0x056a0, 0x0ada0, 0x095d0, // 2081-2090
            0x04bd5, 0x04ad0, 0x0a4d0, 0x1d0b2, 0x0d250, 0x0d520, 0x0dd45, 0x0b5a0, 0x056d0, 0x055b2, // 2091-2100
            0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, 0x06d20, 0x0ada0, 0x14b63, 0x09370, 0x04970, // 2101-2110
            0x064b0, 0x168a6, 0x0ea50, 0x06b20, 0x1a6c4, 0x0aae0, 0x0a2e0, 0x0d2e3, 0x0c960, 0x0d557, // 2111-2120
        };

        #endregion



        /// <summary>
        /// 获取指定公历日期对应的农历日期
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历日期 如：庚子鼠年正月初一</returns>
        public static string GetLunarDate(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{GetLunarYear(lunarDate[0])}年{GetLunarMonth(lunarDate[1])}月{GetLunarDay(lunarDate[2])}";
        }

        /// <summary>
        /// 获取农历年份
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历年份（字符串）如：庚子鼠年</returns>
        public static string GetLunarYear(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{GetLunarYear(lunarDate[0])}年";
        }

        /// <summary>
        /// 获取天干
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历天干（字符串）如：庚</returns>
        public static string GetTianGan(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{Gan[(lunarDate[0] - 4) % 10]}";
        }

        /// <summary>
        /// 获取地支
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历地支（字符串）如 子</returns>
        public static string GetDiZhi(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{Zhi[(lunarDate[0] - 4) % 12]}";
        }

        /// <summary>
        /// 获取生肖
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历生肖（字符串）如 鼠</returns>
        public static string GetChineseZodiac(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{Animal[(lunarDate[0] - 4) % 12]}";
        }

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历月份（字符串）如：正月</returns>
        public static string GetLunarMonth(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{GetLunarMonth(lunarDate[1])}月";
        }

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dateTime">公历日期</param>
        /// <returns>农历日期（字符串）如：廿三</returns>
        public static string GetLunarDay(DateTime dateTime)
        {
            int[] lunarDate = GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
            return $"{GetLunarDay(lunarDate[2])}";
        }

        /// <summary>
        /// 获取指定公历日期对应的农历日期
        /// </summary>
        /// <param name="year">公历年份</param>
        /// <param name="month">公历月份</param>
        /// <param name="day">公历日期</param>
        /// <returns>农历日期</returns>
        private static int[] GetLunarDate(int year, int month, int day)
        {
            int leapMonth = GetLunarLeapMonth(year);
            int offset = (new DateTime(year, month, day) - new DateTime(1900, 1, 31)).Days;

            int iYear = 0, iMonth = 0, iDay = 0;
            bool leap = false;

            for (iYear = 1900; iYear <= 2100 && offset > 0; iYear++)
            {
                offset -= GetLunarYearDays(iYear);
            }

            if (offset < 0)
            {
                offset += GetLunarYearDays(--iYear);
            }

            int yearDays = GetLunarYearDays(iYear);
            int leapMonthIndex = GetLunarLeapMonth(iYear);

            for (iMonth = 1; iMonth <= 12 && offset > 0; iMonth++)
            {
                if (leapMonthIndex > 0 && iMonth == leapMonthIndex + 1 && !leap)
                {
                    iMonth--;
                    leap = true;
                    yearDays = GetLunarLeapMonthDays(iYear);
                }
                else
                {
                    yearDays = GetLunarMonthDays(iYear, iMonth);
                }

                if (leap && iMonth == leapMonthIndex + 1)
                {
                    leap = false;
                }

                offset -= yearDays;
            }

            if (offset == 0 && leapMonthIndex > 0 && iMonth == leapMonthIndex + 1)
            {
                if (leap)
                {
                    leap = false;
                }
                else
                {
                    leap = true;
                    iMonth--;
                }
            }

            if (offset < 0)
            {
                offset += yearDays;
                iMonth--;
            }

            iDay = offset + 1;
            return new[] { iYear, iMonth, iDay, leapMonth, leap ? 1 : 0 };
        }

        /// <summary>
        /// 获取农历年份
        /// </summary>
        /// <param name="year">农历年份（数字）</param>
        /// <returns>农历年份（字符串）如：庚子鼠年</returns>
        private static string GetLunarYear(int year)
        {
            return $"{Gan[(year - 4) % 10]}{Zhi[(year - 4) % 12]}{Animal[(year - 4) % 12]}年";
        }

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="month">农历月份（数字）</param>
        /// <returns>农历月份（字符串）如：正月</returns>
        private static string GetLunarMonth(int month)
        {
            return MonthNames[month - 1] + "月";
        }

        /// <summary>
        /// 获取指定年份的农历年份的天数
        /// </summary>
        /// <param name="year">指定年份</param>
        /// <returns>农历年份的天数</returns>
        private static int GetLunarYearDays(int year)
        {
            int sum = 348;
            for (int i = 0x8000; i > 0x8; i >>= 1)
            {
                if ((LunarMonthDays[year - MinYear] & i) != 0)
                {
                    sum += 1;
                }
            }
            return sum + GetLunarLeapMonthDays(year);
        }


        /// <summary>
        /// 获取农历闰月月份
        /// </summary>
        /// <param name="year">农历年份</param>
        /// <returns>闰月月份（若当年没有闰月，返回0）</returns>
        private static int GetLunarLeapMonth(int year)
        {
            int leapMonth = LunarMonthDays[year - MinYear] >> 16;
            if (leapMonth > 0 && leapMonth < 13 && GetBit(LunarMonthDays[year - MinYear], 16 - leapMonth) == 0)
            {
                return leapMonth;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="day">农历日期（数字）</param>
        /// <returns>农历日期（字符串）如：廿三</returns>
        private static string GetLunarDay(int day)
        {
            int d1 = day / 10;
            int d2 = day % 10;
            if (d1 == 0)
            {
                d1 = 3;
            }
            if (d2 == 0)
            {
                d2 = 10;
            }
            if (d2 == 20)
            {
                d2 = 0;
                d1++;
            }
            return $"{DayNames[d1 - 1]}{ChineseNumbers[d2]}";
        }



        /// <summary>
        /// 获取指定年份和月份的农历月份的天数
        /// </summary>
        /// <param name="year">指定年份</param>
        /// <param name="month">指定月份</param>
        /// <returns>农历月份的天数 29或30</returns>
        private static int GetLunarMonthDays(int year, int month)
        {
            return (LunarMonthDays[year - MinYear] & (0x10000 >> month)) == 0 ? 29 : 30;
        }

        /// <summary>
        /// 获取指定年份的农历闰月的天数
        /// </summary>
        /// <param name="year">指定年份</param>
        /// <returns>农历闰月的天数（29或30，若当年没有闰月，返回0）</returns>
        private static int GetLunarLeapMonthDays(int year)
        {
            if (GetLunarLeapMonth(year) > 0)
            {
                return (LunarMonthDays[year - MinYear] & 0x10000) == 0 ? 29 : 30;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取指定整数的指定位的值
        /// </summary>
        /// <param name="num">指定整数</param>
        /// <param name="bit">指定位（从右往左数，最右边的位为第1位）</param>
        /// <returns>指定位的值（0或1）</returns>
        private static int GetBit(int num, int bit)
        {
            return (num >> (bit - 1)) & 1;
        }

    }
}
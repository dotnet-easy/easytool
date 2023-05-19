using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// 正则工具
    /// </summary>
    public class RegexUtil
    {
        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>如果字符串与正则表达式匹配，则为true；否则为false</returns>
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回匹配结果
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>如果字符串与正则表达式匹配，则为匹配结果；否则为null</returns>
        public static string Match(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : null;
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回所有匹配结果
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>所有匹配结果的字符串数组</returns>
        public static string[] Matches(string input, string pattern)
        {
            return Regex.Matches(input, pattern).Cast<Match>().Select(m => m.Value).ToArray();
        }

        /// <summary>
        /// 使用指定的替换字符串替换输入字符串中与指定正则表达式匹配的所有子字符串
        /// </summary>
        /// <param name="input">要替换的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="replacement">替换字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// 使用指定的替换字符串替换输入字符串中与指定正则表达式匹配的所有子字符串，并返回替换后的字符串和替换次数
        /// </summary>
        /// <param name="input">要替换的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="count">替换次数</param>
        /// <returns>包含替换后的字符串和替换次数的元组</returns>
        public static (string, int) Replace(string input, string pattern, string replacement, int count)
        {
            string result = Regex.Replace(input, pattern, replacement, RegexOptions.None, TimeSpan.FromSeconds(1));
            return (result, Regex.Matches(input, pattern).Count);
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回所有分组的匹配结果
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>所有分组匹配结果的字符串数组</returns>
        public static string[] MatchGroups(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups.Cast<Group>().Skip(1).Select(g => g.Value).ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回所有分组的匹配结果及分组名称
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>所有分组匹配结果及分组名称的字典</returns>
        public static Dictionary<string, string> MatchGroupsWithNames(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups.Cast<Group>().Skip(1).ToDictionary(g => g.Name, g => g.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回匹配结果和捕获组名称的字典
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>匹配结果和捕获组名称的字典</returns>
        public static Dictionary<string, string> MatchWithGroupNames(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups.Cast<Group>().ToDictionary(g => g.Name, g => g.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 验证字符串是否与指定的正则表达式匹配，并返回所有分组的匹配结果及分组名称的元组
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>所有分组匹配结果及分组名称的元组</returns>
        public static (string, Dictionary<string, string>) MatchGroupsWithNamesTuple(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return (match.Value, match.Groups.Cast<Group>().Skip(1).ToDictionary(g => g.Name, g => g.Value));
            }
            else
            {
                return (null, null);
            }
        }
    }
}

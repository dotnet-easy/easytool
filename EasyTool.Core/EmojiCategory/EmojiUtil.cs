using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    public class EmojiUtil
    {
        // Unicode 区间：Emoji 表情符号的 Unicode 区间
        private const string EmojiRanges = "[\u1F600-\u1F64F\u1F910-\u1F96B\u1F980-\u1F9E0]";

        // 表情符号的正则表达式
        private static readonly Regex EmojiRegex = new Regex(EmojiRanges, RegexOptions.Compiled);

        /// <summary>
        /// 判断字符串中是否包含 Emoji 表情符号
        /// </summary>
        /// <param name="text">要检查的字符串</param>
        /// <returns>如果包含 Emoji 表情符号则返回 true，否则返回 false</returns>
        public static bool ContainsEmoji(string text)
        {
            return EmojiRegex.IsMatch(text);
        }

        /// <summary>
        /// 删除字符串中的 Emoji 表情符号
        /// </summary>
        /// <param name="text">要删除 Emoji 表情符号的字符串</param>
        /// <returns>删除 Emoji 表情符号后的字符串</returns>
        public static string RemoveEmoji(string text)
        {
            return EmojiRegex.Replace(text, "");
        }


        /// <summary>
        /// 将字符串中的 Emoji 表情符号转换为对应的 Unicode 码点
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ConvertEmojiToUnicode(string text)
        {
            return EmojiRegex.Replace(text, m => ((int)m.Value[0]).ToString("X").ToLower());
        }

        /// <summary>
        /// 将字符串中的 Emoji 表情符号转换为对应的 HTML 实体编码
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string ConvertEmojiToHtmlEntities(string text)
        {
            var stringBuilder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsSurrogatePair(c, c))
                {
                    // 如果是代理项对，则将其转换为 Unicode 码点再转换为 HTML 实体编码
                    var codepoint = char.ConvertToUtf32(c, text[text.IndexOf(c) + 1]);
                    stringBuilder.Append("&#x").Append(codepoint.ToString("X")).Append(";");
                }
                else if (EmojiRegex.IsMatch(c.ToString()))
                {
                    // 如果是 Emoji 表情符号，则将其转换为对应的 HTML 实体编码
                    stringBuilder.Append("&#x").Append(((int)c).ToString("X")).Append(";");
                }
                else
                {
                    // 否则直接追加到字符串中
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

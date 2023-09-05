using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// 转义和反转义工具类
    /// </summary>
    public class EscapeUtil
    {
        /// <summary>
        /// 将字符串中的特殊字符进行转义
        /// </summary>
        /// <param name="str">需要转义的字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string escaped = Regex.Replace(str, @"[\a\b\f\n\r\t\v\\""]", m => {
                switch (m.Value)
                {
                    case "\a":
                        return @"\a";
                    case "\b":
                        return @"\b";
                    case "\f":
                        return @"\f";
                    case "\n":
                        return @"\n";
                    case "\r":
                        return @"\r";
                    case "\t":
                        return @"\t";
                    case "\v":
                        return @"\v";
                    case "\\":
                        return @"\\";
                    case "\"":
                        return @"\""";
                    default:
                        return m.Value;
                }
            });

            return escaped;
        }

        /// <summary>
        /// 将字符串中的转义字符还原成特殊字符
        /// </summary>
        /// <param name="str">需要还原的字符串</param>
        /// <returns>还原后的字符串</returns>
        public static string Unescape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string unescaped = Regex.Replace(str, @"\\[a-z""\\]", m => {
                switch (m.Value)
                {
                    case @"\a":
                        return "\a";
                    case @"\b":
                        return "\b";
                    case @"\f":
                        return "\f";
                    case @"\n":
                        return "\n";
                    case @"\r":
                        return "\r";
                    case @"\t":
                        return "\t";
                    case @"\v":
                        return "\v";
                    case @"\\":
                        return "\\";
                    case @"\""":
                        return "\"";
                    default:
                        return m.Value;
                }
            });

            return unescaped;
        }

        /// <summary>
        /// 将URL中的特殊字符进行转义
        /// </summary>
        /// <param name="url">需要转义的URL</param>
        /// <returns>转义后的URL</returns>
        public static string UrlEncode(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            return Uri.EscapeDataString(url);
        }

        /// <summary>
        /// 将URL中的转义字符还原成特殊字符
        /// </summary>
        /// <param name="url">需要还原的URL</param>
        /// <returns>还原后的URL</returns>
        public static string UrlDecode(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            return Uri.UnescapeDataString(url);
        }

        /// <summary>
        /// 将HTML字符串进行转义，将特殊字符替换成HTML实体
        /// </summary>
        /// <param name="html">需要转义的HTML字符串</param>
        /// <returns>转义后的HTML字符串</returns>
        public static string HtmlEncode(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            return System.Net.WebUtility.HtmlEncode(html);
        }

        /// <summary>
        /// 将HTML字符串中的HTML实体还原成特殊字符
        /// </summary>
        /// <param name="html">需要还原的HTML字符串</param>
        /// <returns>还原后的HTML字符串</returns>
        public static string HtmlDecode(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            return System.Net.WebUtility.HtmlDecode(html);
        }

        /// <summary>
        /// 将XML字符串进行转义，将特殊字符替换成XML实体
        /// </summary>
        /// <param name="xml">需要转义的XML字符串</param>
        /// <returns>转义后的XML字符串</returns>
        public static string XmlEncode(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            return System.Security.SecurityElement.Escape(xml);
        }

        /// <summary>
        /// 将XML字符串中的XML实体还原成特殊字符
        /// </summary>
        /// <param name="xml">需要还原的XML字符串</param>
        /// <returns>还原后的XML字符串</returns>
        public static string XmlDecode(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            return Regex.Replace(xml, @"&[a-zA-Z]+;", m => {
                switch (m.Value)
                {
                    case "&amp;":
                        return "&";
                    case "&lt;":
                        return "<";
                    case "&gt;":
                        return ">";
                    case "&quot;":
                        return "\"";
                    case "&apos;":
                        return "'";
                    default:
                        return m.Value;
                }
            });
        }
    }
}

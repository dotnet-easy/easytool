using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EasyTool
{
    /// <summary>
    /// URL工具类
    /// </summary>
    public class URLUtil
    {
        /// <summary>
        /// 解析URL并返回其组成部分。
        /// </summary>
        /// <param name="url">要解析的URL。</param>
        /// <returns>URL组成部分的字符串数组。</returns>
        public static string[] ParseUrl(string url)
        {
            Uri uri = new Uri(url);
            return new string[] { uri.Scheme, uri.Host, uri.Port.ToString(), uri.AbsolutePath };
        }

        /// <summary>
        /// 将指定的查询字符串参数添加到URL。
        /// </summary>
        /// <param name="url">要添加查询参数的URL。</param>
        /// <param name="parameters">查询参数的键值对。</param>
        /// <returns>包含新查询参数的URL。</returns>
        public static string AddQueryParameters(string url, params KeyValuePair<string, string>[] parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var parameter in parameters)
            {
                query[parameter.Key] = parameter.Value;
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        /// <summary>
        /// 从URL中删除指定的查询字符串参数。
        /// </summary>
        /// <param name="url">要删除查询参数的URL。</param>
        /// <param name="parameters">要删除的查询参数的键。</param>
        /// <returns>不包含指定查询参数的URL。</returns>
        public static string RemoveQueryParameters(string url, params string[] parameters)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var parameter in parameters)
            {
                query.Remove(parameter);
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        /// <summary>
        /// 将两个URL组合成一个。
        /// </summary>
        /// <param name="baseUrl">基本URL。</param>
        /// <param name="relativeUrl">相对于基本URL的URL。</param>
        /// <returns>组合后的URL。</returns>
        public static string CombineUrls(string baseUrl, string relativeUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (string.IsNullOrEmpty(relativeUrl))
                throw new ArgumentNullException(nameof(relativeUrl));

            Uri baseUri = new Uri(baseUrl);
            Uri relativeUri = new Uri(relativeUrl, UriKind.RelativeOrAbsolute);

            if (!baseUri.IsAbsoluteUri)
                throw new ArgumentException("The base URL must be an absolute URL.", nameof(baseUrl));

            if (relativeUri.IsAbsoluteUri)
                return relativeUri.ToString();

            string baseWithoutQuery = StripQueryAndFragment(baseUri);
            string relativeWithoutQuery = StripQueryAndFragment(relativeUri);

            Uri combinedUri = new Uri(baseWithoutQuery + "/" + relativeWithoutQuery, UriKind.RelativeOrAbsolute);
            return combinedUri.ToString();
        }

        /// <summary>
        /// 从URL中去掉查询参数和片段。
        /// </summary>
        /// <param name="uri">要去掉查询参数和片段的
        /// /// <returns>不包含查询参数和片段的URL。</returns>
        private static string StripQueryAndFragment(Uri uri)
        {
            return uri.GetLeftPart(UriPartial.Path);
        }

        /// <summary>
        /// 编码URL字符串。
        /// </summary>
        /// <param name="value">要编码的字符串。</param>
        /// <returns>编码后的字符串。</returns>
        public static string UrlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        /// <summary>
        /// 编码URL查询字符串。
        /// </summary>
        /// <param name="value">要编码的字符串。</param>
        /// <returns>编码后的字符串。</returns>
        public static string UrlEncodeQuery(string value)
        {
            return HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 解码URL字符串。
        /// </summary>
        /// <param name="value">要解码的字符串。</param>
        /// <returns>解码后的字符串。</returns>
        public static string UrlDecode(string value)
        {
            return HttpUtility.UrlDecode(value);
        }

        /// <summary>
        /// 解码URL查询字符串。
        /// </summary>
        /// <param name="value">要解码的字符串。</param>
        /// <returns>解码后的字符串。</returns>
        public static string UrlDecodeQuery(string value)
        {
            return HttpUtility.UrlDecode(value, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 从URL中提取域名。
        /// </summary>
        /// <param name="url">要提取域名的URL。</param>
        /// <returns>URL中的域名。</returns>
        public static string ExtractDomain(string url)
        {
            var uri = new Uri(url);
            return uri.Host;
        }

        /// <summary>
        /// 从URL中提取路径。
        /// </summary>
        /// <param name="url">要提取路径的URL。</param>
        /// <returns>URL中的路径。</returns>
        public static string ExtractPath(string url)
        {
            var uri = new Uri(url);
            return uri.AbsolutePath;
        }

        /// <summary>
        /// 判断URL是否为HTTPS协议。
        /// </summary>
        /// <param name="url">要判断的URL。</param>
        /// <returns>如果是HTTPS协议，则为true，否则为false。</returns>
        public static bool IsHttps(string url)
        {
            var uri = new Uri(url);
            return uri.Scheme.ToLower() == "https";
        }

        /// <summary>
        /// 从URL中提取查询字符串。
        /// </summary>
        /// <param name="url">要提取查询字符串的URL。</param>
        /// <returns>URL中的查询字符串。</returns>
        public static string ExtractQueryString(string url)
        {
            var uri = new Uri(url);
            return uri.Query;
        }

        /// <summary>
        /// 从URL中提取片段。
        /// </summary>
        /// <param name="url">要提取片段的URL。</param>
        /// <returns>URL中的片段。</returns>
        public static string ExtractFragment(string url)
        {
            var uri = new Uri(url);
            return uri.Fragment;
        }

        /// <summary>
        /// 将URL路径转换为相对路径。
        /// </summary>
        /// <param name="url">要转换的URL路径。</param>
        /// <returns>相对于网站根目录的路径。</returns>
        public static string PathToRelative(string url)
        {
            var uri = new Uri(url);
            return uri.LocalPath.TrimStart('/');
        }

        /// <summary>
        /// 将相对路径转换为URL路径。
        /// </summary>
        /// <param name="relativePath">要转换的相对路径。</param>
        /// <param name="baseUrl">基本URL，如果相对路径不是绝对路径，则使用此URL。</param>
        /// <returns>URL路径。</returns>
        public static string RelativeToPath(string relativePath, string baseUrl)
        {
            Uri baseUri = new Uri(baseUrl);
            Uri uri = new Uri(baseUri, relativePath);
            return uri.ToString();
        }

        /// <summary>
        /// 将URL的查询参数转换为字典。
        /// </summary>
        /// <param name="url">要提取查询参数的URL。</param>
        /// <returns>查询参数的键值对字典。</returns>
        public static Dictionary<string, string> QueryToDictionary(string url)
        {
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.AllKeys.ToDictionary(key => key, key => query[key]);
        }
    }
}

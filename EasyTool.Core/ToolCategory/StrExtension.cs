using System.Text.RegularExpressions;

namespace EasyTool.Extension
{
    public static class StrExtension
    {
        #region 文本可为空判断

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        #endregion
    }
}

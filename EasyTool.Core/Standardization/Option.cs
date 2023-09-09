using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace EasyTool
{

#if NET6_0_OR_GREATER
    /*
     *标准化与前端下拉选项数据结构，减少前后端对接工作
     */

    public record Option<T>(T Value, string Text);

    /// <summary>
    /// 包含Value和Text的选择对象，用于前端下拉选项
    /// </summary>
    public record Option(string Value, string Text) : Option<string>(Value, Text);

    /// <summary>
    /// 包含Value和Text的选择对象，用于前端下拉选项
    /// </summary>
    public record OptionInt(int? Value, string Text) : Option<int?>(Value, Text);

    /// <summary>
    /// 选项接口，用于描述选项的类
    /// 
    /// 示例
    /// public class LogLevel
    /// {
    ///     [DisplayName("调试")]
    ///     public static string Debugg { get; set; } = nameof(Debugg);
    ///     [DisplayName("消息")]
    ///     public static string Info { get; set; } = nameof(Info);
    ///     [DisplayName("警告")]
    ///     public static string Warning { get; set; } = nameof(Warning);
    ///     [DisplayName("错误")]
    ///     public static string Error { get; set; } = nameof(Error);
    /// }
    /// DisplayName：用于前端显示
    /// 字段名称：用于代码编写
    /// 字段值：用于数据存储
    /// </summary>
    public interface IOption
    {
        /// <summary>
        /// 获得选项列表
        /// </summary>
        public static List<Option> GetOptions<T>() where T : IOption, new()
        {
            return new T().ToOptions();
        }
    }

    /// <summary>
    /// 选项扩展
    /// </summary>
    public static class OptionExtension
    {
        /// <summary>
        /// 获得选项列表
        /// </summary>
        public static List<Option> ToOptions(this IOption option)
        {
            return option.GetType().GetProperties().Select(x => new Option(
                x.GetValue(option)?.ToString() ?? x.Name,
                x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? x.Name
                )).ToList();
        }
    }
#endif
}

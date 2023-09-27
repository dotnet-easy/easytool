using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 枚举工具类，包含了各种枚举操作的方法
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// 获取指定枚举类型的所有成员名称
        /// </summary>
        /// <typeparam name="TEnum">要获取成员名称的枚举类型</typeparam>
        /// <returns>所有成员名称的字符串数组</returns>
        public static string[] GetNames<TEnum>()
        {
            return Enum.GetNames(typeof(TEnum));
        }

        /// <summary>
        /// 获取指定枚举类型的所有成员的值
        /// </summary>
        /// <typeparam name="TEnum">要获取成员值的枚举类型</typeparam>
        /// <returns>所有成员值的数组</returns>
        public static TEnum[] GetValues<TEnum>()
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

        /// <summary>
        /// 获取指定枚举值的名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举值的名称</returns>
        public static string GetName<TEnum>(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        /// <summary>
        /// 检查指定的值是否是枚举类型TEnum的成员
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">要检查的值</param>
        /// <returns>如果指定的值是TEnum的成员，则为true；否则为false</returns>
        public static bool IsDefined<TEnum>(object value)
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        /// <summary>
        /// 将字符串转换为枚举类型TEnum的值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">要转换的字符串</param>
        /// <returns>与字符串对应的枚举值</returns>
        public static TEnum Parse<TEnum>(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        /// <summary>
        /// 将字符串转换为枚举类型TEnum的值如果字符串无法转换，则返回默认值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>与字符串对应的枚举值，或默认值（如果字符串无法转换）</returns>
        public static TEnum Parse<TEnum>(string value, TEnum defaultValue)
        {
            var result= Enum.Parse(typeof(TEnum), value);
            return result!=null ? (TEnum)result : defaultValue;
        }

        /// <summary>
        /// 获取指定枚举类型的Type对象
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns>枚举类型的Type对象</returns>
        public static Type GetEnumType<TEnum>()
        {
            return typeof(TEnum);
        }

        /// <summary>
        /// 获取指定枚举类型的所有成员的名称和值的键值对
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns>所有成员名称和值的键值对</returns>
        public static IDictionary<string, TEnum> GetValuesDictionary<TEnum>()
        {
            var valuesDictionary = new Dictionary<string, TEnum>();
            foreach (var value in GetValues<TEnum>())
            {
                valuesDictionary.Add(GetName(value), value);
            }
            return valuesDictionary;
        }

        /// <summary>
        /// 获取指定枚举类型的所有成员的注释
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns>所有成员注释的字典，其中键是成员名称，值是注释内容</returns>
        public static IDictionary<string, string> GetDescriptions<TEnum>()
        {
            var descriptions = new Dictionary<string, string>();
            var enumType = GetEnumType<TEnum>();
            foreach (var memberInfo in enumType.GetMembers())
            {
                var attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null)
                {
                    descriptions.Add(memberInfo.Name, attribute.Description);
                }
            }
            return descriptions;
        }

        /// <summary>
        /// 获取指定枚举类型的指定成员的注释
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举成员</param>
        /// <returns>成员注释的字符串</returns>
        public static string GetDescription<TEnum>(TEnum value)
        {
            var memberInfo = GetEnumType<TEnum>().GetMember(value.ToString()).FirstOrDefault();
            if (memberInfo == null)
            {
                return string.Empty;
            }

            var attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : string.Empty;
        }

        /// <summary>
        /// 获取指定枚举类型的指定成员的Display名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举成员</param>
        /// <returns>成员的Display名称，如果未设置，则返回枚举成员的名称</returns>
        public static string GetDisplayName<TEnum>(TEnum value)
        {
            var memberInfo = GetEnumType<TEnum>().GetMember(value.ToString()).FirstOrDefault();
            if (memberInfo == null)
            {
                return string.Empty;
            }

            var attribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            return attribute != null ? attribute.Name : value.ToString();
        }

        /// <summary>
        /// 获取指定枚举类型的所有成员的Display名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns>所有成员的Display名称的字典，其中键是成员名称，值是Display名称</returns>
        public static IDictionary<string, string> GetDisplayNames<TEnum>()
        {
            var displayNames = new Dictionary<string, string>();
            var enumType = GetEnumType<TEnum>();
            foreach (var memberInfo in enumType.GetMembers())
            {
                var attribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
                if (attribute != null)
                {
                    displayNames.Add(memberInfo.Name, attribute.Name);
                }
            }
            return displayNames;
        }

        /// <summary>
        /// 获取指定枚举类型的指定成员的值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name">成员名称</param>
        /// <returns>成员的值，如果成员不存在，则返回默认值</returns>
        public static TEnum GetValueByName<TEnum>(string name)
        {
            return Parse<TEnum>(name, default(TEnum));
        }

        /// <summary>
        /// 获取指定枚举类型的指定值的名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>与值对应的名称，如果值不存在，则返回null</returns>
        public static string GetNameByValue<TEnum>(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        /// <summary>
        /// 获取指定枚举类型的指定值的注释
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>与值对应的注释，如果值不存在或未设置注释，则返回null</returns>
        public static string GetDescriptionByValue<TEnum>(TEnum value)
        {
            var name = GetNameByValue(value);
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return GetDescription(GetValueByName<TEnum>(name));
        }

        /// <summary>
        /// 获取指定枚举类型的指定值的Display名称
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>与值对应的Display名称，如果值不存在或未设置Display名称，则返回null</returns>
        public static string GetDisplayNameByValue<TEnum>(TEnum value)
        {
            var name = GetNameByValue(value);
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return GetDisplayName(GetValueByName<TEnum>(name));
        }
    }
}

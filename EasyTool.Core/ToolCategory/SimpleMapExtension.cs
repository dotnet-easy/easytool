using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool;
/// <summary>
/// 简单实体转化拓展类
/// </summary>
public static class SimpleMapExtension
{
    /// <summary>
    /// 简单实体转化
    /// 目标泛型需要默认构造函数
    /// 只需属性名字一样，可转化类型会自动转化成目标类型
    /// </summary>
    /// <typeparam name="TSource">源泛型</typeparam>
    /// <typeparam name="TDestination">目标泛型</typeparam>
    /// <param name="source">源实体</param>
    /// <returns></returns>
    public static TDestination SimpleMapTo<TSource, TDestination>(this TSource source) where TDestination : new()
    {
        TDestination entity = Activator.CreateInstance<TDestination>();
        if (source == null)
            return entity;
        var sourceProps = source.GetType().GetProperties();
        var destinationProps = typeof(TDestination).GetProperties();
        foreach (var sourceProp in sourceProps)
        {
            //获取TSource,TDestination名字相同的属性
            var destinationProp = destinationProps.FirstOrDefault(entity => entity.Name == sourceProp.Name);
            //需要做映射
            if (destinationProp != null)
            {
                //获取属性的类型
                Type destinationType = destinationProp.PropertyType;
                //把dtoType强转成TDestination,如果成功返回ture，失败捕获异常 返回false
                try
                {
                    object? sourceValue = sourceProp.GetValue(source, null);
                    object? destinationValue = Convert.ChangeType(sourceValue, destinationType);
                    destinationProp.SetValue(entity, destinationValue);
                }
                catch (Exception)
                {
                    //不做处理 跳过该属性
                }
            }
        }
        return entity;
    }
    /// <summary>
    /// 集合实体转化，需要目标泛型具有默认构造函数
    /// </summary>
    /// <typeparam name="TSource">源泛型</typeparam>
    /// <typeparam name="TDestination">目标泛型</typeparam>
    /// <param name="sources">可枚举类型源实体</param>
    /// <returns></returns>
    public static List<TDestination>? SimpleMapTo<TSource, TDestination>(this IEnumerable<TSource> sources) where TDestination : new()
    {
        if (!sources.Any())
            return default;
        List<TDestination> result = new();
        foreach (var source in sources)
        {
            var destination = SimpleMapTo<TSource, TDestination>(source);
            result.Add(destination);
        }
        return result;
    }
}

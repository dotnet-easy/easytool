using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace EasyTool;
/// <summary>
/// 简单实体转化拓展类
/// </summary>
public static class SimpleMapExtension
{
    private readonly struct MapTypeKey : IEquatable<MapTypeKey>
    {
        private readonly int hashCode;
        public MapTypeKey(Type srcType, Type destType)
        {
            SrcType = srcType;
            DestType = destType;
            hashCode = unchecked((srcType.GetHashCode() * 31) + destType.GetHashCode());
        }

        public Type SrcType { get; }
        public Type DestType { get; }
        public override int GetHashCode()
        {
            return hashCode;
        }
        public bool Equals(MapTypeKey other)
        {
            return SrcType == other.SrcType && DestType == other.DestType;
        }
    }

    private static readonly ConcurrentDictionary<MapTypeKey, Delegate> mapDelegateCache = new();

    private static Delegate BuildSimpleMapDelegate(Type srcType, Type destType)
    {
        var srcExpr = Expression.Parameter(srcType, "src");
        List<MemberAssignment> assignments = new();
        var sourceProps = srcType.GetProperties();
        var destinationProps = destType.GetProperties();
        foreach (var destinationProp in destinationProps)
        {
            if (!destinationProp.CanWrite) continue;
            //获取TSource,TDestination名字相同的属性
            var sourceProp = sourceProps.FirstOrDefault(entity => entity.Name == destinationProp.Name);
            if (sourceProp == null || !sourceProp.CanRead) continue;
            Expression sourcePropExpr = Expression.Property(srcExpr, sourceProp);
            Expression? expr = null;
            //类型相等，直接转换
            if (destinationProp.PropertyType == sourceProp.PropertyType)
            {
                expr = sourcePropExpr;
            }
            else
            {
                //存在转换器可以转换类型
                var sourcePropTypeConverter = TypeDescriptor.GetConverter(sourceProp.PropertyType);
                if (sourcePropTypeConverter.CanConvertTo(destinationProp.PropertyType))
                {
                    expr =
                        Expression.Convert(
                            Expression.Call(
                                Expression.Constant(sourcePropTypeConverter),
                                nameof(TypeConverter.ConvertTo),
                                Type.EmptyTypes,
                                Expression.Convert(sourcePropExpr, typeof(object)),
                                Expression.Constant(destinationProp.PropertyType)
                            ),
                            destinationProp.PropertyType
                        );
                }
                else
                {
                    var destPropTypeConverter = TypeDescriptor.GetConverter(destinationProp.PropertyType);
                    if (destPropTypeConverter.CanConvertFrom(sourceProp.PropertyType))
                    {
                        expr =
                            Expression.Convert(
                                Expression.Call(
                                    Expression.Constant(destPropTypeConverter),
                                    nameof(TypeConverter.ConvertFrom),
                                    Type.EmptyTypes,
                                    Expression.Convert(sourcePropExpr, typeof(object))
                                ),
                                destinationProp.PropertyType
                            );
                    }
                }
            }

            if (expr == null) continue;
            assignments.Add(Expression.Bind(destinationProp, expr));
        }

        var bodyExpr = Expression.Condition(
            Expression.Equal(srcExpr, Expression.Default(srcType)),
            Expression.New(destType),
            Expression.MemberInit(Expression.New(destType), assignments)
            );

        return Expression.Lambda(bodyExpr, srcExpr).Compile();
    }
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
        var mapper = (Func<TSource, TDestination>)mapDelegateCache.GetOrAdd(new(typeof(TSource), typeof(TDestination)), static key => BuildSimpleMapDelegate(key.SrcType, key.DestType));

        return mapper(source);
    }
    /// <summary>
    /// 集合实体转化，需要目标泛型具有默认构造函数
    /// </summary>
    /// <typeparam name="TSource">源泛型</typeparam>
    /// <typeparam name="TDestination">目标泛型</typeparam>
    /// <param name="sources">可枚举类型源实体</param>
    /// <returns></returns>
    public static IEnumerable<TDestination> SimpleMapTo<TSource, TDestination>(this IEnumerable<TSource> sources) where TDestination : new()
    {
        if (!sources.Any())
            return Enumerable.Empty<TDestination>();
        var mapper = (Func<TSource, TDestination>)mapDelegateCache.GetOrAdd(new(typeof(TSource), typeof(TDestination)), static key => BuildSimpleMapDelegate(key.SrcType, key.DestType));

        List<TDestination> result = new();
        foreach (var source in sources)
        {
            result.Add(mapper(source));
        }
        return result;
    }
}

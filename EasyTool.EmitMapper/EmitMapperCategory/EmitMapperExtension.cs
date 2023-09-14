using EmitMapper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyTool.Extension
{
    /// <summary>
    /// EmitMapperHelper
    /// </summary>
    public static class EmitMapperExtension
    {
        /// <summary>
        /// 单实体映射 避免int? 映射 int 类型
        /// </summary>
        public static TDestination EmitMapTo<TSource, TDestination>(this TSource obj)
        {
            if (obj == null)
                return default;
            return ObjectMapperManager.DefaultInstance.GetMapper<TSource, TDestination>().Map(obj);
        }

        /// <summary>
        /// 对象列表映射 避免int? 映射 int 类型
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static List<TDestination> EmitMapToList<TSource, TDestination>(this IEnumerable sources)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSource, TDestination>();
            var list = mapper.MapEnum((IEnumerable<TSource>)sources);
            return list.ToList();
        }
    }
}

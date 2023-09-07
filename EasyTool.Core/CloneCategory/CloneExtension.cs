using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.Extension
{
    public static class CloneExtension
    {
        //定义一个泛型方法，接受一个泛型参数 T，并返回一个 T 类型的对象
        public static T Clone<T>(this T obj)=> CloneUtil.Clone(obj);
    }
}

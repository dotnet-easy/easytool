using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace EasyTool
{
    /// <summary>
    /// 静态工具类 CloneHelper，用于深度克隆对象
    /// </summary>
    public static class CloneUtil
    {
        // 定义一个泛型方法，接受一个泛型参数 T，并返回一个 T 类型的对象
        public static T Clone<T>(T obj)
        {
            // 检查类型是否可序列化
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(obj));
            }

            // 如果对象为 null，则返回 null
            if (ReferenceEquals(obj, null))
            {
                return default(T);
            }

            // 创建一个二进制序列化器
            IFormatter formatter = new BinaryFormatter();

            // 创建一个内存流
            using (var stream = new MemoryStream())
            {
                // 使用二进制序列化将对象写入内存流
                formatter.Serialize(stream, obj);

                // 将内存流位置重置为开头
                stream.Seek(0, SeekOrigin.Begin);

                // 使用反序列化从内存流中读取并返回克隆的对象
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}

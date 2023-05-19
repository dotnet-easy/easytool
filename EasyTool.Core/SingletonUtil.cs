using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 单例工具类
    /// </summary>
    public class SingletonUtil<T> where T : class, new()
    {
        private static readonly Lazy<T> lazyInstance = new Lazy<T>(() => new T());

        /// <summary>
        /// 返回单例对象的唯一实例（懒汉模式）
        /// </summary>
        public static T LazyInstance => lazyInstance.Value;


        private static T instance;
        private static readonly object lockObject = new object();

        /// <summary>
        /// 返回单例对象的唯一实例（饿汉模式）
        /// </summary>
        [Obsolete]
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }

                return instance;
            }
        }
    }
}

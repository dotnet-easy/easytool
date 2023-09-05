using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EasyTool
{
    /// <summary>
    /// 文件跟随工具类
    /// </summary>
    public class Tailer : IDisposable
    {
        private readonly string filePath; // 被监视的文件路径
        private readonly StreamReader reader; // 用于读取文件内容的 StreamReader
        private readonly Timer timer; // 定时器，用于定期检查文件是否有新内容

        // 定义事件，用于通知外部监听器
        public event EventHandler<string> NewLine;

        // 构造函数，初始化文件路径、StreamReader 和定时器
        public Tailer(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new ArgumentException($"The specified file '{filePath}' does not exist.");

            this.filePath = filePath;

            // 初始化 StreamReader，注意要使用 FileShare.ReadWrite 参数，以便其他进程可以同时访问文件
            reader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            // 初始化定时器，每隔 1 秒触发一次 OnTimerCallback 方法
            timer = new Timer(OnTimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        // 定时器回调方法，检查文件是否有新内容
        private void OnTimerCallback(object state)
        {
            // 如果 StreamReader 不可用，直接返回
            if (reader == null || reader.BaseStream == null || !reader.BaseStream.CanRead)
                return;

            // 如果已经读到文件末尾，直接返回
            if (reader.EndOfStream)
                return;

            // 逐行读取文件内容
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 如果有新行，触发 NewLine 事件
                if (NewLine != null)
                {
                    NewLine(this, line);
                }
            }
        }

        // IDisposable 接口实现方法，释放资源
        public void Dispose()
        {
            timer.Dispose();
            reader.Dispose();
        }
    }
}

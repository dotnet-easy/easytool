using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 文件监听工具类
    /// </summary>
    public class WatchMonitor
    {
        private readonly FileSystemWatcher watcher;

        // 定义事件，用于通知外部监听器
        public event EventHandler<FileEventArgs> FileChanged;
        public event EventHandler<FileEventArgs> FileCreated;
        public event EventHandler<FileEventArgs> FileDeleted;
        public event EventHandler<FileEventArgs> FileMissing;
        public event EventHandler<FileEventArgs> FileError;

        /// <summary>
        /// 构造函数，初始化 FileSystemWatcher 实例
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public WatchMonitor(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path))
                throw new ArgumentException($"The specified directory '{path}' does not exist.");

            watcher = new FileSystemWatcher(path);
            watcher.EnableRaisingEvents = true;

            watcher.Changed += OnFileChanged;
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
            watcher.Error += OnFileError;
        }

        /// <summary>
        /// 文件修改事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (FileChanged != null)
            {
                FileChanged(this, new FileEventArgs(e.FullPath));
            }
        }

        /// <summary>
        /// 文件创建事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            if (FileCreated != null)
            {
                FileCreated(this, new FileEventArgs(e.FullPath));
            }
        }

        /// <summary>
        /// 文件删除事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (FileDeleted != null)
            {
                FileDeleted(this, new FileEventArgs(e.FullPath));
            }
        }

        /// <summary>
        /// 文件重命名事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            if (FileMissing != null)
            {
                FileMissing(this, new FileEventArgs(e.OldFullPath));
            }

            if (FileCreated != null)
            {
                FileCreated(this, new FileEventArgs(e.FullPath));
            }
        }

        /// <summary>
        /// 文件错误事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileError(object sender, ErrorEventArgs e)
        {
            if (FileError != null)
            {
                FileError(this, new FileEventArgs(e.GetException()));
            }
        }

        /// <summary>
        /// IDisposable 接口实现方法，释放资源
        /// </summary>
        public void Dispose()
        {
            watcher.Changed -= OnFileChanged;
            watcher.Created -= OnFileCreated;
            watcher.Deleted -= OnFileDeleted;
            watcher.Renamed -= OnFileRenamed;
            watcher.Error -= OnFileError;
            watcher.Dispose();
        }
    }

    /// <summary>
    /// 文件事件参数类，用于传递文件路径信息
    /// </summary>
    public class FileEventArgs : EventArgs
    {
        public string FilePath { get; }
        public Exception Exception { get; }

        public FileEventArgs(string path)
        {
            FilePath = path;
        }

        public FileEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}

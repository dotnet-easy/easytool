using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /*
     * 标准化查询参数，减少前后端对接工作
     */

    /// <summary>
    /// 查询基类
    /// </summary>
    public class QueryPage : IQueryPage
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int PageNum { get; set; } = 0;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 999;
    }


    public interface IQueryPage
    {
        /// <summary>
        /// 页开始序号
        /// </summary>
        int PageNum { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }
    }
}

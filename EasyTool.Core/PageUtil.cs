using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 分页工具类，支持多种数据源和多种排序方式的分页
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PageUtil<T>
    {
        private int currentPage;  // 当前页码
        private int pageSize;  // 每页大小
        private int totalPage;  // 总页数
        private int totalRecord;  // 总记录数
        private List<T> dataSource;  // 数据源
        private Func<T, object> orderField;  // 排序字段
        private bool isAsc;  // 是否升序

        /// <summary>
        /// 构造函数，使用指定数据源、当前页码和每页大小初始化分页工具类
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="currentPage">当前页码，默认为1</param>
        /// <param name="pageSize">每页大小，默认为10</param>
        public PageUtil(List<T> dataSource, int currentPage = 1, int pageSize = 10)
        {
            this.dataSource = dataSource;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.totalRecord = dataSource.Count();
            this.totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            this.orderField = null;
            this.isAsc = true;
        }

        /// <summary>
        /// 构造函数，使用指定数据源、排序字段、排序方式、当前页码和每页大小初始化分页工具类
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="currentPage">当前页码，默认为1</param>
        /// <param name="pageSize">每页大小，默认为10</param>
        public PageUtil(List<T> dataSource, Func<T, object> orderField, bool isAsc, int currentPage = 1, int pageSize = 10)
            : this(dataSource, currentPage, pageSize)
        {
            this.orderField = orderField;
            this.isAsc = isAsc;
            SortDataSource();
        }

        /// <summary>
        /// 对数据源进行排序
        /// </summary>
        private void SortDataSource()
        {
            if (orderField != null)
            {
                if (isAsc)
                {
                    dataSource.Sort((x, y) => Comparer<object>.Default.Compare(orderField(x), orderField(y)));
                }
                else
                {
                    dataSource.Sort((x, y) => Comparer<object>.Default.Compare(orderField(y), orderField(x)));
                }
            }
        }

        /// <summary>
        /// 获取当前页的数据
        /// </summary>
        /// <returns>当前页的数据</returns>
        public List<T> GetData()
        {
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, totalRecord);
            return dataSource.GetRange(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 判断是否有上一页
        /// </summary>
        /// <returns>如果有上一页，返回true
        public bool HasPreviousPage()
        {
            return currentPage > 1;
        }

        /// <summary>
        /// 判断是否有下一页
        /// </summary>
        /// <returns>如果有下一页，返回true</returns>
        public bool HasNextPage()
        {
            return currentPage < totalPage;
        }

        /// <summary>
        /// 获取上一页的页码
        /// </summary>
        /// <returns>上一页的页码，如果当前页是第一页，则返回1</returns>
        public int GetPreviousPage()
        {
            return currentPage > 1 ? currentPage - 1 : 1;
        }

        /// <summary>
        /// 获取下一页的页码
        /// </summary>
        /// <returns>下一页的页码，如果当前页是最后一页，则返回总页数</returns>
        public int GetNextPage()
        {
            return currentPage < totalPage ? currentPage + 1 : totalPage;
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns>总页数</returns>
        public int GetTotalPage()
        {
            return totalPage;
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns>总记录数</returns>
        public int GetTotalRecord()
        {
            return totalRecord;
        }

        /// <summary>
        /// 获取当前页码
        /// </summary>
        /// <returns>当前页码</returns>
        public int GetCurrentPage()
        {
            return currentPage;
        }

        /// <summary>
        /// 获取每页大小
        /// </summary>
        /// <returns>每页大小</returns>
        public int GetPageSize()
        {
            return pageSize;
        }

        /// <summary>
        /// 设置当前页码
        /// </summary>
        /// <param name="currentPage">当前页码</param>
        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
        }

        /// <summary>
        /// 设置每页大小，并重新计算总页数
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        public void SetPageSize(int pageSize)
        {
            this.pageSize = pageSize;
            this.totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
        }

        /// <summary>
        /// 设置排序字段和排序方式，并重新对数据源进行排序
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        public void SetOrderField(Func<T, object> orderField, bool isAsc)
        {
            this.orderField = orderField;
            this.isAsc = isAsc;
            SortDataSource();
        }

        /// <summary>
        /// 获取分页HTML代码，使用分页彩虹算法
        /// </summary>
        /// <param name="urlFormat">分页链接格式，其中{0}会被替换为实际的页码</param>
        /// <param name="currentPageClass">当前页码的CSS类名，用于设置当前页码的样式，默认为"current"</param>
        /// <param name="range">分页彩虹算法中的一个重要参数，用于控制分页链接的数量，默认为5</param>
        /// <returns>分页HTML代码</returns>
        public string GetPaginationHtml(string urlFormat, string currentPageClass = "current", int range = 5)
        {
            string html = "";
            if (totalPage <= 1)
            {
                return html;
            }
            int startPage = Math.Max(1, currentPage - range);
            int endPage = Math.Min(totalPage, currentPage + range);
            if (startPage > 1)
            {
                html += "<a href=\"" + String.Format(urlFormat, 1) + "\">1</a>";
                if (startPage > 2)
                {
                    html += "<span>...</span>";
                }
            }
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == currentPage)
                {
                    html += "<span class=\"" + currentPageClass + "\">" + i.ToString() + "</span>";
                }
                else
                {
                    html += "<a href=\"" + String.Format(urlFormat, i) + "\">" + i.ToString() + "</a>";
                }
            }
            if (endPage < totalPage)
            {
                if (endPage < totalPage - 1)
                {
                    html += "<span>...</span>";
                }
                html += "<a href=\"" + String.Format(urlFormat, totalPage) + "\">" + totalPage.ToString() + "</a>";
            }
            return html;
        }
    }
}

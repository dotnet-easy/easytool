using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyTool.IOCategory
{
    public static class FileTypeExtension
    {
        /// <summary>
        /// 文件流头部信息获得文件类型
        /// 
        /// 说明：
        ///     1、无法识别类型默认按照扩展名识别
        ///     2、xls、doc、msi、ppt、vsd头信息无法区分，按照扩展名区分
        ///     3、zip可能为docx、xlsx、pptx、jar、war头信息无法区分，按照扩展名区分
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>类型，文件的扩展名，未找到为null</returns>
        public static string GetType(this FileInfo file) => FileTypeUtil.GetType(file);
    }
}

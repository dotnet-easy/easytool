using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 文件类型判断工具类
    /// </summary>
    public class FileTypeUtil
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
        public static string GetType(FileInfo file)
        {
            byte[] buffer = new byte[256];
            using (FileStream fs = file.OpenRead())
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }

            string header = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                header += buffer[i].ToString();
            }

            string type = null;
            switch (header)
            {
                case "255216": // jpg
                    type = ".jpg";
                    break;
                case "13780": // png
                    type = ".png";
                    break;
                case "7173": // gif
                    type = ".gif";
                    break;
                case "6677": // bmp
                    type = ".bmp";
                    break;
                case "7790": // exe dll
                    type = ".exe";
                    break;
                case "6063": // xml
                    type = ".xml";
                    break;
                case "6033": // htm html
                    type = ".html";
                    break;
                case "4742": // js
                    type = ".js";
                    break;
                case "5144": // txt
                    type = ".txt";
                    break;
                default:
                case "8297": // rar
                case "8075": // zip
                case "D0CF11E0": // doc xls ppt vsd
                    type = file.Extension;
                    break;
            }

            return type;
        }
    }
}

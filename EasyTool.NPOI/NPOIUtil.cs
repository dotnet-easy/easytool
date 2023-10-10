using NPOI.HSSF.UserModel;
using NPOI.POIFS.NIO;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.Util.ArrayExtensions;
using NPOI.XSSF.UserModel;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EasyTool.NPOI;

public static class NPOIUtil
{
    /// <summary>
    /// 输入文件绝对路径，获取工作簿对象
    /// HHSWorkbook:适用于.xls文件; XSSFWorkbook:适用于.xlsx文件
    /// </summary>
    /// <param name="path">绝对路径</param>
    /// <returns>IWorkbook</returns>
    public static IWorkbook OpenWorkbook(string path)
    {
        if (path == null || !File.Exists(path))
            throw new Exception($"路径{path}不存在");
        using var stream = File.OpenRead(path);
        return Path.GetExtension(path) == ".xls"
            ? new HSSFWorkbook(stream)
            : new XSSFWorkbook(stream);
    }
    /// <summary>
    /// 从流读取工作簿，自动判断类型或者显示指定类型
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileExtension">
    /// null or 缺省:适用于不明确的文件类型
    /// xls:适用于.xls文件
    /// xlsx:适用于.xlsx文件
    /// </param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception> 
    
    public static IWorkbook OpenWorkbookFromStream(Stream stream, ExcelWorkbookType excelType =ExcelWorkbookType.XLSX)
    {
        if (stream == null)
            throw new Exception("流不能为空");
        return excelType switch
        {
            ExcelWorkbookType.XLS => new HSSFWorkbook(stream),
            ExcelWorkbookType.XLSX => new XSSFWorkbook(stream),
            _ => throw new Exception("不支持的格式类型,请选择")
        };
    }
    /// <summary>
    /// 把IWorkbook类型的对象转换成DataSet
    /// </summary>
    /// <param name="workbooks"></param>
    /// <returns></returns>
    public static DataSet? ConvertToDataSet(this IWorkbook workbooks)
    {
        var dataSet = new DataSet();
        for (var i = 0; i < workbooks.NumberOfSheets; i++)
        {
            var sheet = workbooks.GetSheetAt(i);
            var table = new DataTable(sheet.SheetName);
            var headerRow = sheet.GetRow(0);
            if (headerRow == null)
            {
                dataSet.Tables.Add(table);
                continue;
            }
            var cellCount = headerRow.LastCellNum;
            for (var j = headerRow.FirstCellNum; j < cellCount; j++)
            {
                var column = new DataColumn(headerRow.GetCell(j).StringCellValue);
                table.Columns.Add(column);
            }
            var rowCount = sheet.LastRowNum;
            for (var k = sheet.FirstRowNum +1; k <= rowCount; k++)
            {
                var row = sheet.GetRow(k);
                var dataRow = table.NewRow();
                for (var m = 0; m < cellCount; m++)
                {
                    dataRow[m] = row.GetCell(m).ToString();
                }
                table.Rows.Add(dataRow);
            }
            dataSet.Tables.Add(table);
        }
        return dataSet;
    }
    /// <summary>
    /// 输入文件绝对路径,获取解析后的DataSet对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static DataSet? ConvertToDataSet(string path)
    {
        var wookbook = OpenWorkbook(path) ?? throw new Exception("路径");
        return ConvertToDataSet(wookbook);
    }
    /// <summary>
    /// 适用单表，把IWorkbook类型的对象转换成List
    /// </summary>
    /// <typeparam name="T">目标泛型</typeparam>
    /// <param name="wookbook"></param>
    /// <example> IWorkbook workbook;workbook.GetSheetAt(0).ToList<T>();</example>
    /// <returns></returns>
    public static List<T> ConvertToList<T>(this ISheet sheet) where T : new()
    {
        List<T> list = new();
        var headerRow = sheet.GetRow(0);
        if (headerRow == null)
            return list;
        var cellCount = headerRow.LastCellNum;
        var rowCount = sheet.LastRowNum;
        for (var k = sheet.FirstRowNum + 1; k < rowCount; k++)
        {
            var row = sheet.GetRow(k);
            T t = new();
            for (var m = row.FirstCellNum; m < cellCount; m++)
            {
                var cell = row.GetCell(m);
                var value = cell?.ToString();
                if (value == null)
                    continue;
                var property = t.GetType().GetProperty(headerRow.GetCell(m).StringCellValue);
                if (property == null)
                    continue;
                property.SetValue(t, value, null);
            }
            list.Add(t);
        }
        return list;
    }
    /// <summary>
    /// 根据工作表对象返回DataTable
    /// </summary>
    /// <param name="sheet">工作表对象</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">不应传入一个空的工作表对象</exception>
    public static DataTable ConvertToDatatable(this ISheet sheet)
    {
        if (sheet == null)
            throw new ArgumentNullException(nameof(sheet));
        DataTable dataTable = new(sheet.SheetName);
        var columns= new List<DataColumn>();
        var headerRow = sheet.GetRow(0);
        if (headerRow == null)
            return dataTable;
        headerRow.Cells.ForEach(cell =>
        {
            var column = new DataColumn(cell.StringCellValue);
            columns.Add(column);
        });
        dataTable.Columns.AddRange(columns.ToArray());
        var rowCount = sheet.LastRowNum;
        for (var k = sheet.FirstRowNum + 1; k < rowCount; k++)
        {
            var row = sheet.GetRow(k);
            var dataRow = dataTable.NewRow();
            for (var m = 0; m < columns.Count; m++)
            {
                dataRow[m] = row.GetCell(m).ToString();
            }
            dataTable.Rows.Add(dataRow);
        }
        return dataTable;
    }
    /// <summary>
    /// 导出到Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataSource">IEnumerable<T></param>
    /// <param name="path">文件夹路径</param>
    /// <param name="workbookType">工作簿类型</param>
    /// <param name="filename">文件名称</param>

    public static bool ExportToExcel<T>(IEnumerable<T> dataSource,string path, out string message, ExcelWorkbookType workbookType =ExcelWorkbookType.XLSX,string? filename=null)
    {
        bool result = true;
        message = "导出成功";
        try
        {
            IWorkbook workbook = workbookType.Equals(ExcelWorkbookType.XLSX) ? new XSSFWorkbook() : new HSSFWorkbook();
            T t = dataSource.FirstOrDefault();
            filename ??= typeof(T).Name;
            ISheet sheet = workbook.CreateSheet(filename);
            IRow headerRow = sheet.CreateRow(0);
            var props = typeof(T).GetProperties().Where(x => x.PropertyType.IsPublic);
            int count = props.Count(); //T类型公开属性的数量
            for (int i = 0; i < count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(props.ElementAt(i).Name);
            }
            var length = dataSource.Count();//数据源的行数
            for (int i = 0; i < length; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < count; j++)
                {
                    row.CreateCell(j).SetCellValue(props.ElementAt(j).GetValue(dataSource.ElementAt(i)).ToString());
                }
            }
            string filePath = $"{path}{filename}";
            string extension = workbookType.Equals(ExcelWorkbookType.XLSX) ? ".xlsx" : ".xls";
            int num = 1;
            while (File.Exists(filePath + extension))
            {
                filePath = $"{path}{filename}({num})";
                num++;
            }
            using var fs = new FileStream(filePath + extension, FileMode.Create, FileAccess.Write);
            workbook.Write(fs);
        }
        catch (Exception e)
        {
            result = false;
            message = $"导出失败：{e.Message}";
        }
        return result;
        
    }
    /// <summary>
    /// DataTable导出到Excel
    /// </summary>
    /// <param name="dataTable">数据源</param>
    /// <param name="path">保存路径</param>
    /// <param name="workbookType">工作簿类型</param>
    /// <param name="filename">文件名</param>
    public static bool ExportToExcel(this DataTable dataTable, string path, out string message, ExcelWorkbookType workbookType = ExcelWorkbookType.XLSX, string? filename = null)
    {
        bool result = true;
        message = string.Empty;
        try
        {
            IWorkbook workbook = workbookType.Equals(ExcelWorkbookType.XLSX) ? new XSSFWorkbook() : new HSSFWorkbook();
            filename = string.IsNullOrEmpty(filename)
                ? string.IsNullOrEmpty(dataTable.TableName)
                    ? DateTime.Now.ToString("yyyyMMddHHmmss")
                    : dataTable.TableName
                : filename;
            ISheet sheet = workbook.CreateSheet(filename);
            IRow headerRow = sheet.CreateRow(0);
            ;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(dataTable.Columns[i].ColumnName);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
                }
            }
            string filePath = $"{path}{filename}";
            string extension = workbookType.Equals(ExcelWorkbookType.XLSX) ? ".xlsx" : ".xls";
            int num = 1;
            while (File.Exists(filePath + extension))
            {
                filePath = $"{path}{filename}({num})";
                num++;
            }
            using var fs = new FileStream(filePath + extension, FileMode.Create, FileAccess.Write);

            workbook.Write(fs);
        }
        catch (Exception e)
        {
            result = false;
            message = e.Message;
        }
        return result;
        
    }
}

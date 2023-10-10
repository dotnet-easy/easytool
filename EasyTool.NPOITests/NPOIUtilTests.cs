using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool.NPOI;

using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using System.Data;
using System.Text.Json;

namespace EasyTool.NPOITests;

[TestClass()]
public class NPOIUtilTests
{
    string path = @"../TempClass.xlsx";
    List<TempClass> dataList = new List<TempClass>()
    {
        new TempClass()
        {
            Name = "张三",
            Age = 1,
            Birthday = DateTime.Now
        },
        new TempClass()
        {
            Name = "李四",
            Age = 11,
            Birthday = DateTime.Now.AddDays(1)
        },
        new TempClass()
        {
            Name = "王五",
            Age = 23,
            Birthday = DateTime.Now.AddMonths(1)
        }
    };
    DataTable dataTable;
    public NPOIUtilTests()
    {
        dataTable = new DataTable();
        dataTable.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("Name",typeof(string)),
            new DataColumn("Age",typeof(int)),
            new DataColumn("Birthday",typeof(DateTime))
        });
        foreach (var item in dataList)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow["Name"] = item.Name;
            dataRow["Age"] = item.Age;
            dataRow["Birthday"] = item.Birthday;
            dataTable.Rows.Add(dataRow);
        }
    }

    [TestMethod]
    public void Test_ExportToExcel()
    {
        bool res = NPOIUtil.ExportToExcel(dataList, "../", out string msg);
        bool res2 = NPOIUtil.ExportToExcel(dataList, "../", out string msg2, ExcelWorkbookType.XLS);
        Assert.IsTrue(res && res2);
    }
    [TestMethod]
    public void Test_ExportToExcelFromDatatable()
    {
        bool res = NPOIUtil.ExportToExcel(dataTable, "../", out string msg);
        bool res2 = NPOIUtil.ExportToExcel(dataTable, "../", out string msg2, ExcelWorkbookType.XLS);
        Assert.IsTrue(res && res2);

    }
    [TestMethod]
    public void Test_OpenWorkbookFromPath()
    {
        var workbook = NPOIUtil.OpenWorkbook(path);
        Console.WriteLine(workbook.GetSheetName(0));
        Assert.IsNotNull(workbook);
    }
    [TestMethod]
    public void Test_OpenWorkbookFromStream()
    {
        using Stream stream = File.OpenRead(path);
        var workbook = NPOIUtil.OpenWorkbookFromStream(stream, ExcelWorkbookType.XLSX);
        Console.WriteLine(workbook.GetSheetName(0));
        Assert.IsNotNull(workbook);
    }
    [TestMethod]
    public void Test_ConvertToDataSet()
    {
        var workbook = NPOIUtil.OpenWorkbook(path);
        var dataSet = workbook.ConvertToDataSet();
        Console.WriteLine(dataSet?.DataSetName);
        Assert.IsNotNull(dataSet);
    }
    [TestMethod]
    public void Test_ConvertToDataTable()
    {
        var workbook = NPOIUtil.OpenWorkbook(path);
        var dataTable = workbook.GetSheetAt(0).ConvertToDatatable();
        Console.WriteLine(dataTable.TableName);
        Assert.IsNotNull(dataTable);
    }
    [TestMethod]
    public void Test_ConvertToList<T>() where T : new()
    {
        var workbook = NPOIUtil.OpenWorkbook(path);
        List<T> dataList = workbook.GetSheetAt(0).ConvertToList<T>();
        Console.WriteLine(JsonSerializer.Serialize(dataList));
        Assert.IsNotNull(dataList);
    }
}
class TempClass
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime Birthday { get; set; }
}
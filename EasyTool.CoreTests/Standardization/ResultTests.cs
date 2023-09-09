using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EasyTool.Tests
{
    [TestClass()]
    public class ResultTests
    {
        [TestMethod()]
        public void ResultTest()
        {
            var ok = Result.Ok("成功啦");
            Assert.IsTrue(ok.IsOK && ok.Message == "成功啦");

            var okData = Result.Ok<DateTime>(DateTime.Now.Date);
            Assert.IsTrue(okData.IsOK && okData.Data == DateTime.Now.Date);

            var okDataSet = Result.OkSet<int>(new List<int>() { 1, 2, 3 }, 10);
            Assert.IsTrue(okDataSet.IsOK && okDataSet.Data.Sum() == 6 && okDataSet.Total == 10);

            var fail = Result.Fail("失败啦");
            Assert.IsTrue(fail.IsOK == false && fail.Message == "失败啦");
        }
    }
}


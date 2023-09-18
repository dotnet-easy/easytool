using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool.Web.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyTool.WebTests
{
    [TestClass()]
    public class BuildWebApiToTSTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var toDto = BuildWebApiToTS.Build(this.GetType().Assembly);
            Assert.IsTrue(toDto.Contains("GetTest"));
            Assert.IsTrue(toDto.Contains("PostTest"));
        }
    }

    [ApiController]
    public class BuildTestController
    {
        [ApiComments("GetTest Api")]
        [HttpGet]
        public string GetTest()
        {
            return null;
        }

        [ApiComments("PostTest Api")]
        [HttpPost]
        public string PostTest()
        {
            return null;
        }
    }

}
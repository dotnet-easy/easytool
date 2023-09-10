using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTool.Tests
{
    [TestClass()]
    public class BuildDtoToTSTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var toDto = BuildDtoToTS.Build(this.GetType().Assembly);
            Assert.IsTrue(toDto.Contains("BuildDtoTest"));
        }
    }

    [DtoComments("C#编译TS示例Dto")]
    public class BuildDtoTest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
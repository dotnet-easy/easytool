using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EasyTool.Tests
{
    [TestClass()]
    public class BuildOptionToTSTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var toDto = BuildOptionToTS.Build(this.GetType().Assembly);
            Assert.IsTrue(toDto.Contains("BuildOptionTest"));
        }


    }

    [OptionComments("C#编译TS示例Option")]
    public class BuildOptionTest : IOption
    {
        [DisplayName("调试")]
        public static string Debug { get; set; } = nameof(Debug);
        [DisplayName("消息")]
        public static string Info { get; set; } = nameof(Info);
        [DisplayName("警告")]
        public static string Warning { get; set; } = nameof(Warning);
        [DisplayName("错误")]
        public static string Error { get; set; } = nameof(Error);
    }
}
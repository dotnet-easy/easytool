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
    public class OptionExtensionTests
    {
        [TestMethod()]
        public void ToOptionsTest()
        {
            var options = new LogLevel().ToOptions();
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Count == 4);
            Assert.IsTrue(options[0].Value == "Debug");
            Assert.IsTrue(options[0].Text == "调试");

        }

        [TestMethod()]
        public void GetOptionsTest()
        {
            var options = IOption.GetOptions<LogLevel>();
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Count == 4);
            Assert.IsTrue(options[0].Value == "Debug");
            Assert.IsTrue(options[0].Text == "调试");
        }

        public class LogLevel : IOption
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
}


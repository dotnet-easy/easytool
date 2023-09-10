using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTool.Tests
{
    [TestClass()]
    public class GuidUtilTests
    {
        [TestMethod()]
        public void NextGuid()
        {
            Guid guid1 = GuidUtil.NextGuid();
            Thread.Sleep(10);
            Guid guid2 = GuidUtil.NextGuid();
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void NextUUID()
        {
            var uuid1 = GuidUtil.NextUUID();
            Thread.Sleep(10);
            var uuid2 = GuidUtil.NextUUID();

            Assert.IsTrue(true);
        }
    }
}
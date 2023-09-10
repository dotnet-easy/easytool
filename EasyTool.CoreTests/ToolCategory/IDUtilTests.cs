using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasyTool.Tests
{
    [TestClass]
    public class IdUtilTests
    {
        [TestMethod]
        public void NextSequenceUUID_AreGreaterThan()
        {
            var uuid1 = IdUtil.UUID(UUIDStyle.Sequence);
            Thread.Sleep(10);
            var uuid2 = IdUtil.UUID(UUIDStyle.Sequence);

            Assert.IsTrue(uuid2.ToString().CompareTo(uuid1.ToString()) > 0);
        }
    }
}
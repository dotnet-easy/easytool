using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool.Tests
{
    [TestClass()]
    public class MathUtilTests
    {
        [TestMethod()]
        public void GcdTest()
        {
            var result = MathUtil.Gcd(5, 20);
            Assert.IsTrue(result == 5);
        }
    }
}
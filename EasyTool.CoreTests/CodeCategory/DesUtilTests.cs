using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool.CodeCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTool.CodeCategory.Tests
{
    [TestClass()]
    public class DesUtilTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            var input = "abbfly";
            var sk = "fj2#)ks!";
            var en = DesUtil.Encrypt(input, sk);
            var de = DesUtil.Decrypt(en, sk);
            Assert.IsTrue(de == input);
        }
    }
}
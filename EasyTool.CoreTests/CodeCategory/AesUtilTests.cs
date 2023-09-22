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
    public class AesUtilTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            var input = "abbfly";
            var sk = "!@#sfj2#)ksk";
            var en = AesUtil.Encrypt(input, sk);
            var de = AesUtil.Decrypt(en, sk);
            Assert.IsTrue(de == input);
        }
    }
}
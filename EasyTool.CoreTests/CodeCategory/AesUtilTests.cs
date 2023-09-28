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
        public void EncryptSecret16Test()
        {
            var input = "abbfly";
            var sk = "1234567890123456";
            var en = AesUtil.Encrypt(input, sk);
            var de = AesUtil.Decrypt(en, sk);
            Assert.IsTrue(de == input);
        }

        [TestMethod()]
        public void EncryptSecret24Test()
        {
            var input = "abbfly";
            var sk = "123456789012345678901234";
            var en = AesUtil.Encrypt(input, sk);
            var de = AesUtil.Decrypt(en, sk);
            Assert.IsTrue(de == input);
        }

        [TestMethod()]
        public void EncryptSecret32Test()
        {
            var input = "abbfly";
            var sk = "12345678901234567890123456789012";
            var en = AesUtil.Encrypt(input, sk);
            var de = AesUtil.Decrypt(en, sk);
            Assert.IsTrue(de == input);
        }
    }
}
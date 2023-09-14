using EasyTool.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyTool.Tests
{
    [TestClass()]
    public class EmitMapperExtensionTests
    {
        [TestMethod()]
        public void EmitMapperTest()
        {
            var obj1 = new First()
            {
                MyProperty1 = 1,
                MyProperty2 = "A"
            };

            var obj2 = obj1.EmitMapTo<First, Second>();

            Assert.AreEqual(obj1.MyProperty1, obj2.MyProperty1);
            Assert.AreEqual(obj1.MyProperty2, obj2.MyProperty2);
        }

        [Serializable]
        public class First
        {
            public int MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }
        }

        [Serializable]
        public class Second
        {
            public int MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }
        }
    }
}

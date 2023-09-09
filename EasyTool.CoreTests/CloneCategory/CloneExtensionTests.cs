using EasyTool.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyTool.Tests
{
    [TestClass()]
    public class CloneExtensionTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            var obj1 = new First()
            {
                MyProperty1 = 1,
                MyProperty2 = "A",
                Second1 = new Second()
                {
                    MyProperty1 = 2,
                    MyProperty2 = "B",
                },
                Second2 = new Second()
                {
                    MyProperty1 = 3,
                    MyProperty2 = "C",
                }
            };
            var obj2 = obj1.Clone();

            Assert.AreEqual(obj1.MyProperty1, obj2.MyProperty1);
            Assert.AreEqual(obj1.Second1.MyProperty1, obj2.Second1.MyProperty1);
        }

        [Serializable]
        public class First
        {
            public int MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }

            public Second Second1 { get; set; }

            public Second Second2 { get; set; }
        }

        [Serializable]
        public class Second
        {
            public int MyProperty1 { get; set; }
            public string MyProperty2 { get; set; }
        }
    }
}
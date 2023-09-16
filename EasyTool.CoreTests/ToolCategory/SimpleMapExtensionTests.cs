using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyTool;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;

namespace EasyTool.Tests
{
    [TestClass()]
    public class SimpleMapExtensionsTests
    {
        [TestMethod()]
        public void SimpleMapTest()
        {
            ClassA classA = new ClassA()
            {
                MyProperty = 1,
                MyProperty1 = 2,
                MyProperty2 = "3",
                MyProperty3 = null,
            };
            ClassB classB = classA.SimpleMapTo<ClassA, ClassB>();
            Console.WriteLine(classB);
            //输出{"MyProperty":1,"MyProperty1":"2","MyProperty2":3,"MyProperty3":null}
           

        }
        [TestMethod()]
        public void ListSimpleMapTest()
        {
            ClassA classA = new ClassA()
            {
                MyProperty = 1,
                MyProperty1 = 2,
                MyProperty2 = "3",
                MyProperty3 = null,
            };
            List<ClassA> listA = new() { classA, classA, classA };
            var listB = listA.SimpleMapTo<ClassA, ClassB>();
            Console.WriteLine(JsonSerializer.Serialize(listB));
            //输出[{"MyProperty":1,"MyProperty1":"2","MyProperty2":3,"MyProperty3":null},
            //{"MyProperty":1,"MyProperty1":"2","MyProperty2":3,"MyProperty3":null},
            //{"MyProperty":1,"MyProperty1":"2","MyProperty2":3,"MyProperty3":null}]
        }
        class ClassA
        {
            public int MyProperty { get; set; }

            public int MyProperty1 { get; set; }

            public string MyProperty2 { get; set; }
            public string? MyProperty3 { get; set; }
        }
        class ClassB
        {
            public long MyProperty { get; set; }
            public string MyProperty1 { get; set; }

            public long MyProperty2 { get; set; }
            public string MyProperty3 { get; set; }

            public override string ToString() => JsonSerializer.Serialize(this);
        }
    }
}
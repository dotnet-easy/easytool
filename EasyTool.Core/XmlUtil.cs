using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EasyTool
{
    /// <summary>
    /// XML工具类
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 解析XML字符串。
        /// </summary>
        /// <param name="xml">要解析的XML字符串。</param>
        /// <returns>解析后的XML文档对象。</returns>
        public static XmlDocument ParseXml(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        /// <summary>
        /// 从XML文件中加载XML文档。
        /// </summary>
        /// <param name="filePath">XML文件的完整路径。</param>
        /// <returns>XML文档对象。</returns>
        public static XmlDocument LoadXmlFromFile(string filePath)
        {
            var document = new XmlDocument();
            document.Load(filePath);
            return document;
        }

        /// <summary>
        /// 创建新的XML文档。
        /// </summary>
        /// <returns>XML文档对象。</returns>
        public static XmlDocument CreateNewXmlDocument()
        {
            var document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(declaration);
            return document;
        }

        /// <summary>
        /// 创建新的XML元素。
        /// </summary>
        /// <param name="name">元素的名称。</param>
        /// <param name="value">元素的值。</param>
        /// <returns>新创建的XML元素。</returns>
        public static XmlElement CreateXmlElement(string name, string value = null)
        {
            var document = new XmlDocument();
            var element = document.CreateElement(name);
            if (!string.IsNullOrEmpty(value))
            {
                element.InnerText = value;
            }
            return element;
        }

        /// <summary>
        /// 创建新的XML属性。
        /// </summary>
        /// <param name="name">属性的名称。</param>
        /// <param name="value">属性的值。</param>
        /// <returns>新创建的XML属性。</returns>
        public static XmlAttribute CreateXmlAttribute(string name, string value)
        {
            var document = new XmlDocument();
            var attribute = document.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }

        /// <summary>
        /// 从XML文档中获取指定名称的元素。
        /// </summary>
        /// <param name="document">XML文档对象。</param>
        /// <param name="elementName">元素的名称。</param>
        /// <returns>指定名称的XML元素。</returns>
        public static XmlElement GetXmlElement(XmlDocument document, string elementName)
        {
            return document.DocumentElement[elementName];
        }

        /// <summary>
        /// 从XML元素中获取指定名称的子元素。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="childElementName">子元素的名称。</param>
        /// <returns>指定名称的子元素。</returns>
        public static XmlElement GetXmlElement(XmlElement element, string childElementName)
        {
            return (XmlElement)element.SelectSingleNode(childElementName);
        }

        /// <summary>
        /// 从XML元素中获取指定名称的属性。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">属性的名称。</param>
        /// <returns>指定名称的XML属性。</returns>
        public static XmlAttribute GetXmlAttribute(XmlElement element, string attributeName)
        {
            return element.Attributes[attributeName];
        }

        /// <summary>
        /// 从XML元素中获取指定名称的属性的值。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">属性的名称。</param>
        /// <returns>指定名称的属性的值。</returns>
        public static string GetXmlAttributeStringValue(XmlElement element, string attributeName)
        {
            return element.Attributes[attributeName].Value;
        }

        /// <summary>
        /// 从XML元素中获取指定名称的属性的整数值。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">属性的名称。</param>
        /// <returns>指定名称的属性的整数值。</returns>
        public static int GetXmlAttributeIntValue(XmlElement element, string attributeName)
        {
            return int.Parse(element.Attributes[attributeName].Value);
        }

        /// <summary>
        /// 从XML元素中获取指定名称的属性的浮点数值。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">属性的名称。</param>
        /// <returns>指定名称的属性的浮点数值。</returns>
        public static float GetXmlAttributeFloatValue(XmlElement element, string attributeName)
        {
            return float.Parse(element.Attributes[attributeName].Value);
        }

        /// <summary>
        /// 将XML文档保存到指定路径的文件中。
        /// </summary>
        /// <param name="document">XML文档对象。</param>
        /// <param name="filePath">文件路径。</param>
        public static void SaveXmlToFile(XmlDocument document, string filePath)
        {
            document.Save(filePath);
        }

        /// <summary>
        /// 将XML文档转换为字符串。
        /// </summary>
        /// <param name="document">XML文档对象。</param>
        /// <returns>XML文档的字符串表示。</returns>
        public static string XmlToString(XmlDocument document)
        {
            var stringBuilder = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (var writer = XmlWriter.Create(stringBuilder, settings))
            {
                document.Save(writer);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将XML元素转换为字符串。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <returns>XML元素的字符串表示。</returns>
        public static string XmlElementToString(XmlElement element)
        {
            var document = new XmlDocument();
            document.AppendChild(document.ImportNode(element, true));
            return XmlToString(document);
        }

        /// <summary>
        /// 在XML元素中添加子元素。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="childElement">要添加的子元素对象。</param>
        public static void AddXmlElement(XmlElement element, XmlElement childElement)
        {
            element.AppendChild(childElement);
        }

        /// <summary>
        /// 在XML元素中添加属性。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attribute">要添加的属性对象。</param>
        public static void AddXmlAttribute(XmlElement element, XmlAttribute attribute)
        {
            element.Attributes.Append(attribute);
        }

        /// <summary>
        /// 在XML元素中添加具有指定名称和值的属性。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">要添加的属性的名称。</param>
        /// <param name="attributeValue">要添加的属性的值。</param>
        public static void AddXmlAttribute(XmlElement element, string attributeName, string attributeValue)
        {
            var attribute = CreateXmlAttribute(attributeName, attributeValue);
            AddXmlAttribute(element, attribute);
        }

        /// <summary>
        /// 在XML元素中设置指定名称的属性的值。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <param name="attributeName">要设置的属性的名称。</param>
        /// <param name="attributeValue">要设置的属性的值。</param>
        public static void SetXmlAttribute(XmlElement element, string attributeName, string attributeValue)
        {
            var attribute = element.Attributes[attributeName];
            if (attribute != null)
            {
                attribute.Value = attributeValue;
            }
        }

        /// <summary>
        /// 将XML元素转换为字典。
        /// </summary>
        /// <param name="element">XML元素对象。</param>
        /// <returns>包含XML元素的所有属性的键值对字典。</returns>
        public static Dictionary<string, string> XmlElementToDictionary(XmlElement element)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (XmlAttribute attribute in element.Attributes)
            {
                dictionary.Add(attribute.Name, attribute.Value);
            }
            return dictionary;
        }

        /// <summary>
        /// 将字典转换为XML元素。
        /// </summary>
        /// <param name="dictionary">要转换为XML元素的键值对字典。</param>
        /// <param name="elementName">XML元素的名称。</param>
        /// <returns>新创建的XML元素。</returns>
        public static XmlElement DictionaryToXmlElement(Dictionary<string, string> dictionary, string elementName)
        {
            var document = new XmlDocument();
            var element = document.CreateElement(elementName);
            foreach (var pair in dictionary)
            {
                var attribute = CreateXmlAttribute(pair.Key, pair.Value);
                AddXmlAttribute(element, attribute);
            }
            return element;
        }
    }
}

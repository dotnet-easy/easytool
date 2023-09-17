using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyTool.Web.Development
{
    public class BuildOptionToTS
    {
        public static string Build(Assembly assembly)
        {
            List<OptionClass> options = GetOptions(assembly);
            string code = CreateCode(options);
            return code.ToString();
        }

        public static void BuildToFile(Assembly assembly, string path)
        {
            var code = Build(assembly);
            string existsCode = "";
            if (System.IO.File.Exists(path) == true)
                existsCode = System.IO.File.ReadAllText(path);

            if (existsCode != code)
            {
                System.IO.File.WriteAllText(path, code);
            }
        }

        #region 构造代码

        public static string CreateCode(List<OptionClass> options)
        {
            StringBuilder codeOption = new StringBuilder();
            codeOption.AppendLine(@"import { OptionCore, OptionCoreT } from ""src/app/shared/services/result-dto"";");
            codeOption.AppendLine($"");

            StringBuilder codeSet = new StringBuilder();//合并所有枚举
            codeSet.AppendLine($"export const OptionEnum = {{");

            foreach (var option in options)
            {
                /*
export const Global_EMonth: OptionCoreT<number>[] = [
  { Text: '1', Value: 1 },
  { Text: '2', Value: 2 },
];
                 */
                codeOption.AppendLine($"/** {option.Title}  {option.Namespace}*/");
                var optionName = (string.IsNullOrWhiteSpace(option.Entity) ? "" : $"{option.Entity}_") + option.Name;

                var firstProperty = option.Propertys.FirstOrDefault();

                bool isNumber = false;//是否是数字类型（此处偷懒了，假设就只有两种类型）
                if (firstProperty != null && firstProperty.Type == typeof(int))
                {
                    isNumber = true;
                }

                var OptionCoreT = isNumber ? "T<number>" : "";
                codeOption.AppendLine($"export const {optionName}: OptionCore{OptionCoreT}[] = [");

                var ValueT = isNumber ? "" : "'";
                foreach (var property in option.Propertys)
                {
                    codeOption.AppendLine($"  {{ Text: '{property.Text}', Value: {ValueT}{property.Value}{ValueT} }},");
                }
                codeOption.AppendLine($"];");

                /*
export enum WorkRecord_EOperatingEnum {
  新建 = '新建',
};
                 */
                codeOption.AppendLine($"export enum {optionName}Enum {{");
                foreach (var property in option.Propertys)
                {
                    string text = property.Text;
                    if (property.Text[0] >= '0' && property.Text[0] <= '9')
                        text = "_" + text;

                    codeOption.AppendLine($"  {text} = {ValueT}{property.Value}{ValueT},");
                }
                codeOption.AppendLine($"}};");

                codeOption.AppendLine($"");

                codeSet.AppendLine($"  /** {option.Title}  {option.Namespace}*/");
                codeSet.AppendLine($"  {optionName}: {optionName},");
            }
            codeSet.AppendLine($"}}");

            codeOption.AppendLine(codeSet.ToString());
            return codeOption.ToString();
        }


        public static List<OptionClass> GetOptions(Assembly assembly)
        {
            List<OptionClass> dtos = new List<OptionClass>();

            var optionCommentTypes = assembly.GetTypes().Where(x => x.GetCustomAttributes(typeof(OptionCommentsAttribute), false).Count() > 0);
            foreach (var dtoCommentType in optionCommentTypes)
            {
                var dto = new OptionClass(dtoCommentType.Name, dtoCommentType.Namespace);

                dto.Title = dtoCommentType.GetCustomAttribute<OptionCommentsAttribute>()?.Title ?? "";
                dto.Entity = dtoCommentType.GetCustomAttribute<OptionCommentsAttribute>()?.Entity;

                var propertyTypes = dtoCommentType.GetProperties();
                foreach (var propertyType in propertyTypes)
                {
                    var property = new OptionProperty();
                    property.Text = propertyType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyType.Name;
                    property.Value = propertyType.GetValue(null, null)?.ToString() ?? propertyType.Name;
                    property.Type = propertyType.PropertyType;
                    dto.Propertys.Add(property);
                }

                dtos.Add(dto);
            }

            return dtos;
        }


        #endregion

        public record OptionClass
        {
            public OptionClass(string name, string _namespace)
            {
                Name = name;
                Namespace = _namespace;
            }

            public string Name { get; set; }
            public string Namespace { get; set; }

            public string Title { get; set; }//类名称
            public string Entity { get; set; }
            public List<OptionProperty> Propertys { get; set; } = new List<OptionProperty>();

        }


        public record OptionProperty()
        {
            public string Text { get; set; }//显示的文本

            public string Value { get; set; }//值

            public Type Type { get; set; }//值
        }

    }

    public class OptionCommentsAttribute : Attribute
    {
        public string Title { get; set; }

        //标注实体，用于区分相同的选项
        public string Entity { get; set; }
        public OptionCommentsAttribute(string title, string entity = "")
        {
            Entity = entity;
            Title = title;
        }
    }
}

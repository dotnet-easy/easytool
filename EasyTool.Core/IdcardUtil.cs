using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    public class IdcardUtil
    {
        /// <summary>
        /// 验证身份证号码是否合法
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>验证结果</returns>
        public static bool IsValid(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return false;
            }

            if (idcard.Length == 15)
            {
                return IsValid15(idcard);
            }
            else if (idcard.Length == 18)
            {
                return IsValid18(idcard);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证 15 位身份证号码是否合法
        /// </summary>
        /// <param name="idcard">15 位身份证号码</param>
        /// <returns>验证结果</returns>
        public static bool IsValid15(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return false;
            }

            if (!Regex.IsMatch(idcard, @"^\d{15}$"))
            {
                return false;
            }

            if (!IsValidArea(idcard.Substring(0, 6)))
            {
                return false;
            }

            DateTime birthday;
            if (!DateTime.TryParseExact(idcard.Substring(6, 6), "yyMMdd", null, System.Globalization.DateTimeStyles.None, out birthday))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证 18 位身份证号码是否合法
        /// </summary>
        /// <param name="idcard">18 位身份证号码</param>
        /// <returns>验证结果</returns>
        public static bool IsValid18(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return false;
            }

            if (!Regex.IsMatch(idcard, @"^\d{17}[\dX]$"))
            {
                return false;
            }

            if (!IsValidArea(idcard.Substring(0, 6)))
            {
                return false;
            }

            DateTime birthday;
            if (!DateTime.TryParseExact(idcard.Substring(6, 8), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out birthday))
            {
                return false;
            }

            if (!IsValidChecksum(idcard))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断给定的区域代码是否合法
        /// </summary>
        /// <param name="area">区域代码</param>
        /// <returns>是否合法</returns>
        public static bool IsValidArea(string area)
        {
            if (string.IsNullOrEmpty(area))
            {
                return false;
            }

            string[] areas = new string[] {
        "11", "12", "13", "14", "15", "21", "22", "23", "31", "32",
        "33", "34", "35", "36", "37", "41", "42", "43", "44", "45",
        "46", "50", "51", "52", "53", "54", "61", "62", "63", "64",
        "65"
    };

            return areas.Contains(area);
        }

        /// <summary>
        /// 验证身份证号码的校验位是否正确
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>验证结果</returns>
        public static bool IsValidChecksum(string idcard)
        {
            int[] weights = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            string[] checksums = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };

            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(idcard[i].ToString()) * weights[i];
            }

            int checksumIndex = sum % 11;
            string expectedChecksum = checksums[checksumIndex];

            return idcard[17].ToString().ToUpper() == expectedChecksum.ToUpper();
        }

        /// <summary>
        /// 从身份证号码中获取生日
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>生日</returns>
        public static DateTime? GetBirthday(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return null;
            }

            if (idcard.Length == 15)
            {
                if (!IsValid15(idcard))
                {
                    return null;
                }

                DateTime birthday;
                if (DateTime.TryParseExact(idcard.Substring(6, 6), "yyMMdd", null, System.Globalization.DateTimeStyles.None, out birthday))
                {
                    return birthday;
                }
                else
                {
                    return null;
                }
            }
            else if (idcard.Length == 18)
            {
                if (!IsValid18(idcard))
                {
                    return null;
                }

                DateTime birthday;
                if (DateTime.TryParseExact(idcard.Substring(6, 8), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out birthday))
                {
                    return birthday;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从身份证号码中获取性别
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>性别</returns>
        public static Gender? GetGender(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return null;
            }

            if (idcard.Length == 15)
            {
                if (!IsValid15(idcard))
                {
                    return null;
                }

                int genderCode = int.Parse(idcard.Substring(14, 1));
                return genderCode % 2 == 1 ? Gender.Male : Gender.Female;
            }
            else if (idcard.Length == 18)
            {
                if (!IsValid18(idcard))
                {
                    return null;
                }

                int genderCode = int.Parse(idcard.Substring(16, 1));
                return genderCode % 2 == 1 ? Gender.Male : Gender.Female;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从身份证号码中获取
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>省份</returns>
        public static string GetProvince(string idcard)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return null;
            }

            if (IsValid(idcard))
            {
                string[] areas = new string[] {
            "11", "12", "13", "14", "15", "21", "22", "23", "31", "32",
            "33", "34", "35", "36", "37", "41", "42", "43", "44", "45",
            "46", "50", "51", "52", "53", "54", "61", "62", "63", "64",
            "65"
        };

                string provinceCode = idcard.Substring(0, 2);
                if (areas.Contains(provinceCode))
                {
                    switch (provinceCode)
                    {
                        case "11":
                            return "北京市";
                        case "12":
                            return "天津市";
                        case "13":
                            return "河北省";
                        case "14":
                            return "山西省";
                        case "15":
                            return "内蒙古自治区";
                        case "21":
                            return "辽宁省";
                        case "22":
                            return "吉林省";
                        case "23":
                            return "黑龙江省";
                        case "31":
                            return "上海市";
                        case "32":
                            return "江苏省";
                        case "33":
                            return "浙江省";
                        case "34":
                            return "安徽省";
                        case "35":
                            return "福建省";
                        case "36":
                            return "江西省";
                        case "37":
                            return "山东省";
                        case "41":
                            return "河南省";
                        case "42":
                            return "湖北省";
                        case "43":
                            return "湖南省";
                        case "44":
                            return "广东省";
                        case "45":
                            return "广西壮族自治区";
                        case "46":
                            return "海南省";
                        case "50":
                            return "重庆市";
                        case "51":
                            return "四川省";
                        case "52":
                            return "贵州省";
                        case "53":
                            return "云南省";
                        case "54":
                            return "西藏自治区";
                        case "61":
                            return "陕西省";
                        case "62":
                            return "甘肃省";
                        case "63":
                            return "青海省";
                        case "64":
                            return "宁夏回族自治区";
                        case "65":
                            return "新疆维吾尔自治区";
                        default:
                            return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将身份证号码中的生日部分替换成指定的日期，并返回新的身份证号码
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <param name="birthday">新的生日日期</param>
        /// <returns>新的身份证号码</returns>
        public static string ReplaceBirthday(string idcard, DateTime birthday)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return null;
            }

            if (!IsValid(idcard))
            {
                return null;
            }

            string birthdayStr = birthday.ToString("yyyyMMdd");
            if (idcard.Length == 15)
            {
                return idcard.Substring(0, 6) + birthdayStr + idcard.Substring(12);
            }
            else if (idcard.Length == 18)
            {
                return idcard.Substring(0, 6) + birthdayStr + idcard.Substring(14);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将身份证号码中的性别部分替换成指定的性别，并返回新的身份证号码
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <param name="gender">新的性别</param>
        /// <returns>新的身份证号码</returns>
        public static string ReplaceGender(string idcard, Gender gender)
        {
            if (string.IsNullOrEmpty(idcard))
            {
                return null;
            }

            if (!IsValid(idcard))
            {
                return null;
            }

            int genderCode = gender == Gender.Male ? 1 : 2;
            if (idcard.Length == 15)
            {
                return idcard.Substring(0, 14) + genderCode.ToString();
            }
            else if (idcard.Length == 18)
            {
                return idcard.Substring(0, 16) + genderCode.ToString() + idcard.Substring(17);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 生成一个随机的身份证号码
        /// </summary>
        /// <param name="gender">性别</param>
        /// <param name="minAge">最小年龄</param>
        /// <param name="maxAge">最大年龄</param>
        /// <returns>随机的身份证号码</returns>
        public static string GenerateRandomIdcard(Gender gender = Gender.Male, int minAge = 18, int maxAge = 65)
        {
            DateTime now = DateTime.Now;
            DateTime minBirthday = now.AddYears(-maxAge);
            DateTime maxBirthday = now.AddYears(-minAge);

            DateTime birthday = RandomUtil.GetRandomDateTime(minBirthday, maxBirthday);
            string area = GetRandomArea();
            int sequence = RandomUtil.GetRandomInt(1, 999);
            int genderCode = gender == Gender.Male ? 1 : 2;

            string idcard = string.Format("{0}{1:yyyyMMdd}{2:D3}{3:D1}", area, birthday, sequence, genderCode);
            if (!IsValid(idcard))
            {
                return GenerateRandomIdcard(gender, minAge, maxAge);
            }
            else
            {
                return idcard;
            }
        }

        /// <summary>
        /// 获取一个随机的身份证号码的区域代码
        /// </summary>
        /// <returns>区域代码</returns>
        private static string GetRandomArea()
        {
            string[] areas = new string[] {
        "11", "12", "13", "14", "15", "21", "22", "23", "31", "32",
        "33", "34", "35", "36", "37", "41", "42", "43", "44", "45",
        "46", "50", "51", "52", "53", "54", "61", "62", "63", "64",
        "65"
    };

            int index = RandomUtil.GetRandomInt(0, areas.Length - 1);
            return areas[index];
        }


        /// <summary>
        /// 性别枚举
        /// </summary>
        public enum Gender
        {
            /// <summary>
            /// 男性
            /// </summary>
            Male,
            /// <summary>
            /// 女性
            /// </summary>
            Female
        }
    }
}

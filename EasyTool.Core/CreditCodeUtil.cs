using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 社会信用代码工具
    /// </summary>
    public class CreditCodeUtil
    {
        private const string BaseCode = "0123456789ABCDEFGHJKLMNPQRTUWXY"; // 社会信用代码中的基础字符集
        private const int Modulo = 31; // 校验码计算中的模数

        /// <summary>
        /// 检查社会信用代码是否有效
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return false;
            }

            // 将社会信用代码中的每个字符转换为对应的数字，再计算出校验码
            int sum = 0;
            for (int i = 0; i < creditCode.Length - 1; i++)
            {
                int code = BaseCode.IndexOf(creditCode[i]);
                int weight = GetWeight(i + 1);
                sum += code * weight;
            }

            int checkCode = (Modulo - sum % Modulo) % Modulo;
            int lastCode = BaseCode.IndexOf(creditCode[17]);

            return checkCode == lastCode;
        }

        /// <summary>
        /// 获取指定位置的数字权重
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>数字权重</returns>
        private static int GetWeight(int position)
        {
            if (position <= 1 || position == 9)
            {
                return 1;
            }
            else if (position == 2)
            {
                return 9;
            }
            else
            {
                return 9 - position + 2;
            }
        }

        /// <summary>
        /// 生成随机的社会信用代码
        /// </summary>
        /// <returns>随机的社会信用代码</returns>
        public static string GenerateRandomCreditCode()
        {
            string orgCode = "911101"; // 默认的组织机构代码
            string entType = "00"; // 默认的企业类型
            string regNum = RandomUtil.RandomNumberString(10); // 生成随机的注册号
            string code = orgCode + entType + regNum;

            // 计算出校验码并添加到社会信用代码中
            int sum = 0;
            for (int i = 0; i < code.Length; i++)
            {
                int weight = GetWeight(i + 1);
                int digit = BaseCode.IndexOf(code[i]);
                sum += digit * weight;
            }

            int checkCode = (Modulo - sum % Modulo) % Modulo;
            return code + BaseCode[checkCode];
        }

        /// <summary>
        /// 从社会信用代码中提取组织机构代码
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>组织机构代码</returns>
        public static string GetOrgCodeFromCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return null;
            }
                return creditCode.Substring(0, 9);
            }
        /// <summary>
        /// 从社会信用代码中提取企业类型
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>企业类型</returns>
        public static string GetEntTypeFromCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return null;
            }

            return creditCode.Substring(9, 2);
        }

        /// <summary>
        /// 从社会信用代码中提取注册号
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>注册号</returns>
        public static string GetRegNumFromCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return null;
            }

            return creditCode.Substring(11, 10);
        }

        /// <summary>
        /// 从社会信用代码中提取行政区划码
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>行政区划码</returns>
        public static string GetAreaCodeFromCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return null;
            }

            return creditCode.Substring(2, 6);
        }

        /// <summary>
        /// 从社会信用代码中提取机构类型
        /// </summary>
        /// <param name="creditCode">社会信用代码</param>
        /// <returns>机构类型</returns>
        public static string GetOrgTypeFromCreditCode(string creditCode)
        {
            if (string.IsNullOrWhiteSpace(creditCode) || creditCode.Length != 18)
            {
                return null;
            }

            return creditCode[8].ToString();
        }

    }
}

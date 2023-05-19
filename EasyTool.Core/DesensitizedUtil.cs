using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyTool
{
    /// <summary>
    /// 信息脱敏工具类
    /// </summary>
    public class DesensitizedUtil
    {
        private static readonly Regex IdcardRegex = new Regex(@"^\d{15}(\d{2}[0-9xX])?$");
        private static readonly Regex MobileRegex = new Regex(@"^(13\d|14[5-9]|15[^4\D]|16\d|17[0-8]|18\d|19[0-3,5-9])\d{8}$");
        private static readonly Regex TelRegex = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");
        private static readonly Regex EmailRegex = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        private static readonly Regex BankcardRegex = new Regex(@"^\d{12,19}$");
        private static readonly string[] AreaCodes = new string[] {
        "11", "12", "13", "14", "15", "21", "22", "23", "31", "32",
        "33", "34", "35", "36", "37", "41", "42", "43", "44", "45",
        "46", "50", "51", "52", "53", "54", "61", "62", "63", "64",
        "65"
    };

        /// <summary>
        /// 脱敏用户ID，只保留前两位和后两位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>脱敏后的用户ID</returns>
        public static string UserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) || userId.Length <= 4)
            {
                return userId;
            }

            string prefix = userId.Substring(0, 2);
            string suffix = userId.Substring(userId.Length - 2, 2);
            return prefix + new string('*', userId.Length - 4) + suffix;
        }

        /// <summary>
        /// 脱敏中文姓名，只保留第一个汉字和最后一个汉字，其他用*代替
        /// </summary>
        /// <param name="name">中文姓名</param>
        /// <returns>脱敏后的中文姓名</returns>
        public static string ChineseName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length <= 2)
            {
                return name;
            }

            string firstChar = name.Substring(0, 1);
            string lastChar = name.Substring(name.Length - 1, 1);
            return firstChar + new string('*', name.Length - 2) + lastChar;
        }

        /// <summary>
        /// 脱敏身份证号码，只保留前两位和后四位
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>脱敏后的身份证号码</returns>
        public static string Idcard(string idcard)
        {
            if (string.IsNullOrWhiteSpace(idcard) || !IdcardRegex.IsMatch(idcard))
            {
                return idcard;
            }

            string prefix = idcard.Substring(0, 4);
            string suffix = idcard.Substring(idcard.Length - 4, 4);
            return prefix + new string('*', idcard.Length - 8) + suffix;
        }

        /// <summary>
        /// 脱敏座机号码，只保留前三位和后四位
        /// </summary>
        /// <param name="tel">座机号码</param>
        /// <returns>脱敏后的座机号码</returns>
        public static string Tel(string tel)
        {
            if (string.IsNullOrWhiteSpace(tel) || !TelRegex.IsMatch(tel))
            {
                return tel;
            }

            int startIndex = tel.Length - 4 > 0 ? tel.Length - 4 : 0;
            string suffix = tel.Substring(startIndex, 4);
            return tel.Substring(0, 3) + new string('*', startIndex) + suffix;
        }

        /// <summary>
        /// 脱敏手机号码，只保留前三位和后四位
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>脱敏后的手机号码</returns>
        public static string Mobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile) || !MobileRegex.IsMatch(mobile))
            {
                return mobile;
            }

            string prefix = mobile.Substring(0, 3);
            string suffix = mobile.Substring(mobile.Length - 4, 4);
            return prefix + new string('*', mobile.Length - 7) + suffix;
        }

        /// <summary>
        /// 脱敏地址信息，只保留前五个字符和后三个字符
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns>脱敏后的地址信息</returns>
        public static string Address(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || address.Length <= 8)
            {
                return address;
            }

            string prefix = address.Substring(0, 5);
            string suffix = address.Substring(address.Length - 3, 3);
            return prefix + new string('*', address.Length - 8) + suffix;
        }

        /// <summary>
        /// 脱敏电子邮件，只保留邮箱前缀的前三个字符和后两个字符
        /// </summary>
        /// <param name="email">电子邮件</param>
        /// <returns>脱敏后的电子邮件</returns>
        public static string Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            {
                return email;
            }

            int atIndex = email.IndexOf('@');
            if (atIndex <= 3)
            {
                return email;
            }

            string prefix = email.Substring(0, 3);
            string suffix = email.Substring(atIndex - 2, 2);
            return prefix + new string('*', atIndex - 5) + suffix + email.Substring(atIndex);
        }

        /// <summary>
        /// 脱敏密码，只保留前两个字符和后两个字符
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>脱敏后的密码</returns>
        public static string Password(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length <= 4)
            {
                return password;
            }
            string prefix = password.Substring(0, 2);
            string suffix = password.Substring(password.Length - 2, 2);
            return prefix + new string('*', password.Length - 4) + suffix;
        }

        /// <summary>
        /// 脱敏中国大陆车牌号，只保留前两个字符和最后一个字符
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <returns>脱敏后的车牌号</returns>
        public static string PlateNumber(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber) || plateNumber.Length <= 3)
            {
                return plateNumber;
            }

            string prefix = plateNumber.Substring(0, 2);
            string suffix = plateNumber.Substring(plateNumber.Length - 1, 1);
            return prefix + new string('*', plateNumber.Length - 3) + suffix;
        }

        /// <summary>
        /// 脱敏银行卡号，只保留前四位和后四位
        /// </summary>
        /// <param name="bankcard">银行卡号</param>
        /// <returns>脱敏后的银行卡号</returns>
        public static string Bankcard(string bankcard)
        {
            if (string.IsNullOrWhiteSpace(bankcard) || !BankcardRegex.IsMatch(bankcard))
            {
                return bankcard;
            }

            string prefix = bankcard.Substring(0, 4);
            string suffix = bankcard.Substring(bankcard.Length - 4, 4);
            return prefix + new string('*', bankcard.Length - 8) + suffix;
        }
    }
}

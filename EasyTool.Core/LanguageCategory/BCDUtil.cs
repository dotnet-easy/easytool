using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// BCD工具
    /// </summary>
    public class BCDUtil
    {
        /// <summary>
        /// 将一个十进制数转换成对应的二进制码数组
        /// </summary>
        /// <param name="dec">需要转换的十进制数</param>
        /// <returns>二进制码数组</returns>
        public static int[] DecToBinaryArray(int dec)
        {
            if (dec < 0)
            {
                throw new ArgumentException("dec必须是非负整数。");
            }

            if (dec == 0)
            {
                return new int[] { 0 };
            }

            int[] binaryArray = new int[32];
            int index = 0;

            while (dec > 0)
            {
                binaryArray[index++] = dec % 2;
                dec /= 2;
            }

            Array.Resize(ref binaryArray, index);
            Array.Reverse(binaryArray);

            return binaryArray;
        }

        /// <summary>
        /// 将一个二进制码数组转换成对应的十进制数
        /// </summary>
        /// <param name="binaryArray">需要转换的二进制码数组</param>
        /// <returns>对应的十进制数</returns>
        public static int BinaryArrayToDec(int[] binaryArray)
        {
            if (binaryArray == null)
            {
                throw new ArgumentNullException("binaryArray不能为null。");
            }

            int dec = 0;
            int power = 1;

            for (int i = binaryArray.Length - 1; i >= 0; i--)
            {
                dec += binaryArray[i] * power;
                power *= 2;
            }

            return dec;
        }

        /// <summary>
        /// 将一个十进制数转换成对应的BCD码数组
        /// </summary>
        /// <param name="dec">需要转换的十进制数</param>
        /// <returns>BCD码数组</returns>
        public static int[] DecToBCDArray(int dec)
        {
            if (dec < 0)
            {
                throw new ArgumentException("dec必须是非负整数。");
            }

            if (dec == 0)
            {
                return new int[] { 0 };
            }

            int[] bcdArray = new int[10];
            int index = 0;

            while (dec > 0)
            {
                int remainder = dec % 10;
                int[] binaryArray = DecToBinaryArray(remainder);
                int paddingCount = 4 - binaryArray.Length;

                for (int i = 0; i < paddingCount; i++)
                {
                    bcdArray[index++] = 0;
                }

                for (int i = 0; i < binaryArray.Length; i++)
                {
                    bcdArray[index++] = binaryArray[i];
                }

                dec /= 10;
            }

            Array.Resize(ref bcdArray, index);
            Array.Reverse(bcdArray);

            return bcdArray;
        }

        /// <summary>
        /// 将一个BCD码数组转换成对应的十进制数
        /// </summary>
        /// <param name="bcdArray">需要转换的BCD码数组</param>
        /// <returns>对应的十进制数</returns>
        public static int BCDArrayToDec(int[] bcdArray)
        {
            if (bcdArray == null)
            {
                throw new ArgumentNullException("bcdArray不能为null。");
            }

            int dec = 0;
            int power = 1;

            for (int i = bcdArray.Length - 1; i >= 0; i -= 4)
            {
                int binary = 0;

                for (int j = 0; j < 4; j++)
                {
                    int index = i - j;

                    if (index < 0)
                    {
                        break;
                    }

                    binary += bcdArray[index] * (int)Math.Pow(2, 3 - j);
                }

                dec += binary * power;
                power *= 10;
            }

            return dec;
        }

        /// <summary>
        /// 将给定的十进制数转换为 BCD 码字符串。
        /// </summary>
        /// <param name="dec">要转换的十进制数</param>
        /// <returns>转换后的 BCD 码字符串</returns>
        public static string Encode(int dec)
        {
            if (dec == 0)
            {
                return "0";
            }

            string str = dec.ToString();
            int len = str.Length;
            char[] bcdChars = new char[len * 2];
            for (int i = 0; i < len; i++)
            {
                int bcd = ((int)Char.GetNumericValue(str[i])) & 0x0F;
                bcdChars[i * 2] = (char)(bcd + ((bcd > 9) ? 0x37 : 0x30));

                bcd = (((int)Char.GetNumericValue(str[i])) >> 4) & 0x0F;
                bcdChars[i * 2 + 1] = (char)(bcd + ((bcd > 9) ? 0x37 : 0x30));
            }
            return new string(bcdChars);
        }

        /// <summary>
        /// 将给定的 BCD 码字符串转换为十进制数。
        /// </summary>
        /// <param name="bcd">要转换的 BCD 码字符串</param>
        /// <returns>转换后的十进制数</returns>
        public static int Decode(string bcd)
        {
            if (string.IsNullOrEmpty(bcd))
            {
                return 0;
            }

            int len = bcd.Length;
            int dec = 0;
            for (int i = 0; i < len; i += 2)
            {
                int a = ((int)bcd[i]) & 0x0F;
                int b = ((int)bcd[i + 1]) & 0x0F;
                dec = dec * 100 + a + b * 10;
            }
            return dec;
        }
    }
}

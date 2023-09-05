using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Morse 电码工具类
    /// </summary>
    public static class MorseUtil
    {
        // Morse 电码表
        private static readonly Dictionary<char, string> MORSE_TABLE = new Dictionary<char, string>()
        {
            {'A', ".-"},
            {'B', "-..."},
            {'C', "-.-."},
            {'D', "-.."},
            {'E', "."},
            {'F', "..-."},
            {'G', "--."},
            {'H', "...."},
            {'I', ".."},
            {'J', ".---"},
            {'K', "-.-"},
            {'L', ".-.."},
            {'M', "--"},
            {'N', "-."},
            {'O', "---"},
            {'P', ".--."},
            {'Q', "--.-"},
            {'R', ".-."},
            {'S', "..."},
            {'T', "-"},
            {'U', "..-"},
            {'V', "...-"},
            {'W', ".--"},
            {'X', "-..-"},
            {'Y', "-.--"},
            {'Z', "--.."},
            {'0', "-----"},
            {'1', ".----"},
            {'2', "..---"},
            {'3', "...--"},
            {'4', "....-"},
            {'5', "....."},
            {'6', "-...."},
            {'7', "--..."},
            {'8', "---.."},
            {'9', "----."},
            {' ', " "}
        };

        /// <summary>
        /// 将给定的字符串转换为 Morse 电码字符串。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的 Morse 电码字符串</returns>
        public static string Encode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            List<string> morseCodes = new List<string>();
            foreach (char c in str.ToUpper())
            {
                if (MORSE_TABLE.ContainsKey(c))
                {
                    morseCodes.Add(MORSE_TABLE[c]);
                }
            }
            return string.Join(" ", morseCodes);
        }


        /// <summary>
        /// 将给定的 Morse 电码字符串转换为原始字符串。
        /// </summary>
        /// <param name="morseCode">要转换的 Morse 电码字符串</param>
        /// <returns>转换后的原始字符串</returns>
        public static string Decode(string morseCode)
        {
            if (string.IsNullOrEmpty(morseCode))
            {
                return string.Empty;
            }

            string[] codes = morseCode.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<char> chars = new List<char>();
            foreach (string code in codes)
            {
                foreach (KeyValuePair<char, string> kvp in MORSE_TABLE)
                {
                    if (kvp.Value == code)
                    {
                        chars.Add(kvp.Key);
                        break;
                    }
                }
            }
            return new string(chars.ToArray());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// Punycode 工具类
    /// </summary>
    public static class PunycodeUtil
    {
        private const int BASE = 36;
        private const int TMIN = 1;
        private const int TMAX = 26;
        private const int SKEW = 38;
        private const int DAMP = 700;
        private const int INITIAL_BIAS = 72;
        private const int INITIAL_N = 128;

        private static readonly char[] DELIMITER = { '-' };

        /// <summary>
        /// 将给定的 Unicode 字符串按照 Punycode 编码规则进行编码。
        /// </summary>
        /// <param name="input">要编码的 Unicode 字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string Encode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            List<int> inputChars = input.Select(c => (int)c).ToList();
            List<int> basicChars = inputChars.Where(c => c < 0x80).ToList();
            List<int> extendedChars = inputChars.Except(basicChars).ToList();

            List<int> output = new List<int>();
            int n = INITIAL_N;
            int delta = 0;
            int bias = INITIAL_BIAS;

            // Encode the basic code points
            foreach (int b in basicChars)
            {
                output.Add(b);
            }

            int h = output.Count;
            int bLength = basicChars.Count;
            if (bLength > 0 && extendedChars.Count > 0)
            {
                output.Add('-');
            }

            // Main encoding loop
            while (h < inputChars.Count)
            {
                int m = int.MaxValue;
                foreach (int e in extendedChars)
                {
                    if (e >= n && e < m)
                    {
                        m = e;
                    }
                }

                delta += (m - n) * (h + 1);
                n = m;
                foreach (int e in extendedChars)
                {
                    if (e < n)
                    {
                        delta++;
                    }

                    if (e == n)
                    {
                        int q = delta;
                        int k = BASE;
                        while (true)
                        {
                            int t = k <= bias ? TMIN : (k >= bias + TMAX ? TMAX : k - bias);
                            if (q < t)
                            {
                                break;
                            }

                            output.Add(GetCodePoint(t + (q - t) % (BASE - t)));
                            q = (q - t) / (BASE - t);
                            k += BASE;
                        }

                        output.Add(GetCodePoint(q));
                        bias = Adapt(delta, h + 1, h == bLength);
                        delta = 0;
                        h++;
                    }
                }

                delta++;
                n++;
            }

            return new string(output.Select(c => (char)c).ToArray());
        }

        /// <summary>
        /// 将给定的 Punycode 编码字符串进行解码，得到原始的 Unicode 字符串。
        /// </summary>
        /// <param name="input">要解码的 Punycode 编码字符串</param>
        /// <returns>原始的 Unicode 字符串</returns>
        public static string Decode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            List<int> output = new List<int>();
            List<int> inputChars = input.Select(c => (int)c).ToList();
            List<int> basicChars = inputChars.Where(c => c < 0x80).ToList();
            int i = 0;
            int n = INITIAL_N;
            int bias = INITIAL_BIAS;

            // Find the last delimiter
            int lastDelim = input.LastIndexOf('-');
            if (lastDelim < 0)
            {
                lastDelim = 0;
            }

            // Decode the basic code points
            for (int j = 0; j < lastDelim; j++)
            {
                int c = inputChars[j];
                if (!IsBasic(c))
                {
                    throw new ArgumentException("Invalid input string.");
                }

                output.Add(c);
            }

            // Main decoding loop
            int p = lastDelim > 0 ? lastDelim + 1 : 0;
            while (p < inputChars.Count)
            {
                int oldi = i;
                int w = 1;
                int k = BASE;
                while (true)
                {
                    if (p >= inputChars.Count)
                    {
                        throw new ArgumentException("Invalid input string.");
                    }

                    int c = inputChars[p++];
                    int digit = GetDigit(c);
                    if (digit >= BASE)
                    {
                        throw new ArgumentException("Invalid input string.");
                    }

                    if (digit > (int.MaxValue - i) / w)
                    {
                        throw new ArgumentException("Invalid input string.");
                    }

                    i += digit * w;
                    int t = k <= bias ? TMIN : (k >= bias + TMAX ? TMAX : k - bias);
                    if (digit < t)
                    {
                        break;
                    }

                    if (w > int.MaxValue / (BASE - t))
                    {
                        throw new ArgumentException("Invalid input string.");
                    }

                    w *= BASE - t;
                    k += BASE;
                }

                int delta = i - oldi;
                output.Add(GetCodePoint(delta));
                bias = Adapt(delta, output.Count, oldi == 0);
                n += i / output.Count;
                i %= output.Count;
            }

            return new string(output.Select(c => (char)c).ToArray());
        }

        private static bool IsBasic(int codePoint)
        {
            return codePoint < 0x80;
        }

        private static int GetDigit(int codePoint)
        {
            if (codePoint - '0' < 10)
            {
                return codePoint - '0' + 26;
            }

            if (codePoint - 'a' < 26)
            {
                return codePoint - 'a';
            }

            if (codePoint - 'A' < 26)
            {
                return codePoint - 'A';
            }

            throw new ArgumentException("Invalid input string.");
        }

        private static int Adapt(int delta, int numPoints, bool firstTime)
        {
            delta = firstTime ? delta / DAMP : delta >> 1;
            delta += delta / numPoints;

            int k = 0;
            while (delta > ((BASE - TMIN) * TMAX) / 2)
            {
                delta /= BASE - TMIN;
                k += BASE;
            }

            return k + (((BASE - TMIN + 1) * delta) / (delta + SKEW));
        }

        private static int GetCodePoint(int digit)
        {
            if (digit < 26)
            {
                return digit + 'a';
            }

            if (digit < 36)
            {
                return digit - 26 + '0';
            }

            throw new ArgumentException("Invalid input string.");
        }
    }
}

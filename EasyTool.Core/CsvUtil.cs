using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 提供读取和写入 CSV 文件的常用方法。
    /// </summary>
    public static class CsvUtil
    {
        private const char DEFAULT_DELIMITER = ',';
        private const char DEFAULT_QUOTE = '"';

        /// <summary>
        /// 从指定路径的 CSV 文件中读取数据。
        /// </summary>
        /// <param name="path">CSV 文件路径。</param>
        /// <param name="delimiter">CSV 文件中的分隔符。</param>
        /// <param name="quote">CSV 文件中的引用符。</param>
        /// <returns>读取到的数据。</returns>
        public static List<List<string>> Read(string path, char delimiter = DEFAULT_DELIMITER, char quote = DEFAULT_QUOTE)
        {
            List<List<string>> data = new List<List<string>>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    data.Add(ParseLine(line, delimiter, quote));
                }
            }

            return data;
        }

        /// <summary>
        /// 将指定的数据写入到 CSV 文件中。
        /// </summary>
        /// <param name="data">要写入的数据。</param>
        /// <param name="path">CSV 文件路径。</param>
        /// <param name="delimiter">CSV 文件中的分隔符。</param>
        /// <param name="quote">CSV 文件中的引用符。</param>
        public static void Write(List<List<string>> data, string path, char delimiter = DEFAULT_DELIMITER, char quote = DEFAULT_QUOTE)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var record in data)
                {
                    string line = string.Join(delimiter.ToString(), record.Select(s => Escape(s, delimiter, quote)));
                    writer.WriteLine(line);
                }
            }
        }

        private static List<string> ParseLine(string line, char delimiter, char quote)
        {
            List<string> fields = new List<string>();
            int i = 0;
            while (i < line.Length)
            {
                if (line[i] == quote)
                {
                    int j = i + 1;
                    while (j < line.Length)
                    {
                        if (line[j] == quote)
                        {
                            if (j + 1 < line.Length && line[j + 1] == delimiter)
                            {
                                j++; // skip escaped delimiter
                            }
                            else
                            {
                                break; // end of quoted field
                            }
                        }
                        j++;
                    }

                    if (j >= line.Length || line[j] != quote)
                    {
                        throw new ArgumentException("Invalid CSV format: mismatched quotes.");
                    }

                    fields.Add(Unescape(line.Substring(i + 1, j - i - 1), delimiter, quote));
                    i = j + 1;
                }
                else
                {
                    int j = line.IndexOf(delimiter, i);
                    if (j < 0)
                    {
                        fields.Add(line.Substring(i));
                        i = line.Length;
                    }
                    else
                    {
                        fields.Add(line.Substring(i, j - i));
                        i = j + 1;
                    }
                }
            }

            return fields;
        }

        private static string Escape(string value, char delimiter, char quote)
        {
            if (value.Contains(delimiter) || value.Contains(quote) || value.Contains(Environment.NewLine))
            {
                return quote + value.Replace(quote.ToString(), quote.ToString() + quote.ToString()) + quote;
            }
            else
            {
                return value;
            }
        }

        private static string Unescape(string value, char delimiter, char quote)
        {
            return value.Replace(quote.ToString() + quote.ToString(), quote.ToString()).Replace(quote.ToString() + delimiter, delimiter.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// hash算法工具类
    /// </summary>
    public class HashUtil
    {
        /// <summary>
        /// 加法hash
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint AdditiveHash(string str)
        {
            uint hash = 0;
            foreach (char c in str)
            {
                hash += c;
            }
            return hash;
        }

        /// <summary>
        /// 旋转hash
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint RotatingHash(string str)
        {
            uint hash = (uint)str.Length;
            foreach (char c in str)
            {
                hash = (hash << 4) ^ (hash >> 28) ^ c;
            }
            return hash;
        }

        /// <summary>
        /// 一次一个hash
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint OneByOneHash(string str)
        {
            uint hash = 0;
            foreach (char c in str)
            {
                hash += c;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        /// <summary>
        /// Bernstein's hash
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint Bernstein(string str)
        {
            uint hash = 5381;
            foreach (char c in str)
            {
                hash = 33 * hash + c;
            }
            return hash;
        }

        /// <summary>
        /// Universal Hashing
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <param name="prime">大质数</param>
        /// <param name="num_buckets">哈希桶的数量</param>
        /// <param name="a">a的取值范围为[1, prime - 1]</param>
        /// <param name="b">b的取值范围
        public static uint Universal(string str, uint prime, uint num_buckets, uint a, uint b)
        {
            uint hash = a;
            foreach (char c in str)
            {
                hash = hash * prime + c;
            }
            hash = (hash * a + b) % num_buckets;
            return hash;
        }

        /// <summary>
        /// Zobrist Hashing
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <param name="table">随机数表</param>
        /// <returns>返回hash值</returns>
        public static uint Zobrist(string str, uint[] table)
        {
            uint hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash ^= table[str[i]];
            }
            return hash;
        }

        /// <summary>
        /// 改进的32位FNV算法1
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint FnvHash(string str)
        {
            const uint fnv_prime = 0x811C9DC5;
            uint hash = 0;
            foreach (char c in str)
            {
                hash *= fnv_prime;
                hash ^= c;
            }
            return hash;
        }

        /// <summary>
        /// Thomas Wang的算法，整数hash
        /// </summary>
        /// <param name="key">要进行hash的整数</param>
        /// <returns>返回hash值</returns>
        public static uint IntHash(uint key)
        {
            key = ~key + (key << 15);
            key = key ^ (key >> 12);
            key = key + (key << 2);
            key = key ^ (key >> 4);
            key = key * 2057;
            key = key ^ (key >> 16);
            return key;
        }

        /// <summary>
        /// RS算法hash
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <param name="b">b的取值范围为[1, 255]</param>
        /// <param name="a">a的取值范围为[1, b-1]</param>
        /// <returns>返回hash值</returns>
        public static uint RsHash(string str, uint b, uint a)
        {
            uint hash = 0;
            foreach (char c in str)
            {
                hash = hash * a + c;
                a = a * b;
            }
            return hash;
        }

        /// <summary>
        /// JS算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint JsHash(string str)
        {
            uint hash = 1315423911;
            foreach (char c in str)
            {
                hash ^= ((hash << 5) + c + (hash >> 2));
            }
            return hash;
        }

        /// <summary>
        /// PJW算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint PjwHash(string str)
        {
            uint hash = 0;
            const uint BitsInUnsignedInt = (uint)(sizeof(uint) * 8);
            const uint ThreeQuarters = (uint)((BitsInUnsignedInt * 3) / 4);
            const int OneEighth = (int)(BitsInUnsignedInt / 8);
            const uint HighBits = (uint)(0xFFFFFFFF) << (int)(BitsInUnsignedInt - OneEighth);
            foreach (char c in str)
            {
                hash = (hash << OneEighth) + c;
                uint test = hash & HighBits;
                if (test != 0)
                {
                    hash = ((hash ^ (test >> (int)ThreeQuarters)) & (~HighBits));
                }
            }
            return hash;
        }

        /// <summary>
        /// ELF算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint ElfHash(string str)
        {
            uint hash = 0;
            uint x = 0;
            foreach (char c in str)
            {
                hash = (hash << 4) + c;
                x = hash & 0xF0000000;
                if (x != 0)
                {
                    hash ^= (x >> 24);
                }
                hash &= ~x;
            }
            return hash;
        }

        /// <summary>
        /// BKDR算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <param name="seed">种子值</param>
        /// <returns>返回hash值</returns>
        public static uint BkdrHash(string str, uint seed)
        {
            uint hash = 0;
            foreach (char c in str)
            {
                hash = hash * seed + c;
            }
            return hash;
        }

        /// <summary>
        /// SDBM算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint SdbmHash(string str)
        {
            uint hash = 0;
            foreach (char c in str)
            {
                hash = c + (hash << 6) + (hash << 16) - hash;
            }
            return hash;
        }

        /// <summary>
        /// DJB算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint DjbHash(string str)
        {
            uint hash = 5381;
            foreach (char c in str)
            {
                hash = ((hash << 5) + hash) + c;
            }
            return hash;
        }

        /// <summary>
        /// DEK算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint DekHash(string str)
        {
            uint hash = (uint)str.Length;
            foreach (char c in str)
            {
                hash = ((hash << 5) ^ (hash >> 27)) ^ c;
            }
            return hash;
        }

        /// <summary>
        /// AP算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint ApHash(string str)
        {
            uint hash = 0;
            int i;
            for (i = 0; i < str.Length; i++)
            {
                if ((i & 1) == 0)
                {
                    hash ^= ((hash << 7) ^ str[i] ^ (hash >> 3));
                }
                else
                {
                    hash ^= (~((hash << 11) ^ str[i] ^ (hash >> 5)));
                }
            }
            return hash;
        }

        /// <summary>
        /// TianL Hash算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <param name="len">hash表的长度</param>
        /// <returns>返回hash值</returns>
        public static uint TianlHash(string str, uint len)
        {
            uint hash = 0;
            uint[] w = new uint[64];
            uint[] v = new uint[8];

            if (str.Length == 0) return 0;

            if (str.Length <= 64)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    hash += (uint)str[i];
                    hash += (hash << 10);
                    hash ^= (hash >> 6);
                }
                hash += (hash << 3);
                hash ^= (hash >> 11);
                hash += (hash << 15);
                return hash % len;
            }

            for (int i = 0; i < str.Length / 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    w[j] = (uint)str[i * 64 + j];
                }

                for (int j = 0; j < 8; j++)
                {
                    v[j] = 0;
                }

                for (int j = 0; j < 64; j++)
                {
                    uint u = w[j];
                    for (int k2 = 0; k2 < 8; k2++)
                    {
                        v[k2] ^= u & 0xff;
                        u >>= 8;
                    }
                }

                for (int j = 0; j < 8; j++)
                {
                    hash ^= v[j];
                    hash += (hash << 10);
                    hash ^= (hash >> 6);
                }
            }

            if (str.Length % 64 != 0)
            {
                for (int i = str.Length - str.Length % 64; i < str.Length; i++)
                {
                    hash += (uint)str[i];
                    hash += (hash << 10);
                    hash ^= (hash >> 6);
                }
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return hash % len;
        }

        /// <summary>
        /// JAVA自己带的算法
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static uint JavaDefaultHash(string str)
        {
            uint hash = 0;
            uint h = hash;
            foreach (char c in str)
            {
                h = 31 * h + c;
            }
            hash = h;
            return hash;
        }

        /// <summary>
        /// 混合hash算法，输出64位的值
        /// </summary>
        /// <param name="str">要进行hash的字符串</param>
        /// <returns>返回hash值</returns>
        public static ulong MixHash(string str)
        {
            uint seed = 131; // 31 131 1313 13131 131313 etc..
            ulong hash1 = 0;
            ulong hash2 = 0;
            foreach (char c in str)
            {
                hash1 = (hash1 * seed) + c;
                hash2 = (hash2 * seed) + c + 1;
            }
            return hash1 + (hash2 * 1566083941);
        }

    }
    }

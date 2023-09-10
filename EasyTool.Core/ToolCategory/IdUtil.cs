using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace EasyTool
{
    /// <summary>
    /// uuid生成风格
    /// </summary>
    public enum UUIDStyle
    {
        /// <summary>
        /// guid
        /// </summary>
        GUID,
        /// <summary>
        /// mssql顺序guid
        /// </summary>
        SequentialGUID,
        /// <summary>
        /// 顺序
        /// </summary>
        Sequence,
    }
    /// <summary>
    /// 唯一ID工具
    /// </summary>
    public class IdUtil
    {
        private static readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static int objectIdCounter = 0;

        private static long _counter = DateTime.Now.Ticks;
        /// <summary>
        /// 生成UUID
        /// </summary>
        /// <returns>生成的UUID</returns>
        public static Guid UUID(UUIDStyle style = UUIDStyle.GUID)
        {
            switch (style)
            {
                case UUIDStyle.GUID: return Guid.NewGuid();
                case UUIDStyle.SequentialGUID:
                    {
                        Span<byte> guidBytes = stackalloc byte[16];
                        var succeeded = Guid.NewGuid().TryWriteBytes(guidBytes);
                        var incrementedCounter = Interlocked.Increment(ref _counter);
                        Span<byte> counterBytes = stackalloc byte[sizeof(long)];
                        MemoryMarshal.Write(counterBytes, ref incrementedCounter);

                        if (!BitConverter.IsLittleEndian)
                        {
                            counterBytes.Reverse();
                        }

                        guidBytes[08] = counterBytes[1];
                        guidBytes[09] = counterBytes[0];
                        guidBytes[10] = counterBytes[7];
                        guidBytes[11] = counterBytes[6];
                        guidBytes[12] = counterBytes[5];
                        guidBytes[13] = counterBytes[4];
                        guidBytes[14] = counterBytes[3];
                        guidBytes[15] = counterBytes[2];

                        return new Guid(guidBytes);
                    }
                case UUIDStyle.Sequence:
                    {
                        Span<byte> guidBytes = stackalloc byte[16];
                        var succeeded = Guid.NewGuid().TryWriteBytes(guidBytes);
                        var incrementedCounter = Interlocked.Increment(ref _counter);
                        Span<byte> counterBytes = stackalloc byte[sizeof(long)];
                        MemoryMarshal.Write(counterBytes, ref incrementedCounter);

                        if (!BitConverter.IsLittleEndian)
                        {
                            counterBytes.Reverse();
                        }

                        guidBytes[0] = counterBytes[4];
                        guidBytes[1] = counterBytes[5];
                        guidBytes[2] = counterBytes[6];
                        guidBytes[3] = counterBytes[7];
                        guidBytes[4] = counterBytes[2];
                        guidBytes[5] = counterBytes[3];
                        guidBytes[6] = counterBytes[0];
                        guidBytes[7] = counterBytes[1];

                        return new Guid(guidBytes);
                    }
                default:
                    throw new ArgumentException("不支持的UUIDStyle");
            }
        }
        /// <summary>
        /// 生成UUID
        /// </summary>
        /// <returns>生成的UUID</returns>
        public static string UUIDString(UUIDStyle style = UUIDStyle.GUID)
            => UUID(style).ToString();

        /// <summary>
        /// 生成MongoDB ObjectId
        /// </summary>
        /// <returns>生成的MongoDB ObjectId</returns>
        public static string ObjectId()
        {
            byte[] timestamp = BitConverter.GetBytes((int)(DateTime.UtcNow - epoch).TotalSeconds);
            byte[] machineIdentifier = BitConverter.GetBytes(Environment.MachineName.GetHashCode());
            byte[] processIdentifier = BitConverter.GetBytes(System.Diagnostics.Process.GetCurrentProcess().Id);
            byte[] increment = BitConverter.GetBytes(Interlocked.Increment(ref objectIdCounter));

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestamp);
                Array.Reverse(machineIdentifier);
                Array.Reverse(processIdentifier);
                Array.Reverse(increment);
            }

            byte[] objectId = new byte[12];
            Buffer.BlockCopy(timestamp, 0, objectId, 0, 4);
            Buffer.BlockCopy(machineIdentifier, 2, objectId, 4, 2);
            Buffer.BlockCopy(processIdentifier, 0, objectId, 6, 2);
            Buffer.BlockCopy(increment, 1, objectId, 8, 3);

            return Convert.ToBase64String(objectId);
        }

        private static readonly long epochTicks = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        private static readonly long workerIdBits = 5L;
        private static readonly long datacenterIdBits = 5L;
        private static readonly long sequenceBits = 12L;
        private static readonly long maxWorkerId = -1L ^ (-1L << (int)workerIdBits);
        private static readonly long maxDatacenterId = -1L ^ (-1L << (int)datacenterIdBits);
        private static readonly long sequenceMask = -1L ^ (-1L << (int)sequenceBits);

        private static long lastTimestamp = -1L;
        private static long sequence = 0L;
        private static readonly object lockObj = new object();

        private static readonly Random random = new Random();

        private static readonly long workerId = random.Next((int)maxWorkerId);
        private static readonly long datacenterId = random.Next((int)maxDatacenterId);

        /// <summary>
        /// 生成Snowflake ID
        /// </summary>
        /// <returns>生成的Snowflake ID</returns>
        public static long SnowflakeId()
        {
            lock (lockObj)
            {
                long timestamp = DateTime.UtcNow.Ticks - epochTicks;

                if (timestamp < lastTimestamp)
                {
                    throw new Exception("Clock moved backwards, refusing to generate Snowflake ID");
                }

                if (timestamp == lastTimestamp)
                {
                    sequence = (sequence + 1) & sequenceMask;

                    if (sequence == 0)
                    {
                        timestamp = NextMillis(lastTimestamp);
                    }
                }
                else
                {
                    sequence = 0L;
                }

                lastTimestamp = timestamp;

                return (timestamp << (int)(workerIdBits + sequenceBits)) |
                       (datacenterId << (int)sequenceBits) |
                       (workerId << (int)sequenceBits) |
                       sequence;
            }
        }

        private static long NextMillis(long lastTimestamp)
        {
            long timestamp = DateTime.UtcNow.Ticks - epochTicks;

            while (timestamp <= lastTimestamp)
            {
                timestamp = DateTime.UtcNow.Ticks - epochTicks;
            }

            return timestamp;
        }
    }
}

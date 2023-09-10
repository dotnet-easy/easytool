using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyTool
{
    public class GuidUtil
    {
        public static Guid NewGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 获取一个顺序的GUID值
        /// </summary>
        public static Guid NextGuid()
        {
            long _counter = DateTime.UtcNow.Ticks;
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

        /// <summary>
        /// 获取一个顺序的GUID值
        /// </summary>
        public static string NewUUID()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 获取一个顺序的GUID值
        /// </summary>
        public static string NextUUID()
        {
            return NextGuid().ToString("N");
        }
    }
}

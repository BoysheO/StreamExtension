using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;

namespace BoysheO.System.IO.Extensions
{
    public static class StreamExtensions
    {
        public static async IAsyncEnumerable<ArraySegment<byte>> AsAsyncEnumerable(this Stream stream, int buffsize = 1024)
        {
            while (true)
            {

                var buff = ArrayPool<byte>.Shared.Rent(buffsize);
                var count = await stream.ReadAsync(buff, 0, buffsize);
                if (count <= 0)
                {
                    ArrayPool<byte>.Shared.Return(buff);
                    yield break;
                }
                yield return new ArraySegment<byte>(buff, 0, count);
            }
        }
    }
}

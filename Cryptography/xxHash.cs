using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Cryptography
{
    public static class xxHash
    {

        const uint PRIME32_1 = 2654435761U;
        const uint PRIME32_2 = 2246822519U;
        const uint PRIME32_3 = 3266489917U;
        const uint PRIME32_4 = 668265263U;
        const uint PRIME32_5 = 374761393U;

        public static uint CalculateHash(byte[] buf, int len = -1, uint seed = 0)
        {
            uint h32;
            int index = 0;
            if (len == -1)
            {
                len = buf.Length;
            }

            if (len >= 16)
            {
                int limit = len - 16;
                uint v1 = seed + PRIME32_1 + PRIME32_2;
                uint v2 = seed + PRIME32_2;
                uint v3 = seed + 0;
                uint v4 = seed - PRIME32_1;

                do
                {
                    v1 = CalcSubHash(v1, buf, index);
                    index += 4;
                    v2 = CalcSubHash(v2, buf, index);
                    index += 4;
                    v3 = CalcSubHash(v3, buf, index);
                    index += 4;
                    v4 = CalcSubHash(v4, buf, index);
                    index += 4;
                } while (index <= limit);

                h32 = RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);
            }
            else
            {
                h32 = seed + PRIME32_5;
            }

            h32 += (uint)len;

            while (index <= len - 4)
            {
                h32 += BitConverter.ToUInt32(buf, index) * PRIME32_3;
                h32 = RotateLeft(h32, 17) * PRIME32_4;
                index += 4;
            }

            while (index < len)
            {
                h32 += buf[index] * PRIME32_5;
                h32 = RotateLeft(h32, 11) * PRIME32_1;
                index++;
            }

            h32 ^= h32 >> 15;
            h32 *= PRIME32_2;
            h32 ^= h32 >> 13;
            h32 *= PRIME32_3;
            h32 ^= h32 >> 16;

            return h32;
        }

        public static uint CalculateHash(Stream stream, long len = -1, uint seed = 0)
        {
            uint h32;
            var index = 0;

            if (!stream.CanRead || !stream.CanSeek)
                throw new InvalidOperationException("Stream has to be seekable and readable");

            if (len == -1)
            {
                len = stream.Length;
            }

            var streamPosition = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[16];
            if (len >= 16)
            {
                var limit = len - 16;
                var v1 = seed + PRIME32_1 + PRIME32_2;
                var v2 = seed + PRIME32_2;
                var v3 = seed + 0;
                var v4 = seed - PRIME32_1;

                do
                {
                    var loopIndex = 0;
                    stream.Read(buffer, 0, buffer.Length);

                    v1 = CalcSubHash(v1, buffer, loopIndex);
                    loopIndex += 4;
                    v2 = CalcSubHash(v2, buffer, loopIndex);
                    loopIndex += 4;
                    v3 = CalcSubHash(v3, buffer, loopIndex);
                    loopIndex += 4;
                    v4 = CalcSubHash(v4, buffer, loopIndex);
                    loopIndex += 4;

                    index += loopIndex;
                } while (index <= limit);

                h32 = RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);
            }
            else
            {
                h32 = seed + PRIME32_5;
            }

            h32 += (uint)len;

            buffer = new byte[4];
            while (index <= len - 4)
            {
                stream.Read(buffer, 0, buffer.Length);
                h32 += BitConverter.ToUInt32(buffer, 0) * PRIME32_3;
                h32 = RotateLeft(h32, 17) * PRIME32_4;
                index += 4;
            }

            buffer = new byte[1];
            while (index < len)
            {
                stream.Read(buffer, 0, buffer.Length);
                h32 += buffer[0] * PRIME32_5;
                h32 = RotateLeft(h32, 11) * PRIME32_1;
                index++;
            }

            stream.Seek(streamPosition, SeekOrigin.Begin);

            h32 ^= h32 >> 15;
            h32 *= PRIME32_2;
            h32 ^= h32 >> 13;
            h32 *= PRIME32_3;
            h32 ^= h32 >> 16;

            return h32;
        }
        
        private static uint CalcSubHash(uint value, byte[] buf, int index)
        {
            uint read_value = BitConverter.ToUInt32(buf, index);
            value += read_value * PRIME32_2;
            value = RotateLeft(value, 13);
            value *= PRIME32_1;
            return value;
        }

        private static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

    }
}

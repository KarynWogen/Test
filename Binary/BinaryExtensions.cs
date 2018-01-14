using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class BinaryExtensions
    {
        public static ulong ToUInt64(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 8)
            {
#if PERMISSIVE
                return BitConverter.ToUInt64(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    len > 3 ? value[startIndex+3] : (byte)0,
                    len > 4 ? value[startIndex+4] : (byte)0,
                    len > 5 ? value[startIndex+5] : (byte)0,
                    len > 6 ? value[startIndex+6] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a UInt64(8bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToUInt64(value, startIndex);
        }

        public static long ToInt64(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 8)
            {
#if PERMISSIVE
                return BitConverter.ToInt64(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    len > 3 ? value[startIndex+3] : (byte)0,
                    len > 4 ? value[startIndex+4] : (byte)0,
                    len > 5 ? value[startIndex+5] : (byte)0,
                    len > 6 ? value[startIndex+6] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Int64(8bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToInt64(value, startIndex);
        }

        public static uint ToUInt32(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 4)
            {
#if PERMISSIVE
                return BitConverter.ToUInt32(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a UInt32(4bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToUInt32(value, startIndex);
        }

        public static int ToInt32(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 4)
            {
#if PERMISSIVE
                return BitConverter.ToInt32(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Int32(4bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToInt32(value, startIndex);
        }

        public static ushort ToUInt16(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 2)
            {
#if PERMISSIVE
                return BitConverter.ToUInt16(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a UInt16(2bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToUInt16(value, startIndex);
        }

        public static short ToInt16(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 2)
            {
#if PERMISSIVE
                return BitConverter.ToInt16(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Int16(2bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToInt16(value, startIndex);
        }

        public static double ToDouble(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 8)
            {
#if PERMISSIVE
                return BitConverter.ToDouble(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    len > 3 ? value[startIndex+3] : (byte)0,
                    len > 4 ? value[startIndex+4] : (byte)0,
                    len > 5 ? value[startIndex+5] : (byte)0,
                    len > 6 ? value[startIndex+6] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Double(8bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToDouble(value, startIndex);
        }

        public static float ToFloat(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 4)
            {
#if PERMISSIVE
                return BitConverter.ToSingle(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    len > 1 ? value[startIndex+1] : (byte)0,
                    len > 2 ? value[startIndex+2] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Float(4bytes) from " + len + " bytes."));
#endif
            }
            return BitConverter.ToSingle(value, startIndex);
        }

        public static char ToChar(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 2)
            {
#if PERMISSIVE
                return BitConverter.ToChar(new byte[] {
                    len > 0 ? value[startIndex] : (byte)0,
                    0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a Char(2bytes) from " + len + " byte".Pluralize(len).Dot()));
#endif
            }
            return BitConverter.ToChar(value, startIndex);

        }

        public static bool ToBool(this byte[] value, int startIndex = 0)
        {
            var len = value.Length - startIndex;
            if (len < 1)
            {
#if PERMISSIVE
                return BitConverter.ToBoolean(new byte[] { 0 }, 0);
#else
                throw (new IndexOutOfRangeException("Trying to generate a UInt64(1byte) from " + len + " byte".Pluralize(len)));
#endif
            }
            return BitConverter.ToBoolean(value, startIndex);
        }

        public static byte[] ToBytes(this long value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this ulong value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this uint value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this short value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this ushort value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this double value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this float value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this char value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this bool value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}

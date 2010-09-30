using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AzDoc.Web.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadBytes(this Stream stream, int count)
        {
            return EnumBytes(stream, count).ToArray();
        }

        private static IEnumerable<byte> EnumBytes(Stream stream, int count)
        {
            for (int i = 0; i < count; i++)
            {
                int b = stream.ReadByte();
                if (b < 0) yield break;
                yield return (byte) b;
            }
        }
    }
}
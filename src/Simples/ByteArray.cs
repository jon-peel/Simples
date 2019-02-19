using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simples
{
    public static class ByteArray
    {
        public static string ToHexString(this IEnumerable<byte> @this)
        {
            var data = @this as byte[] ?? @this.ToArray();
            var builder = new StringBuilder(data.Length * 2);
            Array.ForEach(data, x => builder.Append($"{x:x2}"));
            return builder.ToString();
        }

        public static string ToBase64(this IEnumerable<byte> @this)
        {
            var data = @this as byte[] ?? @this.ToArray();
            return Convert.ToBase64String(data);
        }
    }
}

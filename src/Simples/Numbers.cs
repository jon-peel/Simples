using System;

namespace Simples
{
    public static class Numbers
    {
        public static decimal Abs(this decimal @this) => Math.Abs(@this);
        public static int Floor(this decimal @this) => (int)Math.Abs(@this);
    }
}

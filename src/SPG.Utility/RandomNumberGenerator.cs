using System;

namespace SPG.Utility
{
    public class RandomNumberGenerator
    {
        public static int NumberValue(int minValue, int maxValue)
        {
            Random r = new Random();
            return r.Next(minValue, maxValue);
        }
        public static byte[]  ByteArrayValue(int length)
        {
            byte[] array = new byte[length];
            Random r = new Random();
            r.NextBytes(array);
            return array;
        }
    }
}

using System;

namespace NetGears.Core.Extensions
{
    public static class ArrayExtension
    {
        public static T[] GetSubArray<T>(this T[] data, int index, int length)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}

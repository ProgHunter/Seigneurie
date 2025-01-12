using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class EnumUtils
    {
        public static IEnumerable<T> GetEnumValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
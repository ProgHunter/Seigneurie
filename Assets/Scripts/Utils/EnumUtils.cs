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

        public static IEnumerable<T> GetEnumValuesWithoutNullValue<T>()
        {
            var liste = GetEnumValues<T>().ToList();
            //Removes the null value in the enum
            liste.RemoveAt(0);
            return liste;
        }
    }
}
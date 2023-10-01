using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Id3Fixer.Extensions
{
    public static class StringArrayExtnsion
    {
        public static string? GetSafe(this string[] array, int index)
        {
            if (array.Length <= index)
            { 
                return null;
            }

            return array[index];
        }
    }
}

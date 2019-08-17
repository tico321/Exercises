using System;

namespace Exercises.Easy
{
    public static class ComparingTwoLongIntegersSolution
    {
        public static string Solve(string a, string b)
        {
            var length = a.Length > b.Length ? a.Length : b.Length;
            a = a.PadLeft(length, '0');
            b = b.PadLeft(length, '0');
            var compare = string.Compare(a, b, StringComparison.Ordinal);
            if (compare == 0) return "=";
            if (compare > 0) return ">";
            return "<";
        }

        public static string Solve2(string a, string b)
        {
            var aLeft0 = 0;
            for (var i = 0; i < a.Length; i++)
            {
                if (a[i] == '0') aLeft0++;
                else break;
            }

            var bLeft0 = 0;
            for (var i = 0; i < b.Length; i++)
            {
                if (b[i] == '0') bLeft0++;
                else break;
            }

            var aLength = a.Length - aLeft0;
            var bLength = b.Length - bLeft0;

            if (aLength > bLength) return ">";
            if (bLength > aLength) return "<";

            for (var i = 0; i < aLength; i++)
            {
                var currentA = a[aLeft0 + i];
                var currentB = b[bLeft0 + i];
                if (currentA > currentB) return ">";
                if (currentB > currentA) return "<";
            }

            return "=";
        }
    }
}

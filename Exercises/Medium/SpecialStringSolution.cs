using System.Linq;
using System.Text.RegularExpressions;

namespace Exercises.Medium
{
    public static class SpecialStringSolution
    {
        public static long Solve(int n, string s)
        {
            var count = s.Length; //count every char

            //count the ones with the middle one different
            var exceptMiddleOne = new Regex("(([a-z])\\2*)(?!\\1)(?=[a-z]\\1)"); //matches a_a and also aa_aa
            count += exceptMiddleOne.Matches(s).Select(m => m.Groups[0].Length).Sum();

            //count repeated chars
            var repeated = new Regex("([a-z])\\1+");
            count += repeated.Matches(s)
                .Select(m => m.Groups[0].Length - 1)
                .Select(l =>
                    (l * l + l) /
                    2) // triangular number given by (n^2+n)/2 // https://en.wikipedia.org/wiki/Triangular_number
                .Sum();

            return count;
        }
    }
}
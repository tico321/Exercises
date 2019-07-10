using System.Linq;

namespace Exercises.Easy
{
    public class RepeatedStringSolution
    {
        public long Solve(string s, long n)
        {
            if (n < 0) return 0;
            if (n < s.Length) return s.Substring(0, (int)n).Where(c => c == 'a').Select(c => 1).Sum();
            var asCount = (long)s.Where(c => c == 'a').Select(c => 1).Sum();
            if (n < s.Length) return asCount;
            var repeatedStringsInN = (n / s.Length);
            var excedentLetters = n % s.Length;
            var excedentAs = s.Substring(0, (int)excedentLetters).Where(c => c == 'a').Select(c => 1).Sum();
            return repeatedStringsInN * asCount + excedentAs;
        }
    }
}

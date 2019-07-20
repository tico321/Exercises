using System;
using System.Linq;

namespace Exercises.Medium
{
    public class CodeParsingSolution
    {
        public static string Solve(string s)
        {
            var xs = s.Where(i => i == 'x').Count();
            var ys = s.Where(i => i == 'y').Count();
            var count = Math.Abs(xs - ys);
            var c = xs > ys ? 'x' : 'y';
            return new string(c, count);
        }
    }
}

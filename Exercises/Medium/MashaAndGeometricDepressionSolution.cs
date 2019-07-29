using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{

    public class MashaAndGeometricDepressionSolution
    {
        public static string Solve(string input, string input2)
        {
            var args = input.Split(' ');
            var b = long.Parse(args[0]);
            var q = long.Parse(args[1]);
            var absMax = long.Parse(args[2]);
            var badCount = long.Parse(args[3]);
            var bad = new HashSet<long>(input2.Split(' ').Select(c => long.Parse(c)).ToList());

            var count = 0;
            var prevB = b;
            while (Math.Abs(b) <= absMax)
            {
                if (!bad.Contains(b)) count++;
                b *= q;
                if (Math.Abs(prevB) == Math.Abs(b))
                {
                    if (prevB != b)
                    {
                        return bad.Contains(b) && bad.Contains(prevB) ?
                            count.ToString() :
                            "inf";
                    }
                    return bad.Contains(b) ? count.ToString() : "inf";
                }
                prevB = b;
            }

            return count.ToString();
        }
    }
}

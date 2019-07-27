using System;

namespace Exercises.Medium
{
    public class RoutineProblemSolution
    {
        public static string Solve(string input)
        {
            var args = input.Split(' ');
            int a = int.Parse(args[0]),
                b = int.Parse(args[1]),
                c = int.Parse(args[2]),
                d = int.Parse(args[3]);

            long p, q;
            if (a * d - b * c > 0) //frame needs streching
            {
                p = (a * d - b * c);
                q = a * d;
            }
            else
            {
                p = (b * c - a * d);
                q = b * c;
            }

            var gcd = Math.Abs(Gcd(p, q));
            return $"{p / gcd}/{q / gcd}";
        }

        public static long Gcd(long a, long b)
        {
            if (b == 0) return a;
            return Gcd(b, a % b);
        }
    }
}

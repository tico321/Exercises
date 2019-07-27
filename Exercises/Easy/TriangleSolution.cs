using System.Collections.Generic;

namespace Exercises.Easy
{
    public class TriangleSolution
    {
        public static string Solve(string input)
        {
            var args = input.Split(' ');
            var s1 = int.Parse(args[0]);
            var s2 = int.Parse(args[1]);
            var s3 = int.Parse(args[2]);
            var s4 = int.Parse(args[3]);

            var sides = new int[] { s1, s2, s3, s4 };
            var segment = false;
            for (var i = 0; i < 4; i++)
            {
                var positions = new List<int> { 0, 1, 2, 3 };
                positions.Remove(i);
                var a = sides[positions[0]];
                var b = sides[positions[1]];
                var c = sides[positions[2]];

                if (IsTriangle(a, b, c))
                {
                    if (IsSegment(a, b, c)) segment = true;
                    else return "TRIANGLE";
                }
            }

            return segment ? "SEGMENT" : "IMPOSSIBLE";
        }

        public static bool IsTriangle(int a, int b, int c)
        {
            return a + b >= c &&
                a + c >= b &&
                b + c >= a;
        }

        public static bool IsSegment(int a, int b, int c)
        {
            return (a == b + c) || (b == a + c) || (c == a + b);
        }
    }
}

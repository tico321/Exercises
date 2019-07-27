using System.Linq;

namespace Exercises.Easy
{
    public class MikeGCDProblemSolution
    {
        public static (string, string) Solve(string input1, string input2)
        {
            var n = long.Parse(input1);
            var a = input2.Split(' ').Select(c => long.Parse(c)).ToArray();

            if (a.Sum() == 0) return ("NO", "0");

            var even = a.Select(e => e % 2 == 0).ToArray();
            if (even.Count(e => e == true) == n) return ("YES", "0");

            //check if all of them share commonDivisor
            var commonDivisor = Gcd(a[0], a[1]);
            var commonDivisorCount = 2;
            for (var i = 1; i < n - 1;)
            {
                var currentCommonDivisor = Gcd(a[i], a[i + 1]);
                if (currentCommonDivisor == 1) break;
                if (commonDivisor > currentCommonDivisor)
                {
                    var temp = commonDivisor;
                    commonDivisor = currentCommonDivisor;
                    currentCommonDivisor = temp;
                }
                if (currentCommonDivisor % commonDivisor != 0) break;
                commonDivisorCount++; i++;
            }
            if (commonDivisorCount == n && commonDivisor > 1) return ("YES", "0");

            // the alternative is to make them all pairs
            var moves = 0;
            for (var i = 0; i < n;)
            {
                if (even[i])
                {
                    i++;
                    continue;
                }
                else
                {
                    if (i + 1 == n) moves += 2; // previous one has to be even
                    else if (even[i + 1]) moves += 2; // takes two moves
                    else moves++; // takes one move
                    i += 2;
                }
            }

            return ("YES", moves.ToString());
        }

        private static long Gcd(long a, long b)
        {
            if (b == 0) return a;
            return Gcd(b, a % b);
        }
    }
}

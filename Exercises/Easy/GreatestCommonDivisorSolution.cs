using System.Linq;

namespace Exercises.Easy
{
    public class GreatestCommonDivisorSolution
    {
        public static int Solve(int[] numbers)
        {
            return numbers.Aggregate(Gdc);
        }

        private static int Gdc(int a, int b) => b == 0 ? a : Gdc(b, a % b);
    }
}
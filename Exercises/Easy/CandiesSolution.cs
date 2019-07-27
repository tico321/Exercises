namespace Exercises.Easy
{
    public class CandiesSolution
    {
        public static long Solve(string input)
        {
            var candies = long.Parse(input);
            var min = 1L;
            var top = candies;
            var result = top;
            while (min <= top)
            {
                var k = (min + top) / 2;
                var moreThanHalf = VasyaEatsMoreThanHalf(candies, k);
                if (moreThanHalf)
                {
                    result = k;
                    top = k - 1;
                }
                else
                {
                    min = k + 1;
                }
            }

            return result;
        }

        private static bool VasyaEatsMoreThanHalf(long candies, long k)
        {
            var totalCandies = candies;
            var vasya = 0L;
            while (candies > 0)
            {
                candies -= k;
                vasya += k;
                if (candies >= 10)
                {
                    var tenPercent = (long)(candies / 10);
                    candies -= tenPercent;
                }
                else
                {
                    vasya += candies;
                    candies = 0;
                }
            }

            return vasya * 2 >= totalCandies;
        }
    }
}
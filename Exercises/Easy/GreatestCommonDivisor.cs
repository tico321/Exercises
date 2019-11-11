using System.Linq;
using Xunit;

namespace Exercises.Easy
{
    public class GreatestCommonDivisor
    {
        [Theory]
        [InlineData(new int[]{ 2, 4, 6 }, 2)]
        [InlineData(new int[]{ 4, 8, 12, 24 }, 4)]
        [InlineData(new int[]{ 2, 3, 8, 9 }, 1)]
        [InlineData(new int[]{ 2, 4, 6, 9 }, 1)]
        [InlineData(new int[]{ 2, 4, 6, 8 }, 2)]
        public void Scenarios(int[] numbers, int expected)
        {
            var actual = GreatestCommonDivisorSolution.Solve(numbers);

            Assert.Equal(expected, actual);
        }
    }
}
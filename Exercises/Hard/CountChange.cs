using System.Collections.Generic;
using Xunit;

namespace Exercises.Hard
{
    /*
        You are given an amount of money and a list of coins. You must return the number of ways the amount can be reached.
        For example: given the amount 4 and the coins [1,2] you should return 3 as there are 3 possible ways to reach the amount:
            * 1,1,1
            * 1,1,2
            * 2,2
     */
    public class CountChange
    {
        [Theory]
        [InlineData(4, new int[] { 1, 2 }, 3)]
        [InlineData(8, new int[] { 1 }, 1)]
        [InlineData(8, new int[] { 1, 3 }, 3)]
        public void Scenarios(int money, IEnumerable<int> coins, int expected)
        {
            var actual = new CountChangeSolution().Solve(money, coins);

            Assert.Equal(expected, actual);
        }
    }
}

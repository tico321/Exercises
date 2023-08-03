using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Easy
{
    /// <summary>
    /// The problem “Count sub-arrays with equal number of 1’s and 0’s” states that you are given an array
    /// consisting of 0’s and 1’s only. The problem statement asks to find out the count of sub-arrays consisting
    /// equal number of 0’s ad 1’s.
    /// Ex:
    /// arr[] = {0, 0, 1, 1, 0}
    /// output = 4
    /// </summary>
    public sealed class CountSubArraysWithEqualNumberOf1And0S
    {
        [Theory]
        [InlineData(new int[]{ 0, 0, 1, 1, 0 }, 4)]
        [InlineData(new int[]{ 0, 0 }, 0)]
        [InlineData(new int[]{ 0, 1 }, 1)]
        public void Test(int[] input, int expected)
        {
            int actual = Solve(input);

            Assert.Equal(expected, actual);
        }

        private int Solve(int[] input)
        {
            List<int> list = new(input);
            return Enumerable.Range(2, list.Count - 1)
                .SelectMany(window => list.Window(window))
                .Select(w => w.ToList())
                .Aggregate(
                    0,
                    (acc, w) => w.Count(x => x == 0) == w.Count(x => x == 1) ? acc + 1 : acc);
        }
    }

    public static class ListExtensions
    {
        public static IEnumerable<IEnumerable<int>> Window(this List<int> list, int window)
        {
            for(int current = 0; current + window <= list.Count; current++)
            {
                yield return list.Skip(current).Take(window);
            }
        }
    }
}

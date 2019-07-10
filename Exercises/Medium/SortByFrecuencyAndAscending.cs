using System.Collections.Generic;
using Xunit;

namespace Exercises
{
    /// <summary>
    /// Given an input array, sort the content by frecuency and value.
    /// For example:
    ///     Given the input: [5, 3, 1, 2, 2, 4]
    ///     the expected output is: 1, 3, 4, 5, 2, 2
    ///     because we first order by frecuency, so 1, 3, 4, 5 are at the beginning as they
    ///     only repeat once. They are followed by 2, 2 as 2 is repeated twice.
    /// </summary>
    public class SortByFrecuencyAndAscending
    {
        [Fact]
        public void Scenarios()
        {
            var input = new List<int> { 5, 3, 1, 2, 2, 4 };
            var expected = new List<int> { 1, 3, 4, 5, 2, 2 };

            var actual = new SortByFrecuencyAndAscendingSolution().Solve(input);

            Assert.Equal(expected, actual);
        }
    }
}

using Xunit;

namespace Exercises.Easy
{
    /*
        It should return an integer that represents the minimum absolute difference between
        any pair of elements. minimumAbsoluteDifference has the following parameter(s):
            arr: an array of integers

        Constrains:
            array size n is 2 <= n <= 10^5
            -10^9 <= arr[i] <= 10^9

        Example:
            given arr = [3, -7, 0] possible pairs are:
                (3, -7), (3,0) and (-7, 0)
            The smallest of these possible differences is 3 from the pair (3,0)
    */
    public class MinimumAbsoluteDifference
    {
        [Theory]
        [InlineData(new int[] { 3, -7, 0 }, 3)]
        [InlineData(new int[] { 1, -3, 71, 68, 17 }, 3)]
        [InlineData(new int[] { -59, -36, -13, 1, -53, -92, -2, -96, -54, 75 }, 1)]
        public void Scenarios(int[] arr, int expected)
        {
            var actual = MinimumAbsoluteDifferenceSolution.Solve(arr);

            Assert.Equal(expected, actual);
        }
    }
}
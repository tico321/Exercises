using Xunit;

namespace Exercises.Easy
{
    /*

        Given a 6*6 2D array, arr:
            1 1 1 0 0 0
            0 1 0 0 0 0
            1 1 1 0 0 0
            0 0 0 0 0 0
            0 0 0 0 0 0
            0 0 0 0 0 0

        We define an hourglass in A to be a subset of values with indices falling in this pattern in arr's
        graphical representation:
            a b c
              d
            e f g

        There are 16 hourglasses in arr, and an hourglass sum is the sum of an hourglass values. Calculate the hourglass
        sum for every hourglass in arr, then print the maximum hourglass sum.

        For example given the 2D array:
            1 1 1 0 0 0
            0 1 0 0 0 0
            1 1 1 0 0 0
            0 0 2 4 4 0
            0 0 0 2 0 0
            0 0 1 2 4 0

        The output would be 19, because arr contains the following hourglasses:
            1 1 1   1 1 0   1 0 0   0 0 0
              1       0       0       0
            1 1 1   1 1 0   1 0 0   0 0 0

            0 1 0   1 0 0   0 0 0   0 0 0
              1       1       0       0
            0 0 2   0 2 4   2 4 4   4 4 0

            1 1 1   1 1 0   1 0 0   0 0 0
              0       2       4       4
            0 0 0   0 0 2   0 2 0   2 0 0

            0 0 2   0 2 4   2 4 4   4 4 0
              0       0       2       0
            0 0 1   0 1 2   1 2 4   2 4 0

        The hourglass with the maximum sum is:
            2 4 4
              2
            1 2 4
     */
    public class Hourglass2DArray
    {
        [Fact]
        public void Scenario1()
        {
            var arr = new int[][]
            {
                new int[] { 1, 1, 1, 0, 0, 0 },
                new int[] { 0, 1, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 0, 0, 0 },
                new int[] { 0, 0, 2, 4, 4, 0 },
                new int[] { 0, 0, 0, 2, 0, 0 },
                new int[] { 0, 0, 1, 2, 4, 0 }
            };
            var expected = 19;

            var actual = new Hourglass2DArraySolution().Solve(arr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Scenario2()
        {
            var arr = new int[][]
            {
                new int[] { 1, 1,  1,  0,  0, 0 },
                new int[] { 0, 1,  0,  0,  0, 0 },
                new int[] { 1, 1,  1,  0,  0, 0 },
                new int[] { 0, 9,  2, -4, -4, 0 },
                new int[] { 0, 0,  0, -2,  0, 0 },
                new int[] { 0, 0, -1, -2, -4, 0 }
            };
            var expected = 13;

            var actual = new Hourglass2DArraySolution().Solve(arr);

            Assert.Equal(expected, actual);
        }
    }
}

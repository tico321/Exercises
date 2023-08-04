using Xunit;

namespace Exercises.Medium
{
    /// <summary>
    /// In problem “Maximum Length of Repeated Subarray” we have given two arrays Array 1 and Array 2,
    /// your task is to find the maximum length of the sub-array that appears in both the arrays.
    ///
    /// arr1: [1,2,3,2,1]
    /// arr2: [3,2,1,4,7]
    /// output: 3
    ///
    /// 3 2 1 are repeated in both arrays.
    /// </summary>
    public sealed class MaximumLengthOfRepeatedSubArray
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 2, 1 }, new[] { 3, 2, 1, 4, 7 }, 3)]
        public void Test(int[] arr1, int[] arr2, int expected)
        {
            Assert.Equal(expected, Solve(arr1, arr2));
        }

        private int Solve(int[] arr1, int[] arr2)
        {
            int result = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < arr2.Length && i + j < arr1.Length; j++)
                {
                    if (arr1[i + j] == arr2[j])
                    {
                        count++;
                        if (result < count)
                        {
                            result = count;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            return result;
        }
    }
}

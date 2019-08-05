using System.Linq;

namespace Exercises.Hard
{
    public class MergeSortCountingInversionsSolution
    {
        public static (int[], long) SortAndCount(int[] arr)
        {
            if(arr.Length == 1) return (arr, 0);

            var mid = arr.Length / 2;
            var (left, leftCount) = SortAndCount(arr.Take(mid).ToArray());
            var (right, rightCount) = SortAndCount(arr.Skip(mid).ToArray());
            var (merge, mergeCount) = MergeAndCount(left, right);

            return (merge, leftCount + rightCount + mergeCount);
        }

        private static (int[], long) MergeAndCount(int[] left, int[] right)
        {
            var result = new int[left.Length + right.Length];
            int i=0, j=0, k=0;
            var swaps = 0L;
            while(i < left.Length && j < right.Length)
            {
                if(left[i] <= right[j])
                {
                    result[k++] = left[i++];
                }
                else
                {
                    result[k++] = right[j++];
                    swaps += left.Length - i;
                }
            }

            while(i < left.Length) result[k++] = left[i++];
            while(j < right.Length) result[k++] = right[j++];

            return (result, swaps);
        }
    }
}
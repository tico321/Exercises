namespace Exercises.Hard
{
    public class ArrayManipulationSolution
    {
        /*
            Solution resources:
                https://www.geeksforgeeks.org/difference-array-range-update-query-o1/
                https://www.hackerrank.com/challenges/crush/forum/comments/69550
                https://www.hackerrank.com/challenges/crush/forum/comments/395339
                https://www.youtube.com/watch?v=hDhf04AJIRs&feature=youtu.be
         */
        public static long Solve(int n, int[][] queries)
        {
            var N = n+1;//n+1 array to just use lowerIndex and upperIndex more naturally
            var differenceArray = new int[N];
            for(var i =0; i<queries.Length; i++)
            {
                var lowerIndex = queries[i][0];
                var upperIndex = queries[i][1];
                var addValue = queries[i][2];

                differenceArray[lowerIndex] += addValue;
                if(upperIndex <= n-1) differenceArray[upperIndex + 1] -= addValue;
            }

            var max = 0L;
            for(long i=1, acc=0; i<N; i++)
            {
                acc += differenceArray[i];
                if(max<acc) max=acc;
            }

            return max;
        }
    }
}
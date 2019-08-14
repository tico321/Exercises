using System;

namespace Exercises.Easy
{
    public class MinimumAbsoluteDifferenceSolution
    {
        public static int Solve(int[] arr)
        {
            Array.Sort(arr);
            var min = int.MaxValue;
            for(var i=0; i<arr.Length-1; i++)
            {
                var diff = Math.Abs(arr[i] - arr[i+1]);
                if(diff < min) min = diff;
            }

            return min;
        }
    }
}
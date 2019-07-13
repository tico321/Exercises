using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class MinimumSwapsSolution
    {
        public int Solve(IEnumerable<int> input)
        {
            var arr = input.ToArray(); // for convenience we convert it to an array
            // we iterate one time to associate value with position O(n)
            var valuePosDictionary = new Dictionary<int, int>();
            for (var i = 0; i < arr.Length; i++)
            {
                valuePosDictionary.Add(arr[i], i);
            }

            var swaps = 0;
            // we iterate again to swap positions O(n)
            for (var i = 0; i < arr.Length - 1; i++)
            {
                var current = arr[i];
                var target = i + 1;
                if (current == target) continue;
                //swap arr
                var targetPos = valuePosDictionary[target];
                arr[i] = target;
                arr[targetPos] = current;
                //update dic
                valuePosDictionary[target] = i;
                valuePosDictionary[current] = targetPos;

                swaps++;
            }

            return swaps;
        }
    }
}

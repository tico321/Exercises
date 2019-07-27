using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class LubaAndTheTicketSolution
    {
        public static int Solve(string input1)
        {
            var firstTree = input1.Substring(0, 3).Select(c => int.Parse(c.ToString()));
            var lastTree = input1.Substring(3).Select(c => int.Parse(c.ToString()));

            return InnerSolve(firstTree.ToList(), lastTree.ToList(), 0);
        }

        private static int InnerSolve(
            List<int> firstTree,
            List<int> lastTree,
            int count)
        {
            var firstSum = firstTree.Sum();
            var lastSum = lastTree.Sum();
            if (firstSum == lastSum) return count;
            var difference = Math.Abs(firstSum - lastSum);
            var firstDiff = 0;
            if (lastSum > firstSum)//needs increment
            {
                firstTree.Sort();
                firstDiff = 9 - firstTree.First();
            }
            else
            {
                firstTree.Sort((a, b) => b - a);
                firstDiff = firstTree.First();
            }

            var lastDiff = 0;
            if (firstSum > lastSum)//needs increment
            {
                lastTree.Sort();
                lastDiff = 9 - lastTree.First();
            }
            else
            {
                lastTree.Sort((a, b) => b - a);
                lastDiff = lastTree.First();
            }

            var updateFirst = firstDiff > lastDiff;

            var list = (updateFirst ? firstTree : lastTree);
            var other = (updateFirst ? lastTree : firstTree);
            if (list.Sum() > other.Sum())// need to decrement
            {
                list.Sort((a, b) => b - a);
                var original = list.First();
                var decremented = original > difference ?
                    original - difference :
                    0;
                list = list.Skip(1).ToList();
                list.Add(decremented);
                return updateFirst ?
                    InnerSolve(list, lastTree, count + 1) :
                    InnerSolve(firstTree, list, count + 1);
            }
            else //need to increment
            {
                list.Sort();
                var original = list.First();
                var incremented = difference >= 9 ?
                    9 :
                    (9 - original) <= difference ? 9 : original + difference;
                list = list.Skip(1).ToList();
                list.Add(incremented);
                return updateFirst ?
                    InnerSolve(list, lastTree, count + 1) :
                    InnerSolve(firstTree, list, count + 1);
            }
        }
    }
}

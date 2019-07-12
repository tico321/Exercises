using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    // If having problems finding a solution I'll recommend to understand the cuadratic solution first.
    public class NewYearChaosSolution
    {
        public IEnumerable<int> Solve(IEnumerable<int> input)
        {

            var testCases = input.First();
            var remaining = input.Skip(1);
            var testCaseResults = new List<int>();
            for (var i = 0; i < testCases; i++)
            {
                var n = remaining.First();
                var q = remaining.Skip(1).Take(n);
                remaining = remaining.Skip(n + 1);
                var minimumBrives = this.SolveCase(q.ToArray());
                testCaseResults.Add(minimumBrives);
            }

            return testCaseResults;
        }

        public int SolveCase(int[] q)
        {
            int totalBrives = 0;
            // we track 3 positions to see how many brives each person has payed
            int expectedFirst = 1;
            int expectedSecond = 2;
            int expectedThird = 3;

            for (int i = 0; i < q.Length; i++)
            {
                var personOriginalSticker = q[i];
                if (personOriginalSticker == expectedFirst) // the person was expected in this position (didn't pay any brive)
                {
                    // first was found, we need to keep looking second and third
                    expectedFirst = expectedSecond;
                    expectedSecond = expectedThird;
                    // third moves to the logically next expectation
                    expectedThird++;
                }
                else if (personOriginalSticker == expectedSecond) // the person advanced one position
                {
                    totalBrives++;
                    // we update the second expectation, while we need to keep looking the first one
                    expectedSecond = expectedThird;
                    // third moves to the logically next expectation
                    expectedThird++;
                }
                else if (personOriginalSticker == expectedThird) // the person advanced two positions
                {
                    totalBrives += 2;
                    //we are still expecting to find expectedFirst and expectedSecond
                    // third moves to the logically next expectation
                    expectedThird++;
                }
                else
                {
                    return -1;
                }
            }

            return totalBrives;
        }
    }
}

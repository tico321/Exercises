using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class NewYearChaosQuadraticSolution
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
            int brives = 0;
            for (var i = 0; i < q.Length; i++)
            {
                var personOriginalSticker = q[i];
                var personCurrentPosition = i + 1;
                if (personOriginalSticker - personCurrentPosition > 2) // no one can move more than 2 places forward
                {
                    return -1;
                }

                for (var j = 0; j < personCurrentPosition; j++) // we iterate until the person current position
                {
                    var personAhead = q[j];
                    if (personAhead > personOriginalSticker) // if the person ahead holds a greater value it means it brived the other person
                    {
                        brives += 1;
                    }
                }
            }

            return brives;
        }
    }
}

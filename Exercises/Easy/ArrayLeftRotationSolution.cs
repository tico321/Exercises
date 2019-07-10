using System.Collections.Generic;
using System.Linq;

namespace Exercises.Easy
{
    public class ArrayLeftRotationSolution
    {
        public IEnumerable<int> Solve(IEnumerable<int> a, int d)
        {
            var rotations = d % a.Count();
            var firstHalf = a.Take(rotations);
            var secondHalf = a.Skip(rotations);
            return secondHalf.Concat(firstHalf);
        }
    }
}

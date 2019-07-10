using System.Collections.Generic;
using System.Linq;

namespace Exercises.Easy
{
    public class JumpingOnTheCloudsSolution
    {
        public int Solve(int n, IEnumerable<int> c)
        {
            var steps = 0;
            var clouds = c.ToArray();
            for (var pos = 0; pos < n - 1;)
            {
                if (pos + 2 < n && clouds[pos + 2] == 0) pos += 2;
                else pos++;
                steps++;
            }

            return steps;
        }
    }
}

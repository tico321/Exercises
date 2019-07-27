using System.Linq;

namespace Exercises.Easy
{
    public class XeniaAndRingRoadSolution
    {
        public static long Solve(string input1, string input2)
        {
            var args1 = input1.Split(' ');
            var args2 = input2.Split(' ');
            var n = int.Parse(args1[0]);
            var m = int.Parse(args1[1]);
            var a = input2.Split(' ').Select(c => int.Parse(c)).ToArray();

            var pos = 1;
            var time = 0L;
            for (var i = 0; i < m; i++)
            {
                var taskPos = a[i];
                if (taskPos == pos) continue;
                if (taskPos > pos) time += taskPos - pos;
                else time += n - pos + taskPos;
                pos = taskPos;
            }

            return time;
        }
    }
}

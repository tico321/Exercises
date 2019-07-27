namespace Exercises.Easy
{
    public class VanyaAndTableSolution
    {
        public static long Solve(int n, string[] rows)
        {
            var result = 0L;
            for (var i = 0; i < n; i++)
            {
                var args = rows[i].Split(' ');
                var x1 = int.Parse(args[0]);
                var y1 = int.Parse(args[1]);
                var x2 = int.Parse(args[2]);
                var y2 = int.Parse(args[3]);
                var area = (x2 - x1 + 1) * (y2 - y1 + 1);
                result += area;
            }

            return result;
        }
    }
}

namespace Exercises.Easy
{
    public class DivisibilitySolution
    {
        public static ulong Solve(string input1)
        {
            var n = ulong.Parse(input1);
            var minMultiple = 2520UL;//min number divisible by 2-10 = 5*7*8*9 = 2520
            var res = n / minMultiple;
            return res;
        }
    }
}

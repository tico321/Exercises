namespace Exercises.Easy
{
    public class TheatreSquareSolution
    {
        public static long Solve(long n, long m, long a)
        {
            var horizontalFlagstones = GetFlagstones(n, a);
            var verticalFlagstones = GetFlagstones(m, a);

            return horizontalFlagstones * verticalFlagstones;
        }

        private static long GetFlagstones(long side, long a)
        {
            long flagstones;
            if (side <= a)
            {
                flagstones = 1;
            }
            else
            {
                flagstones = side / a;
                if (side % a > 0) flagstones++;
            }

            return flagstones;
        }
    }
}

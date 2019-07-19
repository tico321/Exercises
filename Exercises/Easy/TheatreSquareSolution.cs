namespace Exercises.Easy
{
    public class TheatreSquareSolution
    {
        public int Solve(int n, int m, int a)
        {
            var horizontalFlagstones = 0;
            do
            {
                n -= a;
                horizontalFlagstones++;
            } while (n > 0);

            var verticalFlagstones = 0;
            do
            {
                m -= a;
                verticalFlagstones++;
            } while (m > 0);

            return horizontalFlagstones * verticalFlagstones;
        }
    }
}

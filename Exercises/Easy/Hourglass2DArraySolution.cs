namespace Exercises.Easy
{
    public class Hourglass2DArraySolution
    {
        public int Solve(int[][] arr)
        {
            var hourglasses = new int[4,4];
            var max = int.MinValue;
            for (int i=0; i<4; i++)
            {
                for(int j=0; j<4; j++)
                {
                    hourglasses[i, j] = this.CalculateHourglass(arr, i, j);
                    if(hourglasses[i, j] > max)
                    {
                        max = hourglasses[i, j];
                    }
                }
            }

            return max;
        }

        private int CalculateHourglass(int[][] arr, int i, int j)
        {
            return arr[i][j]     + arr[i][j + 1]     + arr[i][j + 2] +
                                   arr[i + 1][j + 1] +
                   arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];
        }
    }
}

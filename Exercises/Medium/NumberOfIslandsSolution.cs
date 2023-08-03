namespace Exercises.Medium
{
    public sealed class NumberOfIslandsSolution
    {
        public static int Solve(char[][] grid)
        {
            int islands = 0;
            for(int x=0; x < grid.Length; x++)
            {
                for(int y=0; y < grid[x].Length; y++)
                {
                    if(grid[x][y] == '1')
                    {
                        islands++;
                        MarkIsland(grid, x, y);
                    }
                }
            }

            return islands;
        }

        private static void MarkIsland(char[][] grid, int x, int y)
        {
            if (x < 0 || x >= grid.Length || y < 0 || y >= grid[x].Length || grid[x][y] != '1')
                return;

            grid[x][y] = 'x';

            MarkIsland(grid, x-1, y);
            MarkIsland(grid, x+1, y);
            MarkIsland(grid, x, y-1);
            MarkIsland(grid, x, y+1);
        }
    }
}

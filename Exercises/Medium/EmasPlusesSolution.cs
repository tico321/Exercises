using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    public class EmasPlusesSolution
    {
        public static int twoPluses(List<string> grid)
        {
            var rows = grid.Count;
            var cols = grid[0].Length;
            var pluses = Enumerable
                .Range(0, rows)
                .SelectMany(r => Enumerable.Range(0, cols).Select(c => (r, c)))
                .Aggregate(new List<(int, int, int, int)>(), (acc, pos) => CalculatePluses(grid, pos, acc))
                .Where(p => p.Item1 > 0)
                .ToList();
            return pluses
                .SelectMany(p => Enumerable.Range(0, pluses.Count).Select(i => new { a= p, b=pluses[i] }))
                .Where(ps => !Collide(ps.a, ps.b, grid))
                .Select(i => i.a.Item1 * i.b.Item1)
                .Max();
        }

        private static List<(int, int, int, int)> CalculatePluses(List<string> grid, (int r, int c) pos, List<(int, int, int, int)> acc)
        {
            if (IsBad(grid, pos))
            {
                acc.Add((0, 0, pos.r, pos.c));
                return acc;
            }

            for (int i = 1, plus = 1;; i++, plus += 4)
            {
                var top = (pos.r - i, pos.c);
                var bottom = (pos.r + i, pos.c);
                var left = (pos.r, pos.c - i);
                var right = (pos.r, pos.c + i);
                acc.Add((plus + 0, i-1, pos.r, pos.c));
                if (IsBad(grid, top) || IsBad(grid, right) || IsBad(grid, bottom) || IsBad(grid, left))
                {
                    return acc;
                }
            }
        }

        private static bool IsBad(List<string> grid, (int r, int c) pos)
        {
            var rows = grid.Count;
            var cols = grid[0].Length;
            if (pos.r < 0 || pos.c < 0) return true;
            if (pos.r >= rows || pos.c >= cols) return true;
            return grid[pos.r][pos.c] == 'B';
        }

        private static bool Collide((int, int, int, int) a, (int, int, int, int) b, List<string> originalGrid)
        {
            var rows = originalGrid.Count;
            var cols = originalGrid[0].Length;
            var (plusA, expandA, rowA, colA) = a;
            var (plusB, expandB, rowB, colB) = b;
            var grid = new bool[rows,cols];
            grid[rowA, colA] = true;
            for (var i = 1; i <= expandA; i++)
            {
                grid[rowA - i, colA] = true;
                grid[rowA + i, colA] = true;
                grid[rowA, colA - i] = true;
                grid[rowA, colA + i] = true;
            }

            for (var i = 0; i <= expandB; i++)
            {
                if (grid[rowB - i, colB] ||
                    grid[rowB + i, colB] ||
                    grid[rowB, colB - i] ||
                    grid[rowB, colB + i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
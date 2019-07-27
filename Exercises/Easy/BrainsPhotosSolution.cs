using System.Collections.Generic;

namespace Exercises.Easy
{
    public class BrainsPhotosSolution
    {
        public static string Solve(int n, int m, string[] strRows)
        {
            var color = new HashSet<char>(new List<char> { 'C', 'M', 'Y' });
            for (int i = 0; i < n; i++)
            {
                for (var j = 0; j < strRows[i].Length; j++)
                {
                    if (color.Contains(strRows[i][j])) return "#Color";
                }
            }

            return "#Black&White";
        }
    }
}

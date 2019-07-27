using System.Text;

namespace Exercises.Easy
{
    public class CielAndDancingSolution
    {
        public static string Solve(string input)
        {
            var args = input.Split(' ');
            var boys = int.Parse(args[0]);
            var girls = int.Parse(args[1]);

            var result = new StringBuilder();
            var j = 0;
            var dances = 0;
            for (var i = 0; i < boys; i++)
            {
                for (; j < girls; j++, dances++)
                {
                    result.Append($"{i + 1} {j + 1}\n");
                }
                j--;
            }

            result.Insert(0, $"{dances}\n");
            result.Remove(result.Length - 1, 1);

            return result.ToString();
        }
    }
}

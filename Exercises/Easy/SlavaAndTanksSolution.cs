using System.Text;

namespace Exercises.Easy
{
    public class SlavaAndTanksSolution
    {
        public static (string, string) Solve(string input)
        {
            var n = int.Parse(input);
            var bombs = 0;
            var sequence = new StringBuilder();
            for (var i = 2; i <= n; i += 2, bombs++)
                sequence.Append($"{i} ");
            for (var i = 1; i <= n; i += 2, bombs++)
                sequence.Append($"{i} ");
            for (var i = 2; i <= n; i += 2, bombs++)
                sequence.Append($"{i} ");

            sequence.Remove(sequence.Length - 1, 1);
            return (bombs.ToString(), sequence.ToString());
        }
    }
}

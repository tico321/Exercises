using System.Text;

namespace Exercises.Easy
{
    public class BallGameSolution
    {
        public static string Solve(string s)
        {
            var n = int.Parse(s);
            var throws = n - 1;
            var pass = 1;
            var kid = 1;
            var result = new StringBuilder();
            while (throws > 0)
            {
                var targetPos = kid + pass;
                kid = targetPos % n;
                throws--;
                pass++;
                result.Append($"{kid} ");
            }

            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}

using System.Linq;

namespace Exercises.Medium
{
    public class TestingPantsForSadnessSolution
    {
        // 4 4 4
        // x
        // x
        // x
        // c x
        // c x
        // c x
        // c c x
        // c c x
        // c c x
        // c c c
        public static long Solve(string input1, string input2)
        {
            var questions = int.Parse(input1);
            var questionAnswers = input2.Split(' ').Select(c => long.Parse(c)).ToArray();
            var tries = questionAnswers[0];
            for (var i = 1; i < questions; i++)
            {
                var errorPaths = questionAnswers[i] - 1;
                var errorTries = errorPaths * i;
                tries = tries + questionAnswers[i] + errorTries;
            }

            return tries;
        }
    }
}
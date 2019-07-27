namespace Exercises.Easy
{
    public class HungryStudentProblemSolution
    {
        public static string Solve(string input1)
        {
            var n = int.Parse(input1);
            if (n % 3 == 0) return "YES";
            if (n % 7 == 0) return "YES";
            while (n > 0)
            {
                n -= 3;
                if (n % 7 == 0) return "YES";
            }
            return "NO";
        }
    }
}

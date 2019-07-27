using System.Linq;

namespace Exercises.Easy
{
    public class UnimodalArraySolution
    {
        public static string Solve(string input1, string input2)
        {
            var n = int.Parse(input1);
            if (n == 1) return "YES";
            var array = input2.Split(' ').Select(c => int.Parse(c)).ToArray();

            var previous = array[0];
            var i = 1;

            //increase
            for (; i < n; i++)
            {
                if (previous == array[i]) break; //constant began
                if (previous > array[i]) break; //constant of 1 and begin decrease
                previous = array[i];
            }

            //constant
            for (; i < n; i++)
            {
                if (array[i] > previous) return "NO"; //cannot increase again
                if (previous > array[i]) break; //begin decrease
            }

            //decrease
            for (; i < n; i++)
            {
                if (array[i] > previous) return "NO";
                if (array[i] == previous) return "NO";
                previous = array[i];
            }

            return "YES";
        }
    }
}

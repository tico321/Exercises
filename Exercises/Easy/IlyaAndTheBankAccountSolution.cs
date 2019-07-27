namespace Exercises.Easy
{
    public class IlyaAndTheBankAccountSolution
    {
        public static string Solve(string input)
        {
            if (input[0] != '-') return input;
            var option1 = input.Remove(input.Length - 1);
            var option2 = $"{input.Substring(0, input.Length - 2)}{input[input.Length - 1]}";
            var compare = option1.CompareTo(option2);
            var result = compare < 0 ? option1 : option2;
            return result != "-0" ? result : "0";
        }

        public static string Solve2(string input)
        {
            if (input[0] != '-') return input;
            var last = input[input.Length - 1];
            var beforeLast = input[input.Length - 2];
            char toInclude;
            if (last.CompareTo(beforeLast) > 0) toInclude = beforeLast;
            else toInclude = last;
            var result = $"{input.Substring(0, input.Length - 2)}{toInclude}";
            return result == "-0" ? "0" : result;
        }
    }
}

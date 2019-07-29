namespace Exercises.Easy
{
    public class PetyaAndJavaSolution
    {
        public static string Solve(string input)
        {
            var isNegative = input[0] == '-';
            input = isNegative ? input.Substring(1) : input;
            if (input.Length < 3) return "byte";
            else if (input.Length == 3)
            {
                if (isNegative) return input.CompareTo("128") > 0 ? "short" : "byte";
                else return input.CompareTo("127") > 0 ? "short" : "byte";
            }
            else if (input.Length < 5) return "short";
            else if (input.Length == 5)
            {
                if (isNegative) return input.CompareTo("32768") > 0 ? "int" : "short";
                else return input.CompareTo("32767") > 0 ? "int" : "short";
            }
            else if (input.Length < 10) return "int";
            else if (input.Length == 10)
            {
                if (isNegative) return input.CompareTo("2147483648") > 0 ? "long" : "int";
                else return input.CompareTo("2147483647") > 0 ? "long" : "int";
            }
            else if (input.Length < 19) return "long";
            else if (input.Length == 19)
            {
                if (isNegative) return input.CompareTo("9223372036854775808") > 0 ? "BigInteger" : "long";
                else return input.CompareTo("9223372036854775807") > 0 ? "BigInteger" : "long";
            }

            return "BigInteger";
        }
    }
}

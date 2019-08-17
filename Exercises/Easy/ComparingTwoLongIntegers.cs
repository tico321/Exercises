using Xunit;

namespace Exercises.Easy
{
    /*
        Comparing Two Long Integers
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        You are given two very long integers a, b (leading zeroes are allowed). You should check what number a or b is greater or determine that they are equal.

        The input size is very large so don't use the reading of symbols one by one. Instead of that use the reading of a whole line or token.

        As input/output can reach huge size it is recommended to use fast input/output methods: for example, prefer to use scanf/printf instead of cin/cout in C++, prefer to use BufferedReader/PrintWriter instead of Scanner/System.out in Java. Don't use the function input() in Python2 instead of it use the function raw_input().

        Input
        The first line contains a non-negative integer a.

        The second line contains a non-negative integer b.

        The numbers a, b may contain leading zeroes. Each of them contains no more than 106 digits.

        Output
        Print the symbol "<" if a < b and the symbol ">" if a > b. If the numbers are equal print the symbol "=".
     */
    public class ComparingTwoLongIntegers
    {
        [Theory]
        [InlineData("9", "10", "<")]
        [InlineData("11", "10", ">")]
        [InlineData("00012345", "12345", "=")]
        [InlineData("0123", "9", ">")]
        [InlineData("0", "0000", "=")]
        [InlineData("00000000", "0000", "=")]
        public void Scenarios1(string input1, string input2, string expected)
        {
            var actual = ComparingTwoLongIntegersSolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("9", "10", "<")]
        [InlineData("11", "10", ">")]
        [InlineData("00012345", "12345", "=")]
        [InlineData("0123", "9", ">")]
        [InlineData("0", "0000", "=")]
        [InlineData("00000000", "0000", "=")]
        public void Scenarios2(string input1, string input2, string expected)
        {
            var actual = ComparingTwoLongIntegersSolution.Solve2(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}

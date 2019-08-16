using Xunit;

namespace Exercises.Easy
{
    //You are given a string. The string is consider valid if all the characteres
    //appear exactly the same number of times.
    //You are allowed to remove exactly one character to make it valid.
    public class SherlockAndTheValidString
    {
        [Theory]
        [InlineData("a", "YES")]
        [InlineData("ab", "YES")]
        [InlineData("abb", "YES")]
        [InlineData("abbb", "NO")]
        [InlineData("aaab", "YES")]
        [InlineData("aaabb", "YES")]
        [InlineData("aabbb", "YES")]
        [InlineData("abacbab", "YES")]
        public void stringTest(string s, string expected)
        {
            var actual = SherlockAndTheValidStringSolution.Solve(s);

            Assert.Equal(expected, actual);
        }
    }
}
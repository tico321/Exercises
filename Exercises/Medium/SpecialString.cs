using Xunit;

namespace Exercises.Medium
{
    /*
        A string is said to be a special string if either of two conditions is met:
            * All of the characters are the same, e.g. aaa.
            * All characters except the middle one are the same, e.g. aadaa.
        A special substring is any substring of a string which meets one of those criteria. Given a string,
        determine how many special substrings can be formed from it.

        For example, given the string s=mnonopoo, we have the following special substrings:
        {m,n,o,n,o,p,o,o,non,ono,opo,oo}
        Constraints
            * 1 <= n <= 10^6 where n is the size of the string
            * s[i] is inside the set [a-z]
     */
    public class SpecialString
    {
        [Theory]
        [InlineData(8, "mnonopoo", 12)]
        [InlineData(5, "asasd", 7)] // [a, s, a, s, d, asa, sas]
        [InlineData(7, "abcbaba", 10)] // [a, b, c, b, a, b, a, bcb, bab, aba]
        [InlineData(4, "aaaa", 10)] //[a, a, a, a, aa, aa, aa, aaa, aaa, aaa]
        public void Scenarios(int n, string s, int expected)
        {
            var actual = SpecialStringSolution.Solve(n, s);

            Assert.Equal(expected, actual);
        }
    }
}
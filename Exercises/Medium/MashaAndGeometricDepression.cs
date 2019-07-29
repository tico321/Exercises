using Xunit;

namespace Exercises.Medium
{
    /*
        Masha and geometric depression
        time limit per test1 second
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Masha really loves algebra. On the last lesson, her strict teacher Dvastan gave she new exercise.

        You are given geometric progression b defined by two integers b1 and q. Remind that a geometric progression is a sequence of integers b1, b2, b3, ..., where for each i > 1 the respective term satisfies the condition bi = bi - 1·q, where q is called the common ratio of the progression. Progressions in Uzhlyandia are unusual: both b1 and q can equal 0. Also, Dvastan gave Masha m "bad" integers a1, a2, ..., am, and an integer l.

        Masha writes all progression terms one by one onto the board (including repetitive) while condition |bi| ≤ l is satisfied (|x| means absolute value of x). There is an exception: if a term equals one of the "bad" integers, Masha skips it (doesn't write onto the board) and moves forward to the next term.

        But the lesson is going to end soon, so Masha has to calculate how many integers will be written on the board. In order not to get into depression, Masha asked you for help: help her calculate how many numbers she will write, or print "inf" in case she needs to write infinitely many integers.

        Input
        The first line of input contains four integers b1, q, l, m (-109 ≤ b1, q ≤ 109, 1 ≤ l ≤ 109, 1 ≤ m ≤ 105) — the initial term and the common ratio of progression, absolute value of maximal number that can be written on the board and the number of "bad" integers, respectively.

        The second line contains m distinct integers a1, a2, ..., am (-109 ≤ ai ≤ 109) — numbers that will never be written on the board.

        Output
        Print the only integer, meaning the number of progression terms that will be written on the board if it is finite, or "inf" (without quotes) otherwise.
     */
    public class MashaAndGeometricDepression
    {
        [Theory]
        [InlineData("3 2 30 4", "6 14 25 48", "3")]
        [InlineData("123 1 2143435 4", "54343 -13 6 124", "inf")]
        [InlineData("123 1 2143435 4", "123 11 -5453 141245", "0")]
        [InlineData(
            "1000000000 1000000000 1000000000 100000",
            "-867369287 -816625161 250106552 -400193572 -446911177 292734357 -395531683 -722404635 -55290003 737242104 299573201 99652615 318889502 458150052 755903501 386817735 -804690964 -926209895 795402694 764327680 865496192 491986848 -521892001 -768476404 -922228767 -576665906 746807636 -110563238 538259634 -993841241 -86896185 449223857 600072261 -187153239 811601862 -961166321 820706110 280028269 -126065742 174687664 45277141 532221161 -705872380 -156888486 674215289",
            "1")]
        [InlineData("11 0 228 5", "-1 0 1 5 -11245", "1")]
        [InlineData("123 -1 2143435 5", "-123 0 12 5 -11245", "inf")]
        [InlineData("123 -1 2143435 5", "123 0 12 5 -11245", "inf")]
        public void Scenarios(string input, string input2, string expected)
        {
            var actual = MashaAndGeometricDepressionSolution.Solve(input, input2);

            Assert.Equal(expected, actual);
        }
    }
}

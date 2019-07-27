using Xunit;

namespace Exercises.Easy
{
    /*
        Mike and gcd problem
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Mike has a sequence A = [a1, a2, ..., an] of length n. He considers the sequence B = [b1, b2, ..., bn] beautiful if the gcd of all its elements is bigger than 1, i.e. .

        Mike wants to change his sequence in order to make it beautiful. In one move he can choose an index i (1 ≤ i < n), delete numbers ai, ai + 1 and put numbers ai - ai + 1, ai + ai + 1 in their place instead, in this order. He wants perform as few operations as possible. Find the minimal number of operations to make sequence A beautiful if it's possible, or tell him that it is impossible to do so.

         is the biggest non-negative number d such that d divides bi for every i (1 ≤ i ≤ n).

        Input
        The first line contains a single integer n (2 ≤ n ≤ 100 000) — length of sequence A.

        The second line contains n space-separated integers a1, a2, ..., an (1 ≤ ai ≤ 109) — elements of sequence A.

        Output
        Output on the first line "YES" (without quotes) if it is possible to make sequence A beautiful by performing operations described above, and "NO" (without quotes) otherwise.

        If the answer was "YES", output the minimal number of moves needed to make sequence A beautiful.
     */
    public class MikeGCDProblem
    {
        [Theory]
        [InlineData("2", "1 1", "YES", "1")]
        [InlineData("3", "6 2 4", "YES", "0")]
        [InlineData("2", "1 3", "YES", "1")]
        [InlineData("2", "2 4", "YES", "0")]
        [InlineData("2", "3 6", "YES", "0")]
        [InlineData("2", "1 0", "YES", "2")]
        [InlineData("3", "3 6 10", "YES", "2")]
        [InlineData("3", "3 6 9", "YES", "0")]
        [InlineData("3", "0 0 0", "NO", "0")]
        [InlineData("9", "57 30 28 81 88 32 3 42 25", "YES", "8")]
        [InlineData("9", "57 30 28 81 88 32 3 25 42", "YES", "5")]
        [InlineData("4", "92 46 3 21", "YES", "1")]
        [InlineData("3", "61683765 133017144 218267946", "YES", "0")]
        public void Scenarios(string input1, string input2, string answer, string moves)
        {
            var actual = MikeGCDProblemSolution.Solve(input1, input2);

            Assert.Equal(answer, actual.Item1);
            Assert.Equal(moves, actual.Item2);
        }
    }
}

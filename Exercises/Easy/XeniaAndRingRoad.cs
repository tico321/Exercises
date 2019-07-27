using Xunit;

namespace Exercises.Easy
{
    /*
        Xenia and Ringroad
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Xenia lives in a city that has n houses built along the main ringroad. The ringroad houses are numbered 1 through n in the clockwise order. The ringroad traffic is one way and also is clockwise.

        Xenia has recently moved into the ringroad house number 1. As a result, she's got m things to do. In order to complete the i-th task, she needs to be in the house number ai and complete all tasks with numbers less than i. Initially, Xenia is in the house number 1, find the minimum time she needs to complete all her tasks if moving from a house to a neighboring one along the ringroad takes one unit of time.

        Input
        The first line contains two integers n and m (2 ≤ n ≤ 105, 1 ≤ m ≤ 105). The second line contains m integers a1, a2, ..., am (1 ≤ ai ≤ n). Note that Xenia can have multiple consecutive tasks in one house.

        Output
        Print a single integer — the time Xenia needs to complete all tasks.

        example:
        input
        4 3
        3 2 3
        output
        6
        1 → 2 → 3 → 4 → 1 → 2 → 3. This is optimal sequence. So, she needs 6 time units.

     */
    public class XeniaAndRingRoad
    {
        [Theory]
        [InlineData("4 3", "3 2 3", 6)]
        [InlineData("4 3", "2 3 3", 2)]
        public void Scenarios(string input1, string input2, int expected)
        {
            var actual = XeniaAndRingRoadSolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}

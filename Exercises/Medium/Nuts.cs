using Xunit;

namespace Exercises.Medium
{
    /*
        You have a nuts and lots of boxes. The boxes have a wonderful feature:
        if you put x (x ≥ 0) divisors (the spacial bars that can divide a box) to it,
        you get a box, divided into x + 1 sections.

        You are minimalist. Therefore, on the one hand, you are against dividing some
        box into more than k sections. On the other hand, you are against putting more
        than v nuts into some section of the box. What is the minimum number of boxes
        you have to use if you want to put all the nuts in boxes, and you have b divisors?

        Please note that you need to minimize the number of used boxes, not sections.
        You do not have to minimize the number of used divisors.

        Input
        The first line contains four space-separated integers k, a, b, v
        (2 ≤ k ≤ 1000; 1 ≤ a, b, v ≤ 1000) — the maximum number of sections in the box,
        the number of nuts, the number of divisors and the capacity of each section of the box.

        Output
        Print a single integer — the answer to the problem.
     */
    public class Nuts
    {
        [Theory]
        [InlineData("3 10 3 3", 2)]
        [InlineData("3 10 1 3", 3)]
        [InlineData("100 100 1 1000", 1)]
        [InlineData("1 1000 0 1", 1000)]
        [InlineData("5 347 20 1", 327)]
        [InlineData("477 492 438 690", 1)]
        public void Scenarios(string input, int expected)
        {
            var actual = NutsSolution.Solve(input);

            Assert.Equal(expected, actual);
        }
    }
}

using Xunit;

namespace Exercises.Easy
{
    /*
        Candies
        time limit per test1 second
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        After passing a test, Vasya got himself a box of 𝑛 candies. He decided to eat an equal amount of candies each morning until there are no more candies. However, Petya also noticed the box and decided to get some candies for himself.

        This means the process of eating candies is the following: in the beginning Vasya chooses a single integer 𝑘, same for all days. After that, in the morning he eats 𝑘 candies from the box (if there are less than 𝑘 candies in the box, he eats them all), then in the evening Petya eats 10% of the candies remaining in the box. If there are still candies left in the box, the process repeats — next day Vasya eats 𝑘 candies again, and Petya — 10% of the candies left in a box, and so on.

        If the amount of candies in the box is not divisible by 10, Petya rounds the amount he takes from the box down. For example, if there were 97 candies in the box, Petya would eat only 9 of them. In particular, if there are less than 10 candies in a box, Petya won't eat any at all.

        Your task is to find out the minimal amount of 𝑘 that can be chosen by Vasya so that he would eat at least half of the 𝑛 candies he initially got. Note that the number 𝑘 must be integer.

        Input
        The first line contains a single integer 𝑛 (1≤𝑛≤1018) — the initial amount of candies in the box.

        Output
        Output a single integer — the minimal amount of 𝑘 that would allow Vasya to eat at least half of candies he got.
     */
    public class Candies
    {
        [Theory]
        [InlineData("68", 3L)]
        [InlineData("1", 1L)]
        [InlineData("43", 2L)]
        [InlineData("42", 1L)]
        [InlineData("756", 29L)]
        [InlineData("999999972", 39259423L)]
        public void Scenarios(string input, int expected)
        {
            var actual = CandiesSolution.Solve(input);

            Assert.Equal(expected, actual);
        }
    }
}
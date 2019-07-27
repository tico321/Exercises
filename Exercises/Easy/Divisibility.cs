﻿using Xunit;

namespace Exercises.Easy
{
    /*
        Divisibility
        time limit per test0.5 seconds
        memory limit per test64 megabytes
        inputstandard input
        outputstandard output
        IT City company developing computer games invented a new way to reward its employees. After a new game release users start buying it actively, and the company tracks the number of sales with precision to each transaction. Every time when the next number of sales is divisible by all numbers from 2 to 10 every developer of this game gets a small bonus.

        A game designer Petya knows that the company is just about to release a new game that was partly developed by him. On the basis of his experience he predicts that n people will buy the game during the first month. Now Petya wants to determine how many times he will get the bonus. Help him to know it.

        Input
        The only line of the input contains one integer n (1 ≤ n ≤ 1018) — the prediction on the number of people who will buy the game.

        Output
        Output one integer showing how many numbers from 1 to n are divisible by all numbers from 2 to 10.
     */
    public class Divisibility
    {
        [Theory]
        [InlineData("3000", 1)]
        [InlineData("642600", 255)]
        [InlineData("504000000000000000", 200000000000000)]
        public void Scenarios(string input1, ulong expected)
        {
            var actual = DivisibilitySolution.Solve(input1);

            Assert.Equal(expected, actual);
        }
    }
}

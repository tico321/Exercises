﻿using Xunit;

namespace Exercises.Easy
{
    /*
        Unimodal Array
        time limit per test1 second
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Array of integers is unimodal, if:

        it is strictly increasing in the beginning;
        after that it is constant;
        after that it is strictly decreasing.
        The first block (increasing) and the last block (decreasing) may be absent. It is allowed that both of this blocks are absent.

        For example, the following three arrays are unimodal: [5, 7, 11, 11, 2, 1], [4, 4, 2], [7], but the following three are not unimodal: [5, 5, 6, 6, 1], [1, 2, 1, 2], [4, 5, 5, 6].

        Write a program that checks if an array is unimodal.

        Input
        The first line contains integer n (1 ≤ n ≤ 100) — the number of elements in the array.

        The second line contains n integers a1, a2, ..., an (1 ≤ ai ≤ 1 000) — the elements of the array.

        Output
        Print "YES" if the given array is unimodal. Otherwise, print "NO".

        You can output each letter in any case (upper or lower).
     */
    public class UnimodalArray
    {
        [Theory]
        [InlineData("6", "1 5 5 5 4 2", "YES")]
        [InlineData("5", "10 20 30 20 10", "YES")]
        [InlineData("4", "1 2 1 2", "NO")]
        [InlineData("7", "3 3 3 3 3 3 3", "YES")]
        [InlineData("6", "1 2 3 4 4 4", "YES")]
        [InlineData("7", "1 2 3 4 4 4 3", "YES")]
        [InlineData("9", "1 2 3 4 4 4 3 3 2", "NO")]
        [InlineData("9", "1 2 3 4 4 4 3 2 2", "NO")]
        public void Scenarios(string input1, string input2, string expected)
        {
            var actual = UnimodalArraySolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}

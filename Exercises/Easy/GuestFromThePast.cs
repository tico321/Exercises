﻿using Xunit;

namespace Exercises.Easy
{
    /*
        Guest From the Past
        time limit per test1 second
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Kolya Gerasimov loves kefir very much. He lives in year 1984 and knows all the details of buying this delicious drink. One day, as you probably know, he found himself in year 2084, and buying kefir there is much more complicated.

        Kolya is hungry, so he went to the nearest milk shop. In 2084 you may buy kefir in a plastic liter bottle, that costs a rubles, or in glass liter bottle, that costs b rubles. Also, you may return empty glass bottle and get c (c < b) rubles back, but you cannot return plastic bottles.

        Kolya has n rubles and he is really hungry, so he wants to drink as much kefir as possible. There were no plastic bottles in his 1984, so Kolya doesn't know how to act optimally and asks for your help.

        Input
        First line of the input contains a single integer n (1 ≤ n ≤ 1018) — the number of rubles Kolya has at the beginning.

        Then follow three lines containing integers a, b and c (1 ≤ a ≤ 1018, 1 ≤ c < b ≤ 1018) — the cost of one plastic liter bottle, the cost of one glass liter bottle and the money one can get back by returning an empty glass bottle, respectively.

        Output
        Print the only integer — maximum number of liters of kefir, that Kolya can drink.
     */
    public class GuestFromThePast
    {
        [Theory]
        [InlineData("10", "11", "9", "8", 2L)]
        [InlineData("10", "5", "6", "1", 2L)]
        [InlineData("9", "10", "10", "1", 0L)]
        [InlineData("1000000000000000000", "2", "10", "9", 999999999999999995L)]
        [InlineData("13", "10", "15", "11", 1L)]
        [InlineData("13", "5", "13", "11", 3L)]
        [InlineData("13", "5", "13", "1", 2L)]
        public void Scenarios(string in1, string in2, string in3, string in4, long expected)
        {
            var actual = GuestFromThePastSolution.Solve(in1, in2, in3, in4);

            Assert.Equal(expected, actual);
        }
    }
}

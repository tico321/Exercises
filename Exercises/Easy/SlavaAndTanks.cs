﻿using Xunit;

namespace Exercises.Easy
{
    /*
        Slava and tanks
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Slava plays his favorite game "Peace Lightning". Now he is flying a bomber on a very specific map.

        Formally, map is a checkered field of size 1 × n, the cells of which are numbered from 1 to n, in each cell there can be one or several tanks. Slava doesn't know the number of tanks and their positions, because he flies very high, but he can drop a bomb in any cell. All tanks in this cell will be damaged.

        If a tank takes damage for the first time, it instantly moves to one of the neighboring cells (a tank in the cell n can only move to the cell n - 1, a tank in the cell 1 can only move to the cell 2). If a tank takes damage for the second time, it's counted as destroyed and never moves again. The tanks move only when they are damaged for the first time, they do not move by themselves.

        Help Slava to destroy all tanks using as few bombs as possible.

        Input
        The first line contains a single integer n (2 ≤ n ≤ 100 000) — the size of the map.

        Output
        In the first line print m — the minimum number of bombs Slava needs to destroy all tanks.

        In the second line print m integers k1, k2, ..., km. The number ki means that the i-th bomb should be dropped at the cell ki.

        If there are multiple answers, you can print any of them.


     */
    public class SlavaAndTanks
    {
        [Theory]
        [InlineData("2", "3", "2 1 2")]
        [InlineData("3", "4", "2 1 3 2")]
        [InlineData("4", "6", "2 4 1 3 2 4")]
        public void Scenarios(string input, string expectedN, string expected)
        {
            var actual = SlavaAndTanksSolution.Solve(input);

            Assert.Equal(expectedN, actual.Item1);
            Assert.Equal(expected, actual.Item2);
        }
    }
}

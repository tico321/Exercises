using System;
using Xunit;

namespace Exercises.Hard
{
    public class ArrayManipulation
    {
        [Theory]
        [InlineData("5 3", new string[]{ "1 2 100", "2 5 100", "3 4 100" }, 200)]
        [InlineData("10 3", new string[]{ "1 5 3", "4 8 7", "6 9 1" }, 10)]
        [InlineData("4 3", new string[]{ "2 3 603", "1 1 286", "4 4 882" }, 882)]
        public void Scenarios(string in1, string[] in2, long expected)
        {
            var args1 = in1.Split(' ');
            var n = int.Parse(args1[0]);
            var m = int.Parse(args1[1]);
            var queries = new int[m][];
            for(var i=0; i<m; i++)
            {
                queries[i] = Array.ConvertAll(in2[i].Split(' '), current => int.Parse(current));
            }

            var actual = ArrayManipulationSolution.Solve(n, queries);

            Assert.Equal(expected, actual);
        }
    }
}
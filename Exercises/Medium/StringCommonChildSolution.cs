using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    // https://www.hackerrank.com/challenges/common-child/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=strings
    /*
     * A string is said to be a child of a another string if it can be formed by deleting 0 or more characters from the
     * other string. Letters cannot be rearranged. Given two strings of equal length, what's the longest string that can
     * be constructed such that it is a child of both?
     * Example: s1: "ABCD", s2: "ABDC"
     * These strings have two children with maximum length 3, ABC and ABD. They can be formed by eliminating either
     * the D or C from both strings. Return 3.
     * constraints the strings have between 1 and 5000 lenght.
     */
    public class StringCommonChild
    {
        [Theory]
        [InlineData("ABCD", "ABDC", 3)]
        [InlineData("HARRY", "SALLY", 2)]
        [InlineData("AA", "BB", 0)]
        [InlineData("SHINCHAN", "NOHARAAA", 3)]
        [InlineData("ABCDEF", "FBDAMN", 2)]
        [InlineData("OUDFRMYMAW", "AWHYFCCMQX", 2)]
        [InlineData("WEWOUCUIDGCGTRMEZEPXZFEJWISRSBBSYXAYDFEJJDLEBVHHKS", "FDAGCXGKCTKWNECHMRXZWMLRYUCOCZHJRRJBOAJOQJZZVUYXIC", 15)]
        public void Scenarios(string s1, string s2, int expected)
        {
            var actual = StringCommonChildSolution.commonChild(s1, s2);

            Assert.Equal(expected, actual);
        }
    }

    /// <summary>
    /// Based on https://en.wikipedia.org/wiki/Longest_common_subsequence_problem
    /// </summary>
    public static class StringCommonChildSolution
    {
        public static int commonChild(string a, string b)
        {
            var C = new int[a.Length + 1, b.Length + 1]; // (a, b).Length + 1
            for (var i = 0; i < a.Length; i++)
                C[i, 0] = 0;
            for (var j = 0; j < b.Length; j++)
                C[0, j] = 0;
            for (var i = 1; i <= a.Length; i++)
            for (var j = 1; j <= b.Length; j++)
            {
                if (a[i - 1] == b[j - 1])//i-1,j-1
                    C[i, j] = C[i - 1, j - 1] + 1;
                else
                    C[i, j] = Math.Max(C[i, j - 1], C[i - 1, j]);
            }
            return C[a.Length, b.Length];
        }
    }
}
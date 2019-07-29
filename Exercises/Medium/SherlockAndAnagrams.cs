using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    /*
    Sherlock and Anagrams
    Two strings are anagrams of each other if the letters of one string can be rearranged to
    form the other string. Given a string, find the number of pairs of substrings of the
    string that are anagrams of each other.

    For example s=mom, the list of all anagrammatic pairs is [m,m], [mo,om] at positions
    [[0],[2]], [[0,1],[1,2]] respectively.

    Function Description
    It must return an integer that represents the number of anagrammatic pairs of substrings
    in s.

    sherlockAndAnagrams has the following parameter(s):
        s: a string .
    Input Format
    The first line contains an integer q, the number of queries.
    Each of the next q lines contains a string s to analyze.

    Constraints
    1 <= q <= 10
    2 <= s <= 100
    String s contains only lowercase letters set=ascii[a-z].

    Output Format
    For each query, return the number of unordered anagrammatic pairs.
    Sample Input 0
        2
        abba
        abcd
    Sample Output 0
        4
        0
    Explanation 0
    The list of all anagrammatic pairs is [a,a], [ab, ba], [b,b] and [abb,bba] at
    positions [[0],[3]], [[0,1],[2,3]], [[1],[2]] and [[0,1,2],[1,2,3]] respectively.

    No anagrammatic pairs exist in the second query as no character repeats.
    */
    public class SherlockAndAnagrams
    {
        [Theory]
        [InlineData("abba", 4)]
        [InlineData("abcd", 0)]
        [InlineData("ifailuhkqq", 3)]
        [InlineData("kkkk", 10)]
        public void Scenarios(string input, int expected)
        {
            var actual = SherlockAndAnagramsSolution.Solve(input);

            Assert.Equal(expected, actual);
        }
    }

    public class SherlockAndAnagramsSolution
    {
        public static int Solve(string s)
        {
            var matches = 0;
            for (int i = 1; i <= s.Length - 1; i++)
            {
                var a = new List<string>();
                for (int j = 0; j <= s.Length - i; j++)
                {
                    a.Add(new string(s.Substring(j, i).OrderBy(c => c).ToArray()));
                }

                var b = (from c in a
                        group c by c into g
                        select new { Key = g.Key, Count = g.Count() })
                        .ToDictionary(g => g.Key, g => g.Count);

                foreach (var n in b.Values)
                {
                    // https://en.wikipedia.org/wiki/Binomial_coefficient
                    // https://en.wikipedia.org/wiki/Combination
                    // n(n - 1)/2 will give us all the possible combinations
                    matches += n * (n - 1) / 2;
                }
            }

            return matches;
        }
    }
}
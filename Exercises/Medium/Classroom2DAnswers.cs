using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    /*
        You are running a classroom and suspect that some of your students are passing around the answer to a multiple-choice question in 2D grids of letters. The word may start anywhere in the grid, and consecutive letters can be either immediately below or immediately to the right of the previous letter.

        Given a grid and a word, write a function that returns the location of the word in the grid as a list of coordinates. If there are multiple matches, return any one.

        grid1 = [
            ['b', 'b', 'b', 'a', 'l', 'l', 'o', 'o'],
            ['b', 'a', 'c', 'c', 'e', 's', 'c', 'n'],
            ['a', 'l', 't', 'e', 'w', 'c', 'e', 'w'],
            ['a', 'l', 'o', 's', 's', 'e', 'c', 'c'],
            ['w', 'o', 'o', 'w', 'a', 'c', 'a', 'w'],
            ['i', 'b', 'w', 'o', 'w', 'w', 'o', 'w']
        ]
        word1_1 = "access"      # [(1, 1), (1, 2), (1, 3), (2, 3), (3, 3), (3, 4)]

        The word must be continuous int he grid, and each subsequent (letter following the previous) letter, must be below or to the right of the previous letter

        Complexity analysis variables:

        r = number of rows
        c = number of columns
        w = length of the word

        time -> O(r*c*w)
     */
    public sealed class Classroom2DAnswers
    {
        [Fact]
        public void Test()
        {
            List<(char[][], string, (int, int)[])> testCases = new()
            {
                (new char[][]
                {
                    new[] { 'b', 'b', 'b', 'a', 'l', 'l', 'o', 'o' },
                    new[] { 'b', 'a', 'c', 'c', 'e', 's', 'c', 'n' },
                    new[] { 'a', 'l', 't', 'e', 'w', 'c', 'e', 'w' },
                    new[] { 'a', 'l', 'o', 's', 's', 'e', 'c', 'c' },
                    new[] { 'w', 'o', 'o', 'w', 'a', 'c', 'a', 'w' },
                    new[] { 'i', 'b', 'w', 'o', 'w', 'w', 'o', 'w' },
                },
                "access",
                new[] { (1, 1), (1, 2), (1, 3), (2, 3), (3, 3), (3, 4) }),
                (new char[][]
                {
                    new[] { 'b', 'b', 'b', 'a', 'l', 'l', 'o', 'o' },
                    new[] { 'b', 'a', 'c', 'c', 'e', 's', 'c', 'n' },
                    new[] { 'a', 'l', 't', 'e', 'w', 'c', 'e', 'w' },
                    new[] { 'a', 'l', 'o', 's', 's', 'e', 'c', 'c' },
                    new[] { 'w', 'o', 'o', 'w', 'a', 'c', 'a', 'w' },
                    new[] { 'i', 'b', 'w', 'o', 'w', 'w', 'o', 'w' },
                },
                "balloon",
                new[] { (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (1, 7) }),
                (new char[][]
                {
                    new[] { 'b', 'b', 'b', 'a', 'l', 'l', 'o', 'o' },
                    new[] { 'b', 'a', 'c', 'c', 'e', 's', 'c', 'n' },
                    new[] { 'a', 'l', 't', 'e', 'w', 'c', 'e', 'w' },
                    new[] { 'a', 'l', 'o', 's', 's', 'e', 'c', 'c' },
                    new[] { 'w', 'o', 'o', 'w', 'a', 'c', 'a', 'w' },
                    new[] { 'i', 'b', 'w', 'o', 'w', 'w', 'o', 'w' },
                },
                "wow",
                new[] { (4, 3), (5, 3), (5, 4) }),
                (new char[][]
                {
                    new[] { 'b', 'b', 'b', 'a', 'l', 'l', 'o', 'o' },
                    new[] { 'b', 'a', 'c', 'c', 'e', 's', 'c', 'n' },
                    new[] { 'a', 'l', 't', 'e', 'w', 'c', 'e', 'w' },
                    new[] { 'a', 'l', 'o', 's', 's', 'e', 'c', 'c' },
                    new[] { 'w', 'o', 'o', 'w', 'a', 'c', 'a', 'w' },
                    new[] { 'i', 'b', 'w', 'o', 'w', 'w', 'o', 'w' },
                },
                "sec",
                new[] { (3, 4), (3, 5), (3, 6) }),
                (new char[][]
                {
                    new[] { 'a' }
                },
                "a",
                new[] { (0, 0) }),
                (new char[][]
                {
                    new[] { 'c', 'a' },
                    new[] { 't', 't' },
                    new[] { 'h', 'a' },
                    new[] { 'a', 'c' },
                    new[] { 't', 'g' }
                },
                "cat",
                new[] { (0, 0), (0, 1), (1, 1) }),
                (new char[][]
                {
                    new[] { 'c', 'a' },
                    new[] { 't', 't' },
                    new[] { 'h', 'a' },
                    new[] { 'a', 'c' },
                    new[] { 't', 'g' }
                },
                "hat",
                new[] { (2, 0), (3, 0), (4, 0) }),
                (new char[][]
                {
                    new[] { 'c', 'c', 'x', 't', 'i', 'b' },
                    new[] { 'c', 'a', 't', 'n', 'i', 'i' },
                    new[] { 'a', 'x', 'n', 'x', 'p', 't' },
                    new[] { 't', 'x', 'i', 'x', 't', 't' }
                },
                "catnip",
                new[] { (0, 1), (1, 1), (1, 2), (1, 3), (1, 4), (2, 4) })
            };

            foreach ((char[][] grid, string word, (int, int)[] expected) in testCases)
            {
                (int, int)[] actual = Solve(grid, word);
                Assert.Equal(expected, actual);
            }
        }

        public (int, int)[] Solve(char[][] grid, string word)
        {
            for(int x=0; x<grid.Length; x++)
            {
                for(int y=0; y<grid[0].Length; y++)
                {
                    List<(int, int)> result = Search(grid, word, x, y, 0, new List<(int, int)>());
                    if(result.Count != 0)
                    {
                        return result.ToArray();
                    }
                }
            }

            return Array.Empty<(int, int)>();
        }

        static List<(int, int)> Search(
            char[][] grid,
            string word,
            int x,
            int y,
            int charPos,
            List<(int, int)> acc)
        {
            if(x >= grid.Length || y >= grid[0].Length || charPos >= word.Length || grid[x][y] != word[charPos]) // not found
            {
                return new List<(int, int)>();
            }

            acc.Add((x, y));
            if(charPos == word.Length - 1)
            {
                return acc;
            }

            var newAcc = Search(grid, word, x, y+1, charPos + 1, new List<(int, int)>(acc));
            if (newAcc.Count != 0)
            {
                return newAcc;
            }

            return Search(grid, word, x+1, y, charPos + 1, new List<(int, int)>(acc));
        }
    }
}

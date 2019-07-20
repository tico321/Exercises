﻿using Xunit;

namespace Exercises.Medium
{
    /*
        Little Vitaly loves different algorithms. Today he has invented a
        new algorithm just for you. Vitaly's algorithm works with string s,
        consisting of characters "x" and "y", and uses two following operations at runtime:

        Find two consecutive characters in the string, such that the first of them
        equals "y", and the second one equals "x" and swap them. If there are several
        suitable pairs of characters, we choose the pair of characters that is located
        closer to the beginning of the string.
        Find in the string two consecutive characters, such that the first of them equals
        "x" and the second one equals "y". Remove these characters from the string.
        If there are several suitable pairs of characters, we choose the pair of characters
        that is located closer to the beginning of the string.
        The input for the new algorithm is string s, and the algorithm works as follows:

        If you can apply at least one of the described operations to the string, go to step 2
        of the algorithm. Otherwise, stop executing the algorithm and print the current string.
        If you can apply operation 1, then apply it. Otherwise, apply operation 2.
        After you apply the operation, go to step 1 of the algorithm.
        Now Vitaly wonders, what is going to be printed as the result of the algorithm's work,
        if the input receives string s.

        Input
        The first line contains a non-empty string s.

        It is guaranteed that the string only consists of characters "x" and "y".
        It is guaranteed that the string consists of at most 106 characters.
        It is guaranteed that as the result of the algorithm's execution won't be an empty
        string.

        Output
        In the only line print the string that is printed as the result of the
        algorithm's work, if the input of the algorithm input receives string s.
     */
    public class CodeParsing
    {
        [Theory]
        [InlineData("x", "x")]
        [InlineData("yxyxy", "y")]
        [InlineData("xxxxxy", "xxxx")]
        [InlineData("xyyxyyyyyxxxxxxxyxyxyyxyyxyyxxyxyxyxxxyxxy", "xx")]
        public void Scenarios(string s, string expected)
        {
            var actual = CodeParsingSolution.Solve(s);

            Assert.Equal(expected, actual);
        }
    }
}

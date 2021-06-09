using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{

    // https://www.hackerrank.com/challenges/two-pluses/problem?h_r=next-challenge&h_v=zen&h_r=next-challenge&h_v=zen
    public class EmasPluses
    {
        [Theory]
        [InlineData(
            new string[]
            {
                "GGGGGG",
                "GBBBGB",
                "GGGGGG",
                "GGBBGB",
                "GGGGGG"
            },
            5)]
        [InlineData(
            new string[]
            {
                "BGBBGB",
                "GGGGGG",
                "BGBBGB",
                "GGGGGG",
                "BGBBGB",
                "BGBBGB"
            },
            25)]
        [InlineData(
            new string[]
            {
                "GGGGGGG",
                "BGBBBBG",
                "BGBBBBG",
                "GGGGGGG",
                "GGGGGGG",
                "BGBBBBG"
            },
            5)]
        [InlineData(
            new string[]
            {
                "GBGBGGB",
                "GBGBGGB",
                "GBGBGGB",
                "GGGGGGG",
                "GGGGGGG",
                "GBGBGGB",
                "GBGBGGB"
            },
            45)]
        [InlineData(
            new string[]
            {
                "GGGGGGGG",
                "GBGBGGBG",
                "GBGBGGBG",
                "GGGGGGGG",
                "GBGBGGBG",
                "GGGGGGGG",
                "GBGBGGBG",
                "GGGGGGGG"
            },
            81)]
        [InlineData(
            new string[]
            {
                "BBBBBGGBGG",
                "GGGGGGGGGG",
                "GGGGGGGGGG",
                "BBBBBGGBGG",
                "BBBBBGGBGG",
                "GGGGGGGGGG",
                "BBBBBGGBGG",
                "GGGGGGGGGG",
                "BBBBBGGBGG",
                "GGGGGGGGGG"
            },
            85)]
        [InlineData(
            new string[]
            {
                "BBBGBGBBB",
                "BBBGBGBBB",
                "BBBGBGBBB",
                "GGGGGGGGG",
                "BBBGBGBBB",
                "BBBGBGBBB",
                "GGGGGGGGG",
                "BBBGBGBBB",
                "BBBGBGBBB",
                "BBBGBGBBB"
            },
            81)]
        [InlineData(
            new string[]
            {
                "GGGGGGGGGGGGGG",
                "GGGGGGGGGGGGGG",
                "GGBGBGGGBGBGBG",
                "GGBGBGGGBGBGBG",
                "GGGGGGGGGGGGGG",
                "GGGGGGGGGGGGGG",
                "GGGGGGGGGGGGGG",
                "GGGGGGGGGGGGGG",
                "GGBGBGGGBGBGBG",
                "GGBGBGGGBGBGBG",
                "GGBGBGGGBGBGBG",
                "GGBGBGGGBGBGBG"
            },
            221)]
        public void TestCases(string[] input, int expected)
        {
            var actual = EmasPlusesSolution.twoPluses(input.ToList());

            Assert.Equal(expected, actual);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Exercises.Medium
{
    /// <summary>
    /// The problem “Text Justification” states that you are given a list s[ ] of type string of size n and an integer
    /// size. Justify the text such that each line of text consists of size number of characters.
    /// You can use space(‘ ‘) as a character to complete the required number of characters in a line.
    ///
    /// Ex: s = {"TutorialCup", "is", "the", "best", "portal", "for", "programming."} and size = 12
    /// Result:
    ///
    /// 1 2 3 4 5 6 7 8 9 10 11 12
    ///
    /// T u t o r i a l C  u  p
    /// i s     t h e   b  e  s  t
    /// p o r t a l        f  o  r
    /// p r o g r a m m i n g .
    /// </summary>
    public sealed class TextJustification
    {
        [Fact]
        public void WhenAllWordsFit()
        {
            string[] input = new[] { "TutorialCup", "is", "the", "best", "portal", "for", "programming." };
            int size = 12;

            string expected =
                "TutorialCup \n" +
                "is  the best\n" +
                "portal   for\n" +
                "programming.\n";

            string actual = Solve(input, size);

            Assert.Equal(expected, actual, StringComparer.Ordinal);
        }

        [Fact]
        public void WithWordLongerThanSize()
        {
            string[] input = new[] { "TutorialCup", "is", "the", "best", "portal", "for", "programming." };
            int size = 10;

            string expected =
                "TutorialCu\n" +
                "p   is the\n" +
                "best      \n" +
                "portal for\n" +
                "programmin\n" +
                "g.        \n";

            string actual = Solve(input, size);

            Assert.Equal(expected, actual, StringComparer.Ordinal);
        }

        private static string Solve(string[] input, int size)
        {
            StringBuilder result = new();
            List<string> remaining = new List<string>(input).SelectMany(word => SplitWord(word, size)).ToList();
            while (remaining.Count > 0)
            {
                remaining = AppendLine(remaining, size, result);
            }

            return result.ToString();
        }

        private static List<string> SplitWord(string word, int size)
        {
            List<string> result = new();
            while (!string.IsNullOrEmpty(word))
            {
                if (word.Length <= size)
                {
                    result.Add(word);
                    word = null;
                }
                else
                {
                    result.Add(word.Substring(0, size));
                    word = word.Substring(size);
                }
            }

            return result;
        }

        private static List<string> AppendLine(List<string> remaining, int size, StringBuilder result)
        {
            int wordsToTake = 1;
            int remainingSize = size - remaining[0].Length;
            for (int i = 1; i < remaining.Count && remainingSize > 0; i++)
            {
                remainingSize -= remaining[i].Length + 1; // plus 1 space in between
                if (remainingSize >= 0)
                {
                    wordsToTake++;
                }
            }

            result.Append(remaining[0]);
            if (wordsToTake > 1)
            {
                string end = string.Join(" ", remaining.Skip(1).Take(wordsToTake - 1));
                result.Append(new String(' ', size - end.Length - remaining[0].Length));
                result.Append(end);
            }
            else
            {
                result.Append(new String(' ', size - remaining[0].Length));
            }

            result.Append("\n");

            return remaining.Skip(wordsToTake).ToList();
        }
    }
}

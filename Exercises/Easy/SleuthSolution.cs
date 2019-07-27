using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Easy
{
    public class SleuthSolution
    {
        public static string Solve(string question)
        {
            var vowels = new HashSet<char>(
                new List<char> { 'A', 'E', 'I', 'O', 'U', 'Y', 'a', 'e', 'i', 'o', 'u', 'y' });
            var consonants = new HashSet<char>(
                new List<char>
                {
                    'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z',
                    'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z'
                });
            var lastChar = question
                .Reverse()
                .SkipWhile(c => !vowels.Contains(c) && !consonants.Contains(c))
                .First();

            return vowels.Contains(lastChar) ? "YES" : "NO";
        }
    }
}

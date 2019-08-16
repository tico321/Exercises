using System;
using System.Linq;

namespace Exercises.Easy
{
    public class SherlockAndTheValidStringSolution
    {
        public static string Solve(string s)
        {
            var charCount = s
                .GroupBy(i => i)
                .ToDictionary(g => g.Key, g => g.Count());
            var firstCount = charCount.First().Value;
            var distinct = charCount
                .Where(c => c.Value != firstCount)
                .ToList();
            if(distinct.Count == 0) return "YES";
            if(distinct.Count == 1)
            {
                var element = distinct.First();
                if(element.Value == 1 ||
                    Math.Abs(element.Value - firstCount) == 1) return "YES";
            }

            return "NO";
        }
    }
}
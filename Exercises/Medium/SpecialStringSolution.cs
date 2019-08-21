using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exercises.Medium
{
    public static class SpecialStringSolution
    {
        public static long Solve(int n, string s)
        {
            var count = s.Length; //count every char

            //count the ones with the middle one different
            var exceptMiddleOne = new Regex("(([a-z])\\2*)(?!\\1)(?=[a-z]\\1)"); //matches a_a and also aa_aa
            count += exceptMiddleOne.Matches(s).Select(m => m.Groups[0].Length).Sum();

            //count repeated chars
            var repeated = new Regex("([a-z])\\1+");
            count += repeated.Matches(s)
                .Select(m => m.Groups[0].Length - 1)
                .Select(l =>
                    // triangular number given by (n^2+n)/2 // https://en.wikipedia.org/wiki/Triangular_number
                    (l * l + l) / 2)
                .Sum();

            return count;
        }

        public static long Solve2(int n, string s)
        {
            s += " ";
            var charCount = new List<(char, long)>();
            var count = 1L;
            var ch = s[0];
            for(var i = 1; i <= n; i++)
            {
                if(ch == s[i])  count++;
                else
                {
                    charCount.Add((ch, count));
                    count = 1;
                    ch = s[i];
                }
            }
            count = 0;
            if(charCount.Count >= 3)
            {
                using (var itr = charCount.GetEnumerator())
                {
                    itr.MoveNext();
                    var (currKey, currCount) = ((char,int))itr.Current;
                    itr.MoveNext();
                    var (nextKey, nextCount) = ((char,int))itr.Current;
                    count = (currCount * (currCount + 1)) / 2;
                    for(var i = 1; i < charCount.Count - 1; i++)
                    {
                        var (prevKey, prevCount) = (currKey, currCount);
                        (currKey, currCount) = (nextKey, nextCount);
                        itr.MoveNext();
                        (nextKey, nextCount) = ((char,int))itr.Current;
                        count += (currCount * (currCount + 1)) / 2;
                        if (prevKey == nextKey && currCount == 1)
                        {
                            count += prevCount > nextCount ? nextCount : prevCount;
                        }
                    }
                    count += (nextCount * (nextCount + 1)) / 2;
                }

            }
            else
            {
                count += charCount.Sum((curr) => (curr.Item2 * (curr.Item2 + 1)) / 2);
            }

            return count;
        }
    }
}
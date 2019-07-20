using System.Collections.Generic;

namespace Exercises.Medium
{
    public class CountTripletsSolution
    {
        public long Solve(long[] arr, long r)
        {
            var tripletCount = 0L;
            var jDictionary = new Dictionary<long, long>();
            var kDictionary = new Dictionary<long, long>();
            foreach (var k in arr)
            {
                if (kDictionary.ContainsKey(k))
                {
                    tripletCount += kDictionary[k];
                }
                if (jDictionary.ContainsKey(k))
                {
                    if (kDictionary.ContainsKey(k * r))
                    {
                        kDictionary[k * r] = kDictionary[k * r] + jDictionary[k];
                    }
                    else
                    {
                        kDictionary[k * r] = jDictionary[k];
                    }
                }
                if (jDictionary.ContainsKey(k * r))
                {
                    jDictionary[k * r] = jDictionary[k * r] + 1;
                }
                else
                {
                    jDictionary[k * r] = 1;
                }
            }

            return tripletCount;
        }
    }
}

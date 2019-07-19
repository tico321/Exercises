using System.Collections.Generic;
using System.Linq;

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

        private List<long> GetNextPositions(
            Dictionary<long, List<long>> arrPosDictionary,
            long currentValuePos,
            long next)
        {
            var result = new List<long>();
            if (!arrPosDictionary.ContainsKey(next)) return result;
            var nextPositions = arrPosDictionary[next].ToList();
            foreach (var nextPos in nextPositions)
            {
                if (nextPos < currentValuePos) continue; //it's not progressive
                result.Add(nextPos);
            }

            return result;
        }

        private void AddToDictionary(
            Dictionary<long, List<long>> arrPos,
            long value,
            long i)
        {
            if (arrPos.ContainsKey(value))
            {
                arrPos[value].Add(i);
            }
            else
            {
                arrPos[value] = new List<long> { i };
            }
        }
    }
}

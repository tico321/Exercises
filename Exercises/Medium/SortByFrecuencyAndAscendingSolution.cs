using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public class SortByFrecuencyAndAscendingSolution
    {
        public List<int> Solve(List<int> input)
        {
            var frecuencyMap = GetFrecuencyMap(input);
            var groupedFrecuencies = GroupByFrecuencies(frecuencyMap);
            return GetSortedByFrecuencyAndOrder(groupedFrecuencies);
        }

        private Dictionary<int, int> GetFrecuencyMap(List<int> arr)
        {
            var frecuencyMap = new Dictionary<int, int>();
            arr.ForEach(i =>
            {
                if (frecuencyMap.ContainsKey(i))
                {
                    frecuencyMap[i] = frecuencyMap[i] + 1;
                }
                else
                {
                    frecuencyMap.Add(i, 1);
                }
            });

            return frecuencyMap;
        }

        private Dictionary<int, List<int>> GroupByFrecuencies(Dictionary<int, int> frecuencies)
        {
            var grouped = new Dictionary<int, List<int>>();
            foreach (var pair in frecuencies)
            {
                var i = pair.Key;
                var frecuency = pair.Value;
                if (!grouped.ContainsKey(frecuency))
                {
                    var group = new List<int>();
                    grouped.Add(frecuency, group);
                }
                grouped[frecuency].Add(i);
            }

            return grouped;
        }

        private List<int> GetSortedByFrecuencyAndOrder(Dictionary<int, List<int>> groupedFrecuencies)
        {
            var result = new List<int>();
            var sortedFrecuencies = GetSortedFrecuencies(groupedFrecuencies);
            foreach (var frecuency in sortedFrecuencies)
            {
                var singleValues = groupedFrecuencies[frecuency];
                singleValues.Sort();
                foreach (var value in singleValues)
                {
                    var repeatedFrecuencyTimes = Enumerable.Repeat(value, frecuency);
                    result.AddRange(repeatedFrecuencyTimes);
                }
            }

            return result;
        }

        private List<int> GetSortedFrecuencies(Dictionary<int, List<int>> groupedFrecuencies)
        {
            var frecuenciesList = new List<int>();
            foreach (var pair in groupedFrecuencies)
            {
                frecuenciesList.Add(pair.Key);
            }
            frecuenciesList.Sort();

            return frecuenciesList;
        }
    }
}

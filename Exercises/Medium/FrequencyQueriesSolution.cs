using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class FrequencyQueriesSolution
    {
        public static List<int> Solve(List<List<int>> queries)
        {
            var valueCountArr = new Dictionary<int, int>();
            var countValueArr = new Dictionary<int, List<int>>();
            var result = new List<int>();
            foreach(var q in queries)
            {
                var instruction = q[0];
                var value = q[1];
                if(instruction == 1) //insert
                {
                    if(valueCountArr.ContainsKey(value))
                    {
                        countValueArr[valueCountArr[value]].Remove(value);
                        if(!countValueArr[valueCountArr[value]].Any()) countValueArr.Remove(valueCountArr[value]);
                        valueCountArr[value]++;
                        if(countValueArr.ContainsKey(valueCountArr[value])) countValueArr[valueCountArr[value]].Add(value);
                        else countValueArr.Add(valueCountArr[value], new List<int>{ value });
                    }
                    else
                    {
                        valueCountArr.Add(value, 1);
                        if(countValueArr.ContainsKey(1)) countValueArr[1].Add(value);
                        else countValueArr.Add(1, new List<int>{ value });
                    }
                }
                else if(instruction == 2) //remove
                {
                    if(!valueCountArr.ContainsKey(value)) continue;
                    countValueArr[valueCountArr[value]].Remove(value);
                    if(!countValueArr[valueCountArr[value]].Any()) countValueArr.Remove(valueCountArr[value]);
                    if(valueCountArr[value] > 1)
                    {
                        valueCountArr[value]--;
                        if(countValueArr.ContainsKey(valueCountArr[value])) countValueArr[valueCountArr[value]].Add(value);
                        else countValueArr.Add(valueCountArr[value], new List<int>{ value });
                    }
                    else
                    {
                        valueCountArr.Remove(value);
                    }
                }
                else //frequency match exactly 1 else 0
                {
                    result.Add(countValueArr.ContainsKey(value) ? 1 : 0);
                }
            }

            return result;
        }
    }
}
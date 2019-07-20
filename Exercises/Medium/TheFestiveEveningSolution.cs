using System.Collections.Generic;

namespace Exercises.Medium
{
    public class TheFestiveEveningSolution
    {
        public static string Solve(string input1, string guests)
        {
            var input1Args = input1.Split(' ');
            var numberOfGuests = int.Parse(input1Args[0]);
            var numberOfGuards = int.Parse(input1Args[1]);

            return WasADoorUnwarded(numberOfGuards, guests);
        }

        private static string WasADoorUnwarded(int numberOfGuards, string guests)
        {
            var lastPositionMap = GetLastPositionsMap(guests);
            var guardsPositions = new HashSet<char>();
            var nextAvailableGuard = numberOfGuards;
            for (var i = 0; i < guests.Length; i++)
            {
                var currentGuest = guests[i];
                if (guardsPositions.Contains(currentGuest))
                {
                    if (lastPositionMap[currentGuest] == i)
                    {
                        nextAvailableGuard++;
                        guardsPositions.Remove(currentGuest);
                    }
                    continue;
                }
                if (nextAvailableGuard == 0) return "YES";
                if (!(lastPositionMap[currentGuest] == i))
                {
                    nextAvailableGuard--;
                    guardsPositions.Add(currentGuest);
                }
            }

            return "NO";
        }

        private static Dictionary<char, int> GetLastPositionsMap(string guests)
        {
            var lastPositionMap = new Dictionary<char, int>();
            for (var i = 0; i < guests.Length; i++)
            {
                if (lastPositionMap.ContainsKey(guests[i]))
                {
                    lastPositionMap[guests[i]] = i;
                }
                else
                {
                    lastPositionMap.Add(guests[i], i);
                }
            }

            return lastPositionMap;
        }
    }
}

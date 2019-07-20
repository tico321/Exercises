using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class BusVideoSystemSolution
    {
        public static int Solve(string input1, string input2)
        {
            var input1Args = input1.Split(' ');
            var input2Args = input2.Split(' ');

            var numberOfBusStops = int.Parse(input1Args[0]);
            var busCapacity = int.Parse(input1Args[1]);
            var stops = input2Args.Select(a => int.Parse(a));

            return GetWaysPassengersWereInitially(numberOfBusStops, busCapacity, stops);
        }

        private static int GetWaysPassengersWereInitially(
            int numberOfBusStops,
            int busCapacity,
            IEnumerable<int> stops)
        {
            var possibleXs = new List<int>();
            var reverseStops = stops.Reverse();
            var lastStop = reverseStops.First();

            if (lastStop > busCapacity)
            {
                return 0;
            }

            if (lastStop < 0) //people left
            {
                //maximum people remaining can be busCapacity-stop, and minimum 0
                var max = busCapacity + lastStop + 1; //we add one because range starts in 0
                possibleXs = Enumerable.Range(0, max) //this range represents ys
                    .Select(y => y - lastStop) //we found xs values with x = y -a
                    .ToList();
            }
            else //people entered
            {
                //maximum people initially could be busCapacity-stop, and minimum 0
                var max = busCapacity - lastStop + 1; //we add one because range starts in 0
                possibleXs = Enumerable.Range(0, max).ToList();
            }

            var remainingStops = reverseStops.Skip(1);
            foreach (var stop in remainingStops)
            {
                //invalid scenarios
                if (stop > 0 && stop > busCapacity) return 0;
                if (stop < 0 && (stop * -1) > busCapacity) return 0;

                //our initial values are the last values of the previous iteration
                //we find the new initial values
                possibleXs = possibleXs
                    .Select(y => y - stop)
                    .Where(x => x >= 0 && x <= busCapacity)
                    .ToList();
            }

            return possibleXs.Count;
        }
    }
}

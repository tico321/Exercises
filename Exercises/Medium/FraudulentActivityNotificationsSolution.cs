using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Medium
{
    public class FraudulentActivityNotificationsSolution
    {
        public static int NaiveSolve(int[] expenditure, int d)
        {
            var n = expenditure.Length;
            var transactionData = expenditure.Take(d).ToArray();

            var notifications = 0;
            var dMid = d / 2;
            for (var i = 0; i < n - d; i++)
            {
                Array.Sort(transactionData);
                var median = (d % 2 == 0) ?
                    (decimal)(transactionData[dMid] + transactionData[dMid - 1]) / 2 :
                    (decimal)transactionData[dMid];

                notifications += expenditure[i + d] >= median * 2 ? 1 : 0;
                transactionData[0] = expenditure[i + d];
            }

            return notifications;
        }

        // To solve the problem we are going to use counting sort https://www.geeksforgeeks.org/counting-sort/
        public static int Solve(int[] expenditure, int d)
        {
            //expenditure max value is 200 that's why we need an array of 201 elements
            var count = Enumerable.Repeat(0, 201).ToArray();
            // first we store the count of each unique value for the trailing days
            // first step of counting sort (but note that we only fill for the trailing days window)
            for (var i = 0; i < d; i++)
            {
                count[expenditure[i]]++;
            }

            var notifications = 0;
            for (var i = d; i < expenditure.Length; i++)
            {
                // second step of counting sort is to ge the frecuency array
                var countFrecuency = GetFrecuencyArray(count);
                //we use the current frecuency array to get the median*2
                var twiceMedian = GetTwiceMedian(expenditure, countFrecuency, d);
                if (expenditure[i] >= twiceMedian) notifications++;
                //finally we update cound
                count[expenditure[i]]++; //increment count of new element in the trailing days window
                count[expenditure[i - d]]--; //take out the element that is out of the window
            }

            return notifications;
        }

        private static List<int> GetFrecuencyArray(int[] count)
        {
            var countFrecuency = new List<int>(count);
            for (var i = 1; i < count.Length; i++)
            {
                countFrecuency[i] += countFrecuency[i - 1];
            }

            return countFrecuency;
        }

        private static int GetTwiceMedian(int[] expenditure, List<int> countFrecuency, int d)
        {
            var twiceMedian = 0;
            if (d % 2 == 0) //if even we need to take the average of the middle numbers
            {
                var mid = d / 2;
                var i = 0;
                for (; i < 201; i++)
                {
                    //find the first number
                    if (mid <= countFrecuency[i])
                    {
                        twiceMedian += i;
                        break;
                    }
                }
                mid += 1;
                for (; i < 201; i++)
                {
                    //keep iterating to find the second number
                    if (mid <= countFrecuency[i])
                    {
                        twiceMedian += i;
                        break;
                    }
                }

            }
            else //if odd we only need the middle number
            {
                var mid = (d + 1) / 2;
                for (var i = 0; i < 201; i++)
                {
                    //find the middle
                    if (mid <= countFrecuency[i])
                    {
                        twiceMedian += i * 2;
                        break;
                    }
                }
            }

            return twiceMedian;
        }
    }
}

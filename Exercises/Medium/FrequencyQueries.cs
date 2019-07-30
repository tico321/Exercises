using System.Collections.Generic;
using Xunit;

namespace Exercises.Medium
{
    /*
        Frequency Queries

        You are given  queries. Each query is of the form two integers described below:
        -  (1, x): Insert x in your data structure.
        -  (2, y): Delete one occurence of y from your data structure, if present.
        -  (3, z): Check if any integer is present whose frequency is exactly z. If yes, print 1 else 0.

        The queries are given in the form of a 2-D array queries of size q where queries[i][0]
        contains the operation, and queries[i][1] contains the data element.
        For example, you are given array queries=[[1,1],[2,2],[3,2],[1,1],[1,1],[2,1],[3,2]].
        The results of each operation are:
        operation   array     output
        [1,1]       [1]
        [2,2]       [1]
        [3,2]                   0
        [1,1]       [1,1]
        [1,1]       [1,1,1]
        [2,1]       [1,1]
        [3,2]                   1

     */
    public class FrequencyQueries
    {
        [Fact]
        public void Scenarios()
        {
            var testMatrix = new List<(List<List<int>>, List<int>)>
            {
                (
                    new List<List<int>>
                    {
                        new List<int>{ 1, 5 },
                        new List<int>{1,6},
                        new List<int>{3,2},
                        new List<int>{1,10},
                        new List<int>{1,10},
                        new List<int>{1,6},
                        new List<int>{2,5},
                        new List<int>{3,2}
                    },
                    new List<int> {0, 1}
                ),
                (
                    new List<List<int>>
                    {
                        new List<int> {3,4},
                        new List<int> {2,1003},
                        new List<int> {1, 16},
                        new List<int> {3,1}

                    },
                    new List<int>{ 0, 1 }
                ),
                (
                    new List<List<int>>
                    {
                        new List<int> {1,3},
                        new List<int> {2,3},
                        new List<int> {3, 2},
                        new List<int> {1,4},
                        new List<int> {1,5},
                        new List<int> {1,5},
                        new List<int> {1,4},
                        new List<int> {3,2},
                        new List<int> {2,4},
                        new List<int> {3,2}
                    },
                    new List<int>{ 0, 1, 1 }
                )
            };
            foreach(var (queries, expected) in testMatrix)
            {
                 var actual = FrequencyQueriesSolution.Solve(queries);

                Assert.Equal(expected, actual);
            }
        }
    }
}
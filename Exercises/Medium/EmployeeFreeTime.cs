using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    /// <summary>
    /// We are given a list schedule of employees, which represents the working time for each employee.
    /// Each employee has a list of non-overlapping Intervals, and these intervals are in sorted order.
    /// Return the list of finite intervals representing the common, positive-length free time for all employees,
    /// also in sorted order.
    ///
    /// Ex:
    /// schedule = [[[1, 2], [5, 6]], [[1, 3]], [[4, 10]]]
    /// output = [[3, 4]]
    /// There are a total of three employees, and all common free time intervals would be [-inf, 1], [3, 4], [10, inf].
    /// We discard any intervals that contain inf as they aren’t finite.
    /// </summary>
    public sealed class EmployeeFreeTime
    {
        [Fact]
        public void Test()
        {
            List<(int[][][], int[][])> testCases = new()
            {
                (
                    new[]
                    {
                        new[] { new[] { 1, 2 }, new[] { 5, 6 } },
                        new[] { new[] { 1, 3 }, new[] { 4, 10 } },
                    },
                    new[] { new[] { 3, 4 } }
                ),
                (
                    new[]
                    {
                        new[] { new[] { 1, 2 }, new[] { 5, 6 }, new[] { 7, 8 } },
                        new[] { new[] { 1, 3 }, new[] { 4, 10 } },
                    },
                    new[] { new[] { 3, 4 } }
                ),
                (
                    new[]
                    {
                        new[] { new[] { 1, 2 }, new[] { 3, 5 }, new[] { 7, 8 } },
                        new[] { new[] { 1, 6 }, new[] { 8, 10 } },
                    },
                    new[] { new[] { 6, 7 } }
                ),
                (
                    new[]
                    {
                        new[] { new[] { 1, 2 }, new[] { 3, 5 } },
                        new[] { new[] { 1, 6 }, new[] { 8, 10 } },
                    },
                    new int[][]{}
                )
            };

            foreach ((int[][][] schedule, int[][] expected) in testCases)
            {
                int[][] actual = Solve(schedule);

                Assert.Equal(expected, actual);
            }
        }

        private int[][] Solve(int[][][] schedule)
        {
            List<(int, int)> freeIntervals = new();
            bool isFirst = true;
            foreach (int[][] personSchedule in schedule)
            {
                for(int i = 0; i < personSchedule.Length - 1; i++)
                {
                    (int currentStart, int currentEnd) = (personSchedule[i][0], personSchedule[i][1]);
                    (int nextStart, int nextEnd) = (personSchedule[i+1][0], personSchedule[i+1][1]);
                    (int, int) freeInterval = (currentEnd, nextStart);
                    freeIntervals = GetFreeIntervals(freeIntervals, freeInterval, isFirst);
                }

                isFirst = false;
            }

            return freeIntervals.Select(i => new int[] { i.Item1, i.Item2 }).ToArray();
        }

        private List<(int, int)> GetFreeIntervals(List<(int, int)> freeIntervals, (int, int) freeInterval, bool isFirst)
        {
            if (isFirst)
            {
                freeIntervals.Add(freeInterval);
                return freeIntervals;
            }

            List<(int, int)> newIntervals = new();
            foreach ((int commonStart, int commonEnd) in freeIntervals)
            {
                (int currentPersonStart, int currentPersonEnd) = freeInterval;
                if (currentPersonStart >= commonStart && currentPersonStart <= commonEnd) // we are on the same interval
                {
                    int newStart = Math.Max(currentPersonStart, commonStart);
                    int newEnd = Math.Min(currentPersonEnd, commonEnd);
                    if (newStart < newEnd)
                    {
                        newIntervals.Add((newStart, newEnd));
                    }
                }
            }

            return newIntervals;
        }
    }
}

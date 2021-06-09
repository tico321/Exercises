using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// The Fibonacci sequence is a sequence of numbers such that any number, except for the first and second,
    /// is the sum of the previous two: 0, 1, 1, 2, 3, 5, 8, 13, 21...
    ///
    /// he value of the first Fibonacci number in the sequence is 0. The value of the fourth Fibonacci number is 2.
    /// It follows that to get the value of any Fibonacci number, n, in the sequence, one can use the formula:
    /// fib(n) = fib(n - 1) + fib(n - 2)
    /// </summary>
    public class L1_1_Fibonacci
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        // Fib creates two branch per call so it grows exponentially and
        // takes to much to resolve for large numbers
        // [InlineData(40, 102334155)]
        public void TestSuiteRecursiveFib(int n, int expected)
        {
            var actual = Fib(n);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        // a solution to the recursive problem is to use memoization
        [InlineData(40, 102334155)]
        public void TestSuiteMemo(int n, int expected)
        {
            var actual = Memo(Fib)(n);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        // another solution is a plain old iterative approach
        [InlineData(40, 102334155)]
        public void TestSuiteIter(int n, int expected)
        {
            var actual = FibIter(n);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 13)]
        [InlineData(40, 102334155)]
        // another intersting solution is to create a stream that generates the numbers
        public void TestSuiteStream(int n, int expected)
        {
            var actual = new FibStream()
                .Skip(n-1)
                .Take(1)
                .First();
            Assert.Equal(expected, actual);
        }

        public int Fib(int n)
        {
            if (n < 0) throw new Exception("Fib.invalid.n");
            if (n == 0) return 0;
            if (n == 1) return 1;
            return Fib(n - 1) + Fib(n - 2);
        }

        public Func<int, int> Memo(Func<int, int> original)
        {
            var map = new Dictionary<int, int>();
            Func<int, int> originalMemoized = (n) =>
            {
                if (map.ContainsKey(n)) return map[n];
                var calculatedRes = original(n);
                map.Add(n, calculatedRes);
                return calculatedRes;
            };
            return originalMemoized;
        }

        // The iterative solution works forward instead of backwards like the recursive solution.
        public int FibIter(int n)
        {
            var fib0 = 0;
            var fib1 = 1;
            for (int i = 0; i < n; i++)
            {
                var aux = fib0;
                fib0 = fib1;
                fib1 = aux + fib1;
            }

            return fib0;
        }

        public class FibStream : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                return new FibEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        public class FibEnumerator : IEnumerator<int>
        {
            private int Fib0 { get; set; } = 0;
            private int Fib1 { get; set; } = 1;

            public bool MoveNext()
            {
                var aux = Fib0;
                Fib0 = Fib1;
                Fib1 = aux + Fib1;
                return true;
            }

            public void Reset()
            {
                Fib0 = 0;
                Fib1 = 1;
            }

            public int Current => Fib0;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}
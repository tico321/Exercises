using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercises.Easy
{
    public class KnightsOfAPolygonalTableSolution
    {
        public static string Solve(string input1, string input2, string input3)
        {
            //number of nights
            var n = int.Parse(input1.Split(' ')[0]);
            //max nights to kill
            var k = byte.Parse(input1.Split(' ')[1]);

            var powers = input2.Split(' ').Select(ch => int.Parse(ch));
            var coins = input3.Split(' ').Select(ch => int.Parse(ch));

            var position = 0;
            var knights = powers
                .Zip(coins, (p, c) => new Knight { Coins = c, Power = p, Position = position++ })
                .ToArray();
            Array.Sort(knights, (a, b) => a.Power.CompareTo(b.Power));

            var solution = new long[n];
            var kAcumulatedCoins = 0L;
            var kNights = new MinBinaryHeap();
            foreach (var knight in knights)
            {
                kAcumulatedCoins += knight.Coins;
                solution[knight.Position] = kAcumulatedCoins;
                if (k == 0)
                {
                    kAcumulatedCoins = 0;
                }
                else if (kNights.HeapSize < k)
                {
                    kNights.Add(knight.Coins);
                }
                else
                {
                    if (kNights.Min() < knight.Coins)
                    {
                        kAcumulatedCoins -= kNights.GetMin();
                        kNights.Add(knight.Coins);
                    }
                    else
                    {
                        kAcumulatedCoins -= knight.Coins;
                    }
                }
            }

            var strBuilder = new StringBuilder();
            for (int i = 0; i < n; i++) strBuilder.Append($"{solution[i]} ");
            return strBuilder.ToString();
        }

        public struct Knight
        {
            public int Power;
            public int Coins;
            public int Position;
        }

        public class MinBinaryHeap
        {
            private readonly List<int> _list;

            public MinBinaryHeap()
            {
                _list = new List<int>();
            }

            public int HeapSize
            {
                get { return _list.Count; }
            }

            public void Add(int value)
            {
                _list.Add(value);
                int i = HeapSize - 1;
                int parent = (i - 1) / 2;

                while (i > 0 && _list[parent] > _list[i])
                {
                    int temp = _list[i];
                    _list[i] = _list[parent];
                    _list[parent] = temp;

                    i = parent;
                    parent = (i - 1) / 2;
                }
            }

            public void Heapify(int i)
            {
                for (; ; )
                {
                    int leftChild = 2 * i + 1;
                    int rightChild = 2 * i + 2;
                    int largestChild = i;

                    if (leftChild < HeapSize && _list[leftChild] < _list[largestChild])
                    {
                        largestChild = leftChild;
                    }

                    if (rightChild < HeapSize && _list[rightChild] < _list[largestChild])
                    {
                        largestChild = rightChild;
                    }

                    if (largestChild == i)
                    {
                        break;
                    }

                    int temp = _list[i];
                    _list[i] = _list[largestChild];
                    _list[largestChild] = temp;
                    i = largestChild;
                }
            }

            public int Min()
            {
                return _list[0];
            }

            public int GetMin()
            {
                int result = _list[0];
                _list[0] = _list[HeapSize - 1];
                _list.RemoveAt(HeapSize - 1);
                Heapify(0);
                return result;
            }
        }
    }
}

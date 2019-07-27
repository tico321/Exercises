using System.Collections.Generic;

namespace Exercises.DataStructures
{
    /// <summary>
    /// Min-heap (priority queue)
    /// https://en.wikipedia.org/wiki/Heap_%28data_structure%29
    /// https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
    /// </summary>
    public class PriorityQueue
    {
        List<int> data = new List<int>();

        public void Add(int val)
        {
            data.Add(val);
            int i = data.Count - 1;
            while (i > 0 && data[Parent(i)] > data[i])
            {
                int t = data[Parent(i)];
                data[Parent(i)] = data[i];
                data[i] = t;
                i = Parent(i);
            }
        }

        public int Peek()
        {
            return data[0];
        }

        public int Pop()
        {
            int r = data[0];

            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);

            int i = 0;
            while (Left(i) < data.Count)
            {
                int smallchild = Left(i);
                if (Right(i) < data.Count && data[Right(i)] < data[smallchild])
                    smallchild = Right(i);
                if (data[smallchild] < data[i])
                {
                    int t = data[smallchild];
                    data[smallchild] = data[i];
                    data[i] = t;
                    i = smallchild;
                }
                else
                    break;
            }

            return r;
        }

        private int Parent(int n) => (n - 1) / 2;

        private int Left(int n) => n * 2 + 1;

        private int Right(int n) => n * 2 + 2;
    }
}

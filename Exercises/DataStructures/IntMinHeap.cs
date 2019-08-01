using System.Collections.Generic;

namespace Exercises.DataStructures
{
    /// <summary>
    /// Min-heap (priority queue)
    /// https://en.wikipedia.org/wiki/Heap_%28data_structure%29
    /// https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
    /// </summary>
    public class IntMinHeap
    {
        private readonly List<int> pairs = new List<int>();

        public int Length { get => this.pairs.Count; }

        public void Add(int val)
        {
            this.pairs.Add(val);
            this.BubbleUp(this.pairs.Count - 1);
        }

        public int Peek()
        {
            return this.pairs[0];
        }

        public int Pop()
        {
            int result = this.pairs[0];

            this.pairs[0] = this.pairs[this.pairs.Count - 1];
            this.pairs.RemoveAt(this.pairs.Count - 1);

            this.PushDown(0);

            return result;
        }

        private int Parent(int n) => (n - 1) / 2;

        private int Left(int n) => n * 2 + 1;

        private int Right(int n) => n * 2 + 2;

        private void BubbleUp(int idx)
        {
            var parentIdx = Parent(idx);
            while (idx > 0 && this.pairs[parentIdx] > this.pairs[idx])
            {
                this.Swap(parentIdx, idx);
                idx = parentIdx;
                parentIdx = Parent(idx);
            }
        }

        private void PushDown(int idx)
        {
            while (Left(idx) < this.pairs.Count)
            {
                int smallchildIdx = Left(idx);
                if (Right(idx) < this.pairs.Count && this.pairs[Right(idx)] < this.pairs[smallchildIdx])
                    smallchildIdx = Right(idx);

                if (this.pairs[smallchildIdx] >= this.pairs[idx]) break;

                this.Swap(smallchildIdx, idx);
                idx = smallchildIdx;
            }
        }

        private void Swap(int a, int b)
        {
            int temp = this.pairs[a];
            this.pairs[a] = this.pairs[b];
            this.pairs[b] = temp;
        }
    }
}

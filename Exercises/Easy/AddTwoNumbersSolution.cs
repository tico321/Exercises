using System.Collections.Generic;

namespace Exercises.Easy
{
    public static class AddTwoNumbersSolution
    {
        // this solution will scale linearly with at most max(listA, listB) iterations
        // so we consider it's O(n)
        public static LinkedList<int> Solve(LinkedList<int> listA, LinkedList<int> listB)
        {
            var result = new LinkedList<int>();
            var pA = listA.First;
            var pB = listB.First;
            var carry = 0;
            while (pA != null || pB != null)
            {
                var sum =
                    (pA?.Value ?? 0) +
                    (pB?.Value ?? 0) +
                    carry;
                carry = sum < 10 ? 0 : 1;
                sum = carry > 0 ? sum - 10 : sum;
                if (result.Count == 0) result.AddFirst(sum);
                else result.AddLast(new LinkedListNode<int>(sum));
                pA = pA?.Next;
                pB = pB?.Next;
            }

            if (carry > 0)
            {
                result.AddLast(new LinkedListNode<int>(carry));
            }

            return new LinkedList<int>(result);
        }
    }
}
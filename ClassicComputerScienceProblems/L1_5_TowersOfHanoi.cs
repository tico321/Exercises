using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /*
     * http://www.cs.cmu.edu/~cburch/survey/recurse/hanoi.html
     * Three vertical pegs (henceforth “towers”) stand tall. We will label them A, B, and C. Doughnut-shaped discs
     * are around tower A. The widest disc is at the bottom, and we will call it disc 1. The rest of the discs above
     * disc 1 are labeled with increasing numerals and get progressively narrower. For instance, if we were to work
     * with three discs, the widest disc, the one on the bottom, would be 1. The next widest disc, disc 2, would sit
     * on top of disc 1. And finally, the narrowest disc, disc 3, would sit on top of disc 2. Our goal is to move
     * all of the discs from tower A to tower C given the following constraints:
     *  - Only one disc can be moved at a time.
     *  - The topmost disc of any tower is the only one available for moving.
     *  - A wider disc can never be atop a narrower disc.
     */
    public class L1_5_TowersOfHanoi
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 7)]
        [InlineData(4, 15)]
        public void TestTransitions(int discs, int expected)
        {
            var hannoi = new Hanoi(discs);

            var actual = hannoi.Solve();

            Assert.Equal(expected, actual);
        }

        /*
         * A stack is a data structure that is modeled on the concept of Last-In-First-Out (LIFO).
         * The last thing put into it is the first thing that comes out of it.
         * Stacks are perfect stand-ins for the towers in The Towers of Hanoi.
         * When we want to put a disc onto a tower, we can just push it.
         * When we want to move a disc from one tower to another, we can pop it from the first and push it onto the second.
         */
        public class Hanoi
        {
            private readonly int _discsNumber;
            private readonly Stack<int> _towerA = new();
            private readonly Stack<int> _towerB = new();
            private readonly Stack<int> _towerC = new();

            public Hanoi(int discs)
            {
                _discsNumber = discs;
                Enumerable
                    .Range(1, _discsNumber)
                    .ToList()
                    .ForEach(_towerA.Push);
            }

            public int Solve()
            {
                var moves = new List<string>();
                Move(_towerA, _towerB, _towerC, _discsNumber, moves);
                return moves.Count;
            }

            /*
             * As an example imagine you have 3 discs, the solution will go as follows:
             *      Move last 2 from A to B
             *          - Move from A to C -> (1,2) () (3)
             *          - Move from A to B -> (1) (2) (3)
             *          - Move from C to B -> (1) (2,3) ()
             *      Move last from A to C
             *          - Move from C to A -> () (2,3) (1)
             *      Move last 2 from B to C
             *          - Move from B to A -> (3) (2) (1)
             *          - Move from B to C -> (3) () (1,2)
             *          - Move from A to C -> () () (1,2,3)
             * The recursive case can be inferred as:
             *      1. base case: if n = 1 move to the target
             *      2. Recursive:
             *          2.1 Move the upper n-1 discs from A to B, using C as middle step
             *          2.2 Move the single lowest disc from A to C
             *          2.3 Move the n-1 discs from B to C, using A as a middle step
             */
            private void Move(Stack<int> begin, Stack<int> end, Stack<int> middle, int n, List<string> moves)
            {
                // base case
                if (n == 1)
                {
                    end.Push(begin.Pop());
                    AddToMoves(moves, begin, end);
                }
                else
                {
                    // Move the upper n-1 discs from A to B, using C as middle step
                    Move(begin, middle, end, n - 1, moves);
                    // Move the single lowest disc from A to C
                    Move(begin, end, middle, 1, moves);
                    // Move the n-1 discs from B to C, using A as a middle step
                    Move(middle, end, begin, n - 1, moves);
                }
            }

            private void AddToMoves(List<string> moves, Stack<int> begin, Stack<int> end)
            {
                var from = begin == _towerA ? "A" : begin == _towerB ? "B" : "C";
                var to = end == _towerA ? "A" : begin == _towerB ? "B" : "C";
                moves.Add($"{from}->{to}");
            }
        }
    }
}
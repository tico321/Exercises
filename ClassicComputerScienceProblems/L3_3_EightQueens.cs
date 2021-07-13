using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ClassicComputerScienceProblems.L3_1_ConstraintSatisfactionProblems;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// A chessboard is an eight-by-eight grid of squares. A queen is a chess piece that can move on the chessboard any
    /// number of squares along any row, column, or diagonal. A queen is attacking another piece if, in a single move,
    /// it can move to the square the piece is on without jumping over any other piece.
    /// (In other words, if the other piece is in the line of sight of the queen, then it is attacked by it.)
    /// The eight queens problem poses the question of how eight queens can be placed on a chessboard without any queen
    /// attacking another queen.
    /// </summary>
    public class L3_2_EightQueens
    {
        [Fact]
        public void SolveSuccess()
        {
            var actual = Solve();

            Assert.Equal(8, actual.Count);
        }

        // To represent squares on the chessboard, we will assign each an integer row and an integer column.
        // We can ensure each of the eight queens is not in the same column by simply assigning them sequentially
        // the columns 1 through 8.

        // The variables in our constraint-satisfaction problem can just be the column of the queen in question.
        public static List<int> Variables = new() { 1, 2, 3, 4, 5, 6, 7, 8 };
        // The domains can be the possible rows
        public static IDictionary<int, List<int>> Domain = Variables.ToDictionary(c => c, _ => Variables);

        // Constraint that checks whether any two queens are on the same row or diagonal. (They were all assigned
        // different sequential columns to begin with.) Checking for the same row is trivial, but checking for the same
        // diagonal requires a little bit of math. If any two queens are on the same diagonal, the difference between
        // their rows will be the same as the difference between their columns.
        public class QueensConstraint : Constraint<int, int>
        {
            public QueensConstraint(List<int> variables)
            {
                Variables = variables;
            }
            public List<int> Variables { get; }

            public bool Satisfied(IDictionary<int, int> assignment)
            {
                // q1c = queen 1 column, q1r = queen 1 row
                // q2c = queen 2 column
                foreach(var (q1c, q1r) in assignment) {
                    for (var q2c = q1c + 1; q2c <= Variables.Count; q2c++) {
                        if (assignment.ContainsKey(q2c)) {
                            // q2r = queen 2 row
                            int q2r = assignment[q2c];
                            // same row?
                            if (q1r == q2r) {
                                return false;
                            }
                            // same diagonal?
                            if (Math.Abs(q1r - q2r) == Math.Abs(q1c - q2c)) {
                                return false;
                            }
                        }
                    }
                }
                return true; // no conflict
            }
        }

        public static IDictionary<int, int> Solve()
        {
            var csp = new CSP<int, int>(Variables, Domain);
            csp.AddConstraint(new QueensConstraint(Variables));
            var sol = csp.BacktrackingSearch();
            return sol;
        }
    }
}

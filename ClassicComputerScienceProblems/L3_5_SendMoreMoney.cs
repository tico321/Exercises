using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ClassicComputerScienceProblems.L3_1_ConstraintSatisfactionProblems;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    ///     SEND+MORE=MONEY is a cryptarithmetic puzzle, meaning that it is about finding digits that replace letters to
    ///     make a mathematical statement true. Each letter in the problem represents one digit (0-9). No two letters can
    ///     represent the same digit. When a letter repeats, it means a digit repeats in the solution.
    ///     SEND
    ///     +
    ///     MORE
    ///     =
    ///     MONEY
    /// </summary>
    public class L3_4_SendMoreMoney
    {
        [Fact]
        public void Solve()
        {
            CSP<char, int> csp = new (Variables, Domain);
            csp.AddConstraint(new SendMoreMoneyConstraint(Variables));

            var actual = csp.BacktrackingSearch();

            Assert.NotEmpty(actual);
        }

        // characters are the variables
        public static readonly List<char> Variables = new() { 'S', 'E', 'N', 'D', 'M', 'O', 'R', 'Y' };
        // the domain are the possible digits foreach character
        private static readonly List<int> Digits = Enumerable.Range(0, 10).ToList();
        private static readonly List<int> One = new() {1};
        public static readonly IDictionary<char, List<int>> Domain = Variables
            .ToDictionary(
                v => v,
                v => v == 'M' ?
                    // because of how our constraint is implemented we predefine the solution for M with 1
                    One :
                    Digits);

        public class SendMoreMoneyConstraint : Constraint<char, int>
        {
            public SendMoreMoneyConstraint(List<char> variables)
            {
                Variables = variables;
            }

            public List<char> Variables { get; }

            public bool Satisfied(IDictionary<char, int> assignment)
            {
                // We return true so a partial continues to be worked on.
                if (assignment.Count != Variables.Count) return true;

                // if there are duplicates is not a solution
                if (new HashSet<int>(assignment.Values).Count < Variables.Count) return false;

                // // if all variables have been assigned, check if it adds correctly
                var s = assignment['S'];
                var e = assignment['E'];
                var n = assignment['N'];
                var d = assignment['D'];
                var m = assignment['M'];
                var o = assignment['O'];
                var r = assignment['R'];
                var y = assignment['Y'];
                var send = s * 10^3 + e * 10^2 + n * 10 + d;
                var more = m * 10^3 + o * 10^2 + r * 10 + e;
                var money = m * 10^4 + o * 10^3 + n * 10^2 + e * 10 + y;
                return send + more == money;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ClassicComputerScienceProblems.L3_1_ConstraintSatisfactionProblems;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Imagine you have a map of Australia that you want to color by state/territory (which we will collectively call
    /// regions). No two adjacent regions should share a color. Can you color the regions with just three different colors?
    /// </summary>
    public class L3_1_AustralianMapColoring
    {
        [Fact]
        public void SolutionSuccess()
        {
            var actual = Solver();

            Assert.Equal(7, actual.Count);
        }

        // To model the problem as a CSP, we need to define the variables, domains, and constraints.
        // The variables are the seven regions of Australia: Western Australia, Northern Territory, South Australia,
        // Queensland, New South Wales, Victoria, and Tasmania.
        public List<string> Variables => new()
        {
            "Western Australia", "Northern Territory", "South Australia", "Queensland", "New South Wales",
            "Victoria", "Tasmania"
        };
        // The domain of each variable is the three different colors that can possibly be assigned (red, green and blue).
        public List<string> Colors => new() { "red", "green", "blue" };
        public IDictionary<string, List<string>> Domains => Variables.ToDictionary(v => v, _ => Colors);

        // No two adjacent regions can be colored with the same color, so our constraints will be dependent on which
        // regions border one another. We can use what are called binary constraints (constraints between two variables).
        public class MapColoringConstraint : Constraint<string, string>
        {
            private readonly string _place1;
            private readonly string _place2;

            public MapColoringConstraint(string place1, string place2)
            {
                _place1 = place1;
                _place2 = place2;
                Variables = new List<string> { place1, place2 };
            }
            public List<string> Variables { get; }

            public bool Satisfied(IDictionary<string, string> assignment)
            {
                // if either place is not in the assignment, then it is not yet possible for their colors to be conflicting
                if (!assignment.ContainsKey(_place1) ||
                    !assignment.ContainsKey(_place2)) {
                    return true;
                }
                // check the color assigned to place1 is not the same as the color assigned to place2
                return assignment[_place1] != assignment[_place2];
            }
        }

        // Fill variables domains and constraints for CSP
        public IDictionary<string, string> Solver()
        {
            var csp = new CSP<String, String>(Variables, Domains);
            csp.AddConstraint(new MapColoringConstraint("Western Australia", "Northern Territory"));
            csp.AddConstraint(new MapColoringConstraint("Western Australia", "South Australia"));
            csp.AddConstraint(new MapColoringConstraint("South Australia", "Northern Territory"));
            csp.AddConstraint(new MapColoringConstraint("Queensland", "Northern Territory"));
            csp.AddConstraint(new MapColoringConstraint("Queensland", "South Australia"));
            csp.AddConstraint(new MapColoringConstraint("Queensland", "New South Wales"));
            csp.AddConstraint(new MapColoringConstraint("New South Wales", "South Australia"));
            csp.AddConstraint(new MapColoringConstraint("Victoria", "South Australia"));
            csp.AddConstraint(new MapColoringConstraint("Victoria", "New South Wales"));
            csp.AddConstraint(new MapColoringConstraint("Victoria", "Tasmania"));

            return csp.BacktrackingSearch();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Constraint-satisfaction problems (CSPs).
    /// CSPs are composed of variables with possible values that fall into ranges known as domains.
    /// Constraints between the variables must be satisfied in order for constraint-satisfaction problems to be solved.
    /// Those three core concepts--variables, domains, and constraints--are simple to understand, and their generality
    /// underlies the wide applicability of constraint-satisfaction problem solving.
    ///
    /// Programming languages like Prolog and Picat have facilities for solving constraint-satisfaction problems built in.
    /// The usual technique in other languages is to build a framework that incorporates a backtracking search and
    /// several heuristics to improve the performance of that search.
    /// </summary>
    public class L3_1_ConstraintSatisfactionProblems
    {
        // Constraints will be defined as subclasses of a Constraint class.
        // Each Constraint consists of the variables it constrains and a method that checks whether it is satisfied().
        public interface Constraint<V, D>
        {
            List<V> Variables { get; }
            bool Satisfied(IDictionary<V, D> assignment);
        }
        // The centerpiece of our constraint-satisfaction framework will be a class called CSP.
        // CSP is the gathering point for variables, domains, and constraints.
        public class CSP<V, D>
        {
            // The variables collection is a List of variables
            private readonly List<V> _variables;

            // Domains is a Map mapping variables to lists of possible values (the domains of those variables)
            private readonly IDictionary<V, List<D>> _domains;

            // Constraints is a Map that maps each variable to a List of the constraints imposed on it.
            private readonly IDictionary<V, List<Constraint<V, D>>> _constraints = new Dictionary<V, List<Constraint<V, D>>>();

            public CSP(List<V> variables, IDictionary<V, List<D>> domains)
            {
                _variables = variables;
                _domains = domains;
                foreach (var variable in variables)
                {
                    _constraints.Add(variable, new List<Constraint<V, D>>());
                    if (!domains.ContainsKey(variable))
                    {
                        throw new ArgumentException("Every variable should have a domain assigned to it.");
                    }
                }
            }

            // AddConstraint() method goes through all of the variables touched by a given constraint and
            // adds itself to the constraints mapping for each of them.
            public void AddConstraint(Constraint<V, D> constraint)
            {
                foreach (var variable in constraint.Variables)
                {
                    if (!_variables.Contains(variable))
                    {
                        throw new ArgumentException("Variable in constraint not in CSP");
                    }

                    _constraints[variable].Add(constraint);
                }
            }

            // checks if the constraint is satisfied, given the new assignment.
            // If the assignment satisfies every constraint, true is returned.
            // If any constraint imposed on the variable is not satisfied, false is returned.
            public bool Consistent(
                V variable,
                // an assignment is a specific domain value selected for each variable.
                IDictionary<V, D> assignment)
            {
                return _constraints[variable].All(constraint => constraint.Satisfied(assignment));
            }

            // This constraint-satisfaction framework will use a simple backtracking search to find solutions to problems.
            // Backtracking is the idea that once you hit a wall in your search, you go back to the last known point
            // where you made a decision before the wall, and you choose a different path.

            public IDictionary<V, D> BacktrackingSearch() // helper for backtrackingSearch when nothing known yet
            {
                return BacktrackingSearch(new Dictionary<V, D>());
            }
            public IDictionary<V, D> BacktrackingSearch(IDictionary<V, D> assignment)
            {
                // assignment is complete if every variable is assigned (base case)
                if (assignment.Count == _variables.Count) {
                    return assignment;
                }

                // get the first variable in the CSP but not in the assignment
                var unassigned = _variables.First(v => !assignment.ContainsKey(v));

                // look through every domain value of the first unassigned variable
                foreach(var value in _domains[unassigned]) {
                    // shallow copy of assignment that we can change
                    var localAssignment = new Dictionary<V, D>(assignment);
                    localAssignment.Add(unassigned, value);
                    // if not consistent try with next value
                    if (!Consistent(unassigned, localAssignment)) continue;

                    // if we're still consistent, we recurse (continue)
                    var result = BacktrackingSearch(localAssignment);
                    // if we found the result, we end up backtracking
                    if (result != null) return result;
                }

                return null; // we didn't find the result
            }
        }
    }
}
using Priority_Queue;
using System;
using System.Collections.Generic;

namespace ClassicComputerScienceProblems
{
    // DFS, BFS and A* search implementations.
    public class L2_2_2_GenericSearch
    {
        /*
         * DFS. A search that goes as deeply as it can before backtracking to its last decision point if it reaches a dead end.
         *
         * The depth-first search algorithm relies on a data structure known as a stack.
         * A stack is a data structure that operates under the Last-In-First-Out (LIFO) principle.
         * The last item put into a stack must be the next item removed from a stack.
         *
         * An in-progress depth-first search needs to keep track of two data structures:
         *     - the stack of states (or “places”) that we are considering searching, which we will call the frontier
         *     - the set of states that we have already searched, which we will call explored.
         * As long as there are more states to visit in the frontier, DFS will keep checking whether they are the goal
         * and adding their successors to the frontier. It will also mark each state that has already been searched as explored,
         * so that the search does not get caught in a circle, reaching states that have prior visited states as successors.
         * If the frontier is empty, it means there is nowhere left to search.
         *
            x			G
            5	x
            4	6	x
            3	7	8
            2
            1
            S

         */
        public static Node<T> DeepFirstSearch<T>(T initial, Func<T, bool> goalTest, Func<T, List<T>> successors)
        {
            // frontier is where we have to go
            var frontier = new Stack<Node<T>>();
            frontier.Push(new Node<T>(initial, null));
            // explored is where we have been
            var explored = new HashSet<T> { initial };

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Pop();
                var currentState = currentNode.State;
                if (goalTest(currentState)) return currentNode; // we are done
                // check where we can go next
                foreach (var child in successors(currentState))
                {
                    if (explored.Contains(child)) continue;
                    explored.Add(child);
                    frontier.Push(new Node<T>(child, currentNode));
                }
            }

            return null; // went through everything and didn't find a goal
        }

        /*
         * Breadth-first search (BFS) always finds the shortest path by systematically looking one layer of nodes
         * farther away from the start state in each iteration of the search.
         *
         * To implement BFS, a queue data structure is required.
         * A BFS expands like an onion each layer bigger than the previous.
            x			G
            	x
            		x
            6
            3	7
            1	4	8
            S	2	5

            the algorithm for a breadth-first search is identical to the algorithm for a depth-first search,
            with the frontier changed from a stack to a queue.
         */
        public static Node<T> BreadthFirstSearch<T>(T initial, Func<T, bool> goalTest, Func<T, List<T>> successors)
        {
            // frontier is where we have to go
            var frontier = new Queue<Node<T>>();
            frontier.Enqueue(new Node<T>(initial, null));
            // explored is where we have been
            var explored = new HashSet<T> { initial };

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                var currentState = currentNode.State;
                if (goalTest(currentState)) return currentNode; // we are done
                // check where we can go next
                foreach (var child in successors(currentState))
                {
                    if (explored.Contains(child)) continue;
                    explored.Add(child);
                    frontier.Enqueue(new Node<T>(child, currentNode));
                }
            }

            return null; // went through everything and didn't find a goal
        }

        /*
         * A* search aims to find the shortest path from start state to goal state.
         * An A* search uses a combination of a cost function and a heuristic function to focus its search on pathways
         * most likely to get to the goal quickly.
         *
         * Cost. The cost function, g(n), examines the cost to get to a particular state.
         *
         * Heuristic. The heuristic function, h(n), gives an estimate of the cost to get from the state in question to the goal state.
         * It can be proved that if h(n) is an admissible heuristic, the final path found will be optimal.
         * An admissible heuristic is one that never overestimates the cost to reach the goal.
         *
         * Total cost. the total cost f(n) is the combination of cost and heuristic. f(n) = g(n) + h(n)
         * A* search picks the one with the lowest f(n). This is how it distinguishes itself from BFS and DFS.
         *
         * Priority queue.
         * To pick the state on the frontier with the lowest f(n), an A* search uses a priority queue as the
         * data structure for its frontier. A priority queue keeps its elements in an internal order,
         * such that the first element popped out is always the highest-priority element.
         * (In our case, the highest-priority item is the one with the lowest f(n).)
         * Usually this means the internal use of a binary heap, which results in O(lg n) pushes and O(lg n) pops.
         *
         * 1. For more information on heuristics, see Stuart Russell and Peter Norvig, Artificial Intelligence:
         * A Modern Approach, 3rd edition (Pearson, 2010), p. 94.
         * 2. For more about heuristics for A* pathfinding, check out the “Heuristics” chapter in Amit Patel’s
         * Amit’s Thoughts on Pathfinding, http://mng.bz/z7O4.
         */
        public static Node<T> AStarSearch<T>(
            T initial,
            Func<T, bool> goalTest,
            Func<T, List<T>> successors,
            Func<T, double> g,
            Func<T, double> h)
        {
            // frontier is where we have to go, for A* is a priority queue
            var frontier = new SimplePriorityQueue<Node<T>, double>();
            var rootNode = new Node<T>(initial, null, 0, h(initial));
            frontier.Enqueue(rootNode, rootNode.TotalCost);

            // explored is where we have been
            // A HashMap will allow us to keep track of the lowest cost (g(n)) of each node we may visit.
            // With the heuristic function now in play, it is possible some nodes may be visited twice if the heuristic
            // is inconsistent. If the node found through the new direction has a lower cost than the prior we prefer the new route.
            var explored = new Dictionary<T, double> { { initial, 0 } };

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                var currentState = currentNode.State;
                if (goalTest(currentState)) return currentNode; // we are done
                // check where we can go next
                foreach (var child in successors(currentState))
                {
                    var newCost = currentNode.Cost + g(currentState);
                    if (explored.ContainsKey(child) && explored[child] < newCost) continue;
                    explored[child] = newCost;
                    var next = new Node<T>(child, currentNode, newCost, h(child));
                    frontier.Enqueue(next, next.TotalCost);
                }
            }

            return null; // went through everything and didn't find a goal
        }

        /*
         * If search is successful, it returns the Node encapsulating the goal state.
         * The path from the start to the goal can be reconstructed by working backward from this Node and its priors
         * using the parent property.
         */
        public static List<T> NodeToPath<T>(Node<T> node)
        {
            var path = new List<T>();
            // work backwards from end to front
            while (node != null)
            {
                path.Add(node.State);
                node = node.Parent;
            }

            path.Reverse();
            return path;
        }

        /*
         * A Node class that we will use to keep track of how we got from one state to another state
         * (or from one place to another place) as we search.
         */
        public class Node<T> : IComparable<Node<T>>
        {
            // for dfs and bfs we won't use cost and heuristic
            public Node(T state, Node<T> parent)
            {
                State = state;
                Parent = parent;
            }

            // A* uses cost and heuristic
            public Node(T state, Node<T> parent, double cost, double heuristic) : this(state, parent)
            {
                Cost = cost;
                Heuristic = heuristic;
                TotalCost = cost + heuristic;
            }

            public T State { get; }
            public Node<T> Parent { get; }
            public double Cost { get; set; }
            public double Heuristic { get; set; }
            public double TotalCost { get; set; }

            // CompareTo maybe needed for the priority queue in A*
            public int CompareTo(Node<T> other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                var mine = Cost + Heuristic;
                var theirs = other.Cost + other.Heuristic;
                return mine.CompareTo(theirs);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L4_1_MapAsAGraph;

namespace ClassicComputerScienceProblems
{
    // Weighted Graph Minimum Spanning Tree
    public class L4_4_WeightedGraphMSP
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L4_4_WeightedGraphMSP(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindingTheMinimumSpanningTree()
        {
            var cities = new List<string>
            {
                "Seattle", "San Francisco", "Los Angeles", "Riverside", "Phoenix", "Chicago", "Boston",
                "New York", "Atlanta", "Miami", "Dallas", "Houston", "Detroit", "Philadelphia", "Washington"
            };
            var cityGraph = new WeightedGraph<string>(cities);
            cityGraph.AddEdge("Seattle", "Chicago", 1737);
            cityGraph.AddEdge("Seattle", "San Francisco", 678);
            cityGraph.AddEdge("San Francisco", "Riverside", 386);
            cityGraph.AddEdge("San Francisco", "Los Angeles", 348);
            cityGraph.AddEdge("Los Angeles", "Riverside", 50);
            cityGraph.AddEdge("Los Angeles", "Phoenix", 357);
            cityGraph.AddEdge("Riverside", "Phoenix", 307);
            cityGraph.AddEdge("Riverside", "Chicago", 1704);
            cityGraph.AddEdge("Phoenix", "Dallas", 887);
            cityGraph.AddEdge("Phoenix", "Houston", 1015);
            cityGraph.AddEdge("Dallas", "Chicago", 805);
            cityGraph.AddEdge("Dallas", "Atlanta", 721);
            cityGraph.AddEdge("Dallas", "Houston", 225);
            cityGraph.AddEdge("Houston", "Atlanta", 702);
            cityGraph.AddEdge("Houston", "Miami", 968);
            cityGraph.AddEdge("Atlanta", "Chicago", 588);
            cityGraph.AddEdge("Atlanta", "Washington", 543);
            cityGraph.AddEdge("Atlanta", "Miami", 604);
            cityGraph.AddEdge("Miami", "Washington", 923);
            cityGraph.AddEdge("Chicago", "Detroit", 238);
            cityGraph.AddEdge("Detroit", "Boston", 613);
            cityGraph.AddEdge("Detroit", "Washington", 396);
            cityGraph.AddEdge("Detroit", "New York", 482);
            cityGraph.AddEdge("Boston", "New York", 190);
            cityGraph.AddEdge("New York", "Philadelphia", 81);
            cityGraph.AddEdge("Philadelphia", "Washington", 123);

            _testOutputHelper.WriteLine(cityGraph.ToString());

            var mst = cityGraph.JarnikMinimumSpanningTree(0);
            var mstPath = cityGraph.PrintWeightedPath(mst);

            _testOutputHelper.WriteLine($"msp {Environment.NewLine}{mstPath}");
        }

        //  The weight of an edge is the distance between the two cities that it connects.
        //  For this a weight is added to each edge (the distance is the weight)
        public record WeightedEdge(
            int U, // the "from" vertex
            int V, // the "to" vertex
            double Weight) : IEdge, IComparable<WeightedEdge>
        {
            // Return an Edge that travels in the opposite direction of the edge it is applied to.
            public WeightedEdge Reversed() => new WeightedEdge(V, U, Weight);
            // compareTo() method is only interested in looking at weights
            // because it needs to find the smallest edge by weight
            public int CompareTo(WeightedEdge other)
            {
                return other == null ? 1 : Weight.CompareTo(other);
            }

            public override string ToString() => $"{U} { Weight } > {V}";
        }

        public class WeightedGraph<V> : Graph<V, WeightedEdge>
        {
            public WeightedGraph(List<V> vertices) : base(vertices)
            {
            }

            // This is an undirected graph, so we always add
            // edges in both directions
            public void AddEdge(WeightedEdge edge) {
                Edges[edge.U].Add(edge);
                Edges[edge.V].Add(edge.Reversed());
            }

            public void AddEdge(int u, int v, double weight) {
                AddEdge(new WeightedEdge(u, v, weight));
            }

            public void AddEdge(V first, V second, double weight) {
                AddEdge(IndexOf(first), IndexOf(second), weight);
            }
            // Make it easy to pretty-print a Graph
            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < VertexCount; i++) {
                    sb.Append(VertexAt(i));
                    sb.Append(" -> ");
                    sb.Append(EdgesOf(i).Select(we => $"({VertexAt(we.V)}, {we.Weight})"));
                    sb.Append(Environment.NewLine);
                }
                return sb.ToString();
            }

            public static double TotalWeight(List<WeightedEdge> path) {
                return path.Select(we => we.Weight).Sum();
            }


            /*
             * *** Minimum spanning tree
             *
             * A tree is a special kind of graph that has one, and only one, path between any two vertices. This implies
             * that there are no cycles in a tree (which is sometimes called acyclic). A cycle can be thought of as a loop:
             * if it is possible to traverse a graph from a starting vertex, never repeat any edges, and get back to the
             * same starting vertex, then it has a cycle. Any connected graph that is not a tree can become a tree by
             * pruning edges.
             *
             * Tree with cycles
             *        B -- D -- E
             *       / \  /
             *      A   C
             * Pruned tree
             *       B -- D -- E
             *      / \
             *     A   C
             *
             * A connected graph is a graph that has some way of getting from any vertex to any other vertex.
             * A spanning tree is a tree that connects every vertex in a graph.
             * A minimum spanning tree is a tree that connects every vertex in a weighted graph with the minimum total weight.
             *
             * The point is that finding a minimum spanning tree is the same as finding a way to connect every vertex in a
             * weighted graph with the minimum weight.
             *
             * *** Jarnik's algorithm (commonly referred to as Prim’s algorithm)
             *
             * For finding a minimum spanning tree works by dividing a graph into two parts: the vertices in the
             * still-being-assembled minimum spanning tree and the vertices not yet in the minimum spanning tree.
             * It takes the following steps:
             * 1. Pick an arbitrary vertex to include in the minimum spanning tree.
             * 2. Find the lowest-weight edge connecting the minimum spanning tree to the vertices not yet in the minimum spanning tree.
             * 3. Add the vertex at the end of that minimum edge to the minimum spanning tree.
             * 4. Repeat steps 2 and 3 until every vertex in the graph is in the minimum spanning tree.
             */
            public List<WeightedEdge> JarnikMinimumSpanningTree(int start)
            {
                // the resulting msp
                var minimumSpanningTree = new List<WeightedEdge>();

                // return empty if invalid
                if (start < 0 || start > (VertexCount - 1)) return minimumSpanningTree;

                // To run Jarník’s algorithm efficiently, a priority queue is used
                // Every time a new vertex is added to the minimum spanning tree, all of its outgoing edges that link to
                // vertices outside the tree are added to the priority queue. The lowest-weight edge is always popped off
                // the priority queue, and the algorithm keeps executing until the priority queue is empty.
                // This ensures that the lowest-weight edges are always added to the tree first
                var pq = new SimplePriorityQueue<WeightedEdge, double>();
                var visited = new bool[VertexCount]; // seen it

                // this is like a "visit" inner function
                Action<int> visit = index =>
                {
                    visited[index] = true; // mark as visited
                    EdgesOf(index)
                        .Where(edge => !visited[edge.V])
                        .ToList()
                        .ForEach(edge => pq.Enqueue(edge, edge.Weight));
                };

                visit(start); // the start vertex is where we begin
                while (pq.Count > 0) { // keep going while there are edges
                    var edge = pq.Dequeue();
                    // Edges that connect to vertices already in the tree are ignored when they are popped.
                    if (visited[edge.V]) {
                        continue; // don't ever revisit
                    }
                    // this is the current smallest, so add it to solution
                    minimumSpanningTree.Add(edge);
                    visit(edge.V); // visit where this connects
                }

                return minimumSpanningTree;
            }

            public string PrintWeightedPath(List<WeightedEdge> wp)
            {
                var sb = new StringBuilder();
                foreach (var (i, v, weight) in wp)
                {
                    var p = $"{VertexAt(i)} {weight} > {VertexAt(v)}";
                    sb.Append(p);
                    sb.Append(Environment.NewLine);
                }
                sb.Append($"Total Weight: {TotalWeight(wp)}");
                return sb.ToString();
            }
        }
    }
}

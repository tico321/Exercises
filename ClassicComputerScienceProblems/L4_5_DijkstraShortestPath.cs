using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L4_4_WeightedGraphMSP;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Dijkstra’s algorithm solves the single-source shortest path problem.
    /// It is provided a starting vertex, and it returns the lowest-weight path to any other vertex on a weighted graph.
    /// It also returns the minimum total weight to every other vertex from the starting vertex.
    ///
    /// Dijkstra’s algorithm starts at the single-source vertex and then continually explores the closest vertices
    /// to the starting vertex. For this reason, Dijkstra’s algorithm is greedy. When Dijkstra’s algorithm encounters
    /// a new vertex, it keeps track of how far it is from the starting vertex and updates this value if it ever
    /// finds a shorter path. It also keeps track of which edge got it to each vertex.
    /// </summary>
    public class L4_5_DijkstraShortestPath
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L4_5_DijkstraShortestPath(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindShortestPathWithDijkstra()
        {
            var cities = new List<string>
            {
                "Seattle", "San Francisco", "Los Angeles", "Riverside", "Phoenix", "Chicago", "Boston",
                "New York", "Atlanta", "Miami", "Dallas", "Houston", "Detroit", "Philadelphia", "Washington"
            };
            var cityGraph = new DijkstraWeightedGraph<string>(cities);
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

            var dijkstraResult = cityGraph.Dijkstra("Los Angeles");
            var nameDistance = cityGraph.DistanceArrayToDistanceMap(dijkstraResult.Distances);

            _testOutputHelper.WriteLine("Distances from Los Angeles:");
            nameDistance
                .ToList()
                .ForEach((keyValuePair) =>
                {
                    var (name, distance) = keyValuePair;
                    _testOutputHelper.WriteLine($"{name}: {distance}");
                });

            _testOutputHelper.WriteLine("");

            _testOutputHelper.WriteLine("Shortest path from Los Angeles to Boston:");
            var path = PathMapToPath(
                cityGraph.IndexOf("Los Angeles"),
                cityGraph.IndexOf("Boston"),
                dijkstraResult.PathMap);
            var res = cityGraph.PrintWeightedPath(path);
            _testOutputHelper.WriteLine(res);
        }


        // DijkstraNode
        // a simple data structure for keeping track of costs associated with each vertex explored so far and for comparing them.
        public record DijkstraNode(int Vertex, double Distance) : IComparable<DijkstraNode>
        {
            public int CompareTo(DijkstraNode other) => Distance.CompareTo(other.Distance);
        }

        // DijkstraResult
        // a class for pairing the distances calculated by the algorithm and the paths calculated by the algorithm.
        public record DijkstraResult(double[] Distances, IDictionary<int, WeightedEdge> PathMap);

        public class DijkstraWeightedGraph<V> : WeightedGraph<V>
        {
            public DijkstraWeightedGraph(List<V> vertices) : base(vertices)
            {
            }

            /*
             * Here are all of the algorithm’s steps:
             * 1. Add the starting vertex to a priority queue.
             * 2. Pop the closest vertex from the priority queue (at the beginning, this is just the starting vertex);
             *    we’ll call it the current vertex.
             * 3. Look at all of the neighbors connected to the current vertex. If they have not previously been recorded,
             *    or if the edge offers a new shortest path to them, then for each of them record its distance from the
             *    start, record the edge that produced this distance, and add the new vertex to the priority queue.
             * 4. Repeat steps 2 and 3 until the priority queue is empty.
             * 5. Return the shortest distance to every vertex from the starting vertex and the path to get to each of them.
             */
            public DijkstraResult Dijkstra(V root)
            {
                var first = IndexOf(root); // find starting index
                var distances = new double[VertexCount]; // distances are unknown at first
                distances[first] = 0; // root's distance to root is 0
                var visited = new bool[VertexCount];
                visited[first] = true;
                // how we got to each vertex
                var pathMap = new Dictionary<int, WeightedEdge>();
                var pq = new SimplePriorityQueue<DijkstraNode, double>();

                // we start with the root vertex
                var firstNode = new DijkstraNode(first, 0);
                pq.Enqueue(firstNode, firstNode.Distance);

                // keep running until the priory queue is empty
                while (pq.Count > 0) {
                    var u = pq.Dequeue().Vertex; // explore next closest vertex we search from
                    // the shortest distance for getting to u along known routes
                    var distU = distances[u]; // should already have been seen
                    // look at every edge/vertex from the vertex in question
                    foreach (var we in EdgesOf(u)) {
                        //  distV is the distance to any known vertex attached by an edge from u.
                        var distV = distances[we.V]; // the old distance to this vertex
                        var pathWeight = we.Weight + distU; // the new distance to this vertex
                        // If shorter path already found continue
                        if (visited[we.V] && (!(distV > pathWeight))) continue;
                        visited[we.V] = true;
                        // update the distance to this vertex
                        distances[we.V] = pathWeight;
                        // update the edge on the shortest path
                        if (!pathMap.ContainsKey(we.V)) pathMap.Add(we.V, we);
                        else pathMap[we.V] = we;
                        // push any vertices that have new paths to them to the priority queue to explore them in the future
                        var node = new DijkstraNode(we.V, pathWeight);
                        pq.Enqueue(node, node.Distance);
                    }
                }

                return new DijkstraResult(distances, pathMap);
            }

            // Helper function to get easier access to dijkstra results
            public IDictionary<V, double> DistanceArrayToDistanceMap(double[] distances)
            {
                var distanceMap = new Dictionary<V, Double>();
                for (var i = 0; i < distances.Length; i++) {
                    distanceMap.Add(VertexAt(i), distances[i]);
                }
                return distanceMap;
            }
        }

        // Takes a map of edges to reach each node and returns a list of
        // edges that goes from *start* to *end*
        public static List<WeightedEdge> PathMapToPath(int start, int end, IDictionary<int, WeightedEdge> pathMap)
        {
            if (pathMap.Count == 0) return new List<WeightedEdge>();

            var path = new List<WeightedEdge>();
            var edge = pathMap[end];
            path.Add(edge);
            while (edge.U != start) {
                edge = pathMap[edge.U];
                path.Add(edge);
            }

            path.Reverse();

            return path;
        }
    }
}

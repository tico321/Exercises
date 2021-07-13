using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L2_2_2_GenericSearch;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Unweighted graphs don't take into consideration tolls from one vertex to another but the fewer stops, the better.
    /// In graph theory, a set of edges that connects two vertices is known as a path.
    /// In other words, a path is a way of getting from one vertex to another vertex.
    ///
    /// BFS Breadth-first-search can be used to search for the shortest path.
    /// </summary>
    public class L4_2_UnweightedGraph
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L4_2_UnweightedGraph(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void FindingTheShortestPathOfUnweightedGraphWithBfs()
        {
            // Represents the 15 largest MSAs in the United States
            var cityGraph = new L4_1_MapAsAGraph.UnweightedGraph<string>(new List<string>
            {
                "Seattle", "San Francisco", "Los Angeles", "Riverside", "Phoenix", "Chicago", "Boston", "New York",
                "Atlanta", "Miami", "Dallas", "Houston", "Detroit", "Philadelphia", "Washington"
            });

            cityGraph.AddEdge("Seattle", "Chicago");
            cityGraph.AddEdge("Seattle", "San Francisco");
            cityGraph.AddEdge("San Francisco", "Riverside");
            cityGraph.AddEdge("San Francisco", "Los Angeles");
            cityGraph.AddEdge("Los Angeles", "Riverside");
            cityGraph.AddEdge("Los Angeles", "Phoenix");
            cityGraph.AddEdge("Riverside", "Phoenix");
            cityGraph.AddEdge("Riverside", "Chicago");
            cityGraph.AddEdge("Phoenix", "Dallas");
            cityGraph.AddEdge("Phoenix", "Houston");
            cityGraph.AddEdge("Dallas", "Chicago");
            cityGraph.AddEdge("Dallas", "Atlanta");
            cityGraph.AddEdge("Dallas", "Houston");
            cityGraph.AddEdge("Houston", "Atlanta");
            cityGraph.AddEdge("Houston", "Miami");
            cityGraph.AddEdge("Atlanta", "Chicago");
            cityGraph.AddEdge("Atlanta", "Washington");
            cityGraph.AddEdge("Atlanta", "Miami");
            cityGraph.AddEdge("Miami", "Washington");
            cityGraph.AddEdge("Chicago", "Detroit");
            cityGraph.AddEdge("Detroit", "Boston");
            cityGraph.AddEdge("Detroit", "Washington");
            cityGraph.AddEdge("Detroit", "New York");
            cityGraph.AddEdge("Boston", "New York");
            cityGraph.AddEdge("New York", "Philadelphia");
            cityGraph.AddEdge("Philadelphia", "Washington");

            _testOutputHelper.WriteLine(cityGraph.ToString());

            var initial = "Boston";
            Func<string, bool> goalTest = city => city == "Miami";
            Func<string, List<string>> succesors = city => cityGraph.NeighborsOf(city).ToList();
            var bfsResult = BreadthFirstSearch(initial, goalTest, succesors);
            var path = NodeToPath(bfsResult);

            _testOutputHelper.WriteLine($"shortest path: [{string.Join(',', path)}]");
        }


    }
}

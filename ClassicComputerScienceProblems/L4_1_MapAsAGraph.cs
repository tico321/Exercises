using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassicComputerScienceProblems
{
    /*
     * A graph is an abstract mathematical construct that is used for modeling a real-world problem by dividing
     * the problem into a set of connected nodes. We call each of the nodes a vertex and each of the connections an edge.
     *
     * For instance, a subway map can be thought of as a graph representing a transportation network. Each of the dots
     * represents a station, and each of the lines represents a route between two stations. In graph terminology,
     * we would call the stations “vertices” and the routes “edges.”
     *
     * There are two types of graphs:
     * - Unweighted graphs. An unweighted edge is simply a connection between two vertices.
     * - Weighted graphs, associate a weight (a comparable value, usually numeric) with each edge. Ex. distances.
     */
    public class L4_1_MapAsAGraph
    {
        // exercise is to construct a reduced graph between the main metropolitan areas in the united states.
        // We want this graph framework to be as flexible as possible so that it can represent as many different problems
        // as possible. To achieve this goal, we will use generics to abstract away the type of the vertices.
        // Every vertex will ultimately be assigned an integer index, but it will be stored as the user-defined generic type.

        // An Edge is defined as a connection between two vertices, each of which is represented by an integer index.
        // By convention, u is used to refer to the first vertex, and v is used to represent the second vertex.
        // This Edge represents undirected graphs (graphs with edges that allow travel in both directions),
        // but in directed graphs, also known as digraphs, edges can also be one-way.
        public interface IEdge
        {
            public int U { get; }
            public int V { get; }
        }

        public record Edge(
            int U, // the "from" vertex
            int V) // the "to" vertex
            : IEdge
        {
            // Return an Edge that travels in the opposite direction of the edge it is applied to.
            public Edge Reversed() => new Edge(V, U);
            public override string ToString() => U + " -> " + V;
        }

        // The Graph abstract class focuses on the essential role of a graph: associating vertices with edges.
        // There are many ways to implement a graph data structure, but the two most common are:
        // - A vertex matrix. Each cell of the matrix represents the intersection of two vertices in the graph, and
        // the value of that cell indicates the connection (or lack thereof) between them
        // - Adjacency lists. Every vertex has a list of vertices that it is connected to.
        public abstract class Graph<
            V, // V is the type of the vertices in the Graph
            E> where E : IEdge // E is the type of the edges
        {
            // Each vertex will be stored in the list, but we will later refer to them by their integer index in the list.
            // V can be a complex type but the index will always be an int.
            private List<V> Vertices = new();
            protected List<List<E>> Edges = new();

            public Graph() { }

            public Graph(List<V> vertices)
            {
                Vertices = vertices;
                Vertices.ForEach(v => Edges.Add(new List<E>()));
            }

            public int VertexCount => Vertices.Count;
            public int EdgeCount => Edges.Select(e => e.Count).Sum();
            public V VertexAt(int index) => Vertices[index];
            public int IndexOf(V vertex) => Vertices.LastIndexOf(vertex);
            // Add a vertex to the graph and return its index
            public int AddVertex(V vertex)
            {
                Vertices.Add(vertex);
                Edges.Add(new List<E>());
                return VertexCount - 1;
            }
            // Find the vertices that a vertex at some index is connected to (the neighbors of the vertex)
            // A vertex’s neighbors are all of the other vertices that are directly connected to it by an edge.
            public IReadOnlyList<V> NeighborsOf(int index) =>
                Edges[index] // The adjacency list of edges through which the vertex in question is connected to other vertices.
                // edge.V represents the index of the neighbor that points to the vertex
                // edge.U represents the index of the vertex
                .Select(edge => VertexAt(edge.V))
                .ToList();

            // Look up a vertex's index and find its neighbors (convenience method)
            public IReadOnlyList<V> NeighborsOf(V vertex) => NeighborsOf(IndexOf(vertex));
            public IReadOnlyList<E> EdgesOf(int index) => Edges[index];
            // Look up the index of a vertex and return its edges (convenience method)
            public IReadOnlyList<E> EdgesOf(V vertex) => Edges[IndexOf(vertex)];

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < VertexCount; i++) {
                    sb.Append(VertexAt(i));
                    sb.Append(" -> ");
                    var u = string.Join(",", NeighborsOf(i));
                    sb.Append($"[{u}]");
                    sb.Append(Environment.NewLine);
                }
                return sb.ToString();
            }
        }

        public class UnweightedGraph<V> : Graph<V, Edge>
        {
            public UnweightedGraph(List<V> vertices) : base(vertices)
            {
            }

            // This is an undirected graph, so we always add edges in both directions
            public void AddEdge(Edge edge) {
                //  Adds an edge to the adjacency list of the “from” vertex (u)
                Edges[edge.U].Add(edge);
                // Add a reversed version of the edge to the adjacency list of the “to” vertex (v)
                Edges[edge.V].Add(edge.Reversed());
            }
            // Add an edge using vertex indices (convenience method)
            public void AddEdge(int u, int v) => AddEdge(new Edge(u, v));
            // Add an edge by looking up vertex indices (convenience method)
            public void AddEdge(V first, V second)
            {
                var u = IndexOf(first);
                var v = IndexOf(second);
                AddEdge(new Edge(u, v));
            }
        }
    }
}

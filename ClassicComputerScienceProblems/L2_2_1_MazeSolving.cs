using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L2_2_2_GenericSearch;

namespace ClassicComputerScienceProblems
{
    /*
     * This exercise explores DFS, BFS and A* search.
     *
     * Find the path through a maze to illustrate breadth-first, depth-first and A* algorithms.
     */
    public class L2_2_MazeSolving
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L2_2_MazeSolving(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void MazeConstructor()
        {
            var maze = new Maze();

            var actual = maze.ToString();

            Assert.StartsWith("S", actual);
            Assert.EndsWith("G", actual.Replace(Environment.NewLine, ""));
            Assert.Contains("X", actual);
        }

        [Fact]
        public void DeepFirstSearchSuccess()
        {
            var maze = new Maze();

            maze.SolveWithDeepFirstSearch();
            var actual = maze.ToString();

            Assert.Contains("P", actual);
        }

        [Fact]
        public void BreadthFirstSearchSuccess()
        {
            var maze = new Maze();

            maze.SolveWithBreadthFirstSearch();
            var actual = maze.ToString();

            Assert.Contains("P", actual);
        }

        [Fact]
        public void AStarSearchSuccess()
        {
            var maze = new Maze();

            maze.SolveWithAStarSearch();
            var actual = maze.ToString();

            _testOutputHelper.WriteLine(actual);
        }

        [Fact]
        public void CompareSolutions()
        {
            var maze = new Maze();

            maze.SolveWithDeepFirstSearch();
            var dfs = maze.ToString();
            _testOutputHelper.WriteLine("dfs");
            _testOutputHelper.WriteLine(dfs);
            maze.ClearPath();

            maze.SolveWithBreadthFirstSearch();
            var bfs = maze.ToString();
            _testOutputHelper.WriteLine("bfs");
            _testOutputHelper.WriteLine(bfs);
            maze.ClearPath();

            maze.SolveWithAStarSearch();
            var aStar = maze.ToString();
            _testOutputHelper.WriteLine("aStar");
            _testOutputHelper.WriteLine(aStar);
        }

        /*
         * The maze is a two-dimensional grid of Cells. A Cell is an enum that knows how to turn itself into a String.
         * For example, " " will represent an empty space, and "X" will represent a blocked space.
         *
         * The maze that is generated should be fairly sparse so that there is almost always a path
         * from a given starting location to a given goal location.
         */
        public class Maze
        {
            private readonly int _columns;
            private readonly MazeLocation _goal;
            private readonly int _rows;
            private readonly double _sparseness;
            private readonly MazeLocation _start;
            private readonly Cell[,] _grid;

            public Maze() : this(10, 10, new MazeLocation(0, 0), new MazeLocation(9, 9), 0.2)
            {
            }

            public Maze(int rows, int columns, MazeLocation start, MazeLocation goal, double sparseness)
            {
                _rows = rows;
                _columns = columns;
                _start = start;
                _goal = goal;
                _sparseness = sparseness;
                _grid = new Cell[_rows, _columns];
                for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _columns; j++)
                    _grid[i, j] = Cell.Empty;
                RandomFill(_sparseness);
                _grid[_start.Row, _start.Column] = Cell.Start;
                _grid[_goal.Row, _goal.Column] = Cell.Goal;
            }

            private void RandomFill(double sparseness)
            {
                var random = new Random();
                for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _columns; j++)
                    if (random.NextDouble() < sparseness)
                        _grid[i, j] = Cell.Blocked;
            }

            public bool Goal(MazeLocation ml) => ml == _goal;

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < _rows; i++)
                {
                    for (var j = 0; j < _columns; j++) sb.Append(_grid[i, j]);
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }

            /*
             * How can we move within our mazes? Let’s say that we can move horizontally and vertically one space
             * at a time from a given space in the maze. Using these criteria, a successors() function can find the
             * possible next locations from a given MazeLocation.
             *
             * successors() simply checks above, below, to the right, and to the left of a MazeLocation in a Maze
             * to see if it can find empty spaces that can be gone to from that location. It also avoids checking
             * locations beyond the edges of the Maze.
             */
            public List<MazeLocation> Successors(MazeLocation ml)
            {
                var locations = new List<MazeLocation>();
                var (row, column) = ml;
                if (row + 1 < _rows && _grid[row + 1, column] != Cell.Blocked)
                    locations.Add(new MazeLocation(row + 1, column));
                if (row - 1 >= 0 && _grid[row - 1, column] != Cell.Blocked)
                    locations.Add(new MazeLocation(row - 1, column));
                if (column + 1 < _columns && _grid[row, column + 1] != Cell.Blocked)
                    locations.Add(new MazeLocation(row, column + 1));
                if (column - 1 >= 0 && _grid[row, column - 1] != Cell.Blocked)
                    locations.Add(new MazeLocation(row, column - 1));
                return locations;
            }

            /*
             * euclideanDistance() is a function that takes a maze location and returns its straight-line distance to the goal.
             * The shortest path between two points is a straight line. It makes sense, then, that a straight-line
             * heuristic will always be admissible for the maze-solving problem. The Euclidean distance, derived from
             * the Pythagorean theorem, states that distance = √((difference in x)2 + (difference in y)2).
             * For our mazes, the difference in x is equivalent to the difference in columns between two maze locations,
             * and the difference in y is equivalent to the difference in rows.
             */
            public double EuclideanDistance(MazeLocation ml)
            {
                var (row, column) = ml;
                var xDist = column - _goal.Column;
                var yDist = row - _goal.Row;
                return Math.Sqrt(xDist * xDist + yDist * yDist);
            }

            /*
             * Euclidean distance is great, but for our particular problem (you can move only in one of four directions)
             * we can do even better. The Manhattan distance is derived from navigating the streets of Manhattan,
             * the most famous of New York City’s boroughs, which is laid out in a grid pattern.
             * To get from anywhere to anywhere in Manhattan, one needs to walk a certain number of horizontal blocks
             * and a certain number of vertical blocks.
             *
             * The Manhattan distance is derived by simply finding the difference in rows between two maze locations
             * and summing it with the difference in columns.
             */
            public double ManhattanDistance(MazeLocation ml)
            {
                var (row, column) = ml;
                var xDist = Math.Abs(column - _goal.Column);
                var yDist = Math.Abs(row - _goal.Row);
                return xDist + yDist;
            }

            //  We just consider every hop in our maze to be a cost of 1.
            public double MoveCost(MazeLocation ml) => 1;

            public void SolveWithDeepFirstSearch()
            {
                var node = DeepFirstSearch(_start, Goal, Successors);
                if (node == null) return;
                var path = NodeToPath(node);
                Mark(path);
            }

            public void SolveWithBreadthFirstSearch()
            {
                var node = BreadthFirstSearch(_start, Goal, Successors);
                if (node == null) return;
                var path = NodeToPath(node);
                Mark(path);
            }

            public void SolveWithAStarSearch()
            {
                var node = AStarSearch(_start, Goal, Successors, ManhattanDistance, MoveCost);
                if (node == null) return;
                var path = NodeToPath(node);
                Mark(path);
            }

            public void Mark(List<MazeLocation> path)
            {
                foreach (var ml in path) _grid[ml.Row, ml.Column] = Cell.Path;
                _grid[_start.Row, _start.Column] = Cell.Start;
                _grid[_goal.Row, _goal.Column] = Cell.Goal;
            }

            public void ClearPath()
            {
                for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _columns; j++)
                    if (_grid[i, j] == Cell.Path)
                        _grid[i, j] = Cell.Empty;

                _grid[_start.Row, _start.Column] = Cell.Start;
                _grid[_goal.Row, _goal.Column] = Cell.Goal;
            }
        }

        public record MazeLocation(int Row, int Column);

        public class Cell : SmartEnum<Cell, string>
        {
            public static Cell Empty = new(nameof(Empty), " ");
            public static Cell Blocked = new(nameof(Blocked), "X");
            public static Cell Start = new(nameof(Start), "S");
            public static Cell Goal = new(nameof(Goal), "G");
            public static Cell Path = new(nameof(Path), "P");

            private Cell(string name, string value) : base(name, value)
            {
            }

            public override string ToString() => Value;
        }
    }
}

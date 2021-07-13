using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static ClassicComputerScienceProblems.L3_1_ConstraintSatisfactionProblems;

namespace ClassicComputerScienceProblems
{
    /*
     * A word search is a grid of letters with hidden words placed along rows, columns, and diagonals.
     * A player of a word-search puzzle attempts to find the hidden words by carefully scanning through the grid.
     * Finding places to put the words so that they all fit on the grid is a kind of constraint-satisfaction problem.
     * The variables are the words, and the domains are the possible locations of those words.
     * For the purposes of expediency, our word search will not include words that overlap
     */
    public class L3_4_WordSearch
    {
        [Fact]
        public void Solution()
        {
            var grid = new WordGrid(9, 9);
            var words = new List<string>{ "MATTHEW", "JOE", "MARY", "SARAH", "SALLY" };
            // generate domains for all words
            var domains = words.ToDictionary(word => word, word => grid.GenerateDomain(word));
            var csp = new CSP<String, List<GridLocation>>(words, domains);
            csp.AddConstraint(new WordSearchConstraint(words));

            var solution = csp.BacktrackingSearch();
        }

        public record GridLocation(int Row, int Column);

        public class WordGrid
        {
            private readonly int AlphabetLength = 26;
            private readonly int LetterA = Convert.ToInt16('A');
            private readonly int _rows;
            private readonly int _columns;
            private char[,] _grid;

            public WordGrid(int rows, int columns)
            {
                _rows = rows;
                _columns = columns;
                _grid = new char[rows, columns];
                var random = new Random();
                // we will fill the grid with random letters of the English alphabet (A-Z).
                for (var row = 0; row<_rows; row++)
                for (var col = 0; col < _columns; col++)
                {
                    var randomLetter = Convert.ToChar(random.Next(LetterA, AlphabetLength + LetterA));
                    _grid[row, col] = randomLetter;
                }
            }

            public void Mark(string word, List<GridLocation> locations) {
                for (var i = 0; i < word.Length; i++)
                {
                    var (row, column) = locations[i];
                    _grid[row, column] = word[i];
                }
            }

            public string ToString() {
                var sb = new StringBuilder();
                for (var i = 0; i < _rows; i++)
                {
                    for(var j=0; j<_columns; j++)
                    {
                        sb.Append(_grid[i, j]);
                    }
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }

            /*
             * To figure out where words can fit in the grid, we will generate their domains. The domain of a word is a
             * list of lists of the possible locations of all of its letters (List<List<GridLocation>>).
             * Words cannot go just anywhere, though. They must stay within a row, column, or diagonal that is within
             * the bounds of the grid. In other words, they should not go off the end of the grid. The purpose of
             * generateDomain() and its helper “fill” methods is to build these lists for every word.
             */
            public List<List<GridLocation>> GenerateDomain(String word)
            {
                var domain = new List<List<GridLocation>>();
                int length = word.Length;

                for (int row = 0; row < _rows; row++) {
                    for (int column = 0; column < _columns; column++) {
                        if (column + length <= _columns) {
                            // left to right
                            FillRight(domain, row, column, length);
                            // diagonal towards bottom right
                            if (row + length <= _rows) {
                                FillDiagonalRight(domain, row, column, length);
                            }
                        }
                        if (row + length <= _rows) {
                            // top to bottom
                            FillDown(domain, row, column, length);
                            // diagonal towards bottom left
                            if (column - length >= 0) {
                                FillDiagonalLeft(domain, row, column, length);
                            }
                        }
                    }
                }
                return domain;
            }
            private void FillRight(List<List<GridLocation>> domain, int row, int column, int length)
            {
                var locations = new List<GridLocation>();
                for (var c = column; c < (column + length); c++) {
                    locations.Add(new GridLocation(row, c));
                }
                domain.Add(locations);
            }

            private void FillDiagonalRight(List<List<GridLocation>> domain, int row, int column, int length) {
                var locations = new List<GridLocation>();
                var r = row;
                for (var c = column; c < (column + length); c++) {
                    locations.Add(new GridLocation(r, c));
                    r++;
                }
                domain.Add(locations);
            }

            private void FillDown(List<List<GridLocation>> domain, int row, int column, int length) {
                var locations = new List<GridLocation>();
                for (var r = row; r < (row + length); r++) {
                    locations.Add(new GridLocation(r, column));
                }
                domain.Add(locations);
            }

            private void FillDiagonalLeft(List<List<GridLocation>> domain, int row, int column, int length)
            {
                var locations = new List<GridLocation>();
                var c = column;
                for (var r = row; r < (row + length); r++)
                {
                    locations.Add(new GridLocation(r, c));
                    c--;
                }
                domain.Add(locations);
            }
        }

        /*
         * checks whether any of the locations proposed for one word are the same as a location proposed for another
         * word. It does this using a Set. Converting a List into a Set will remove all duplicates. If there are fewer
         * items in a Set converted from a List than there were in the original List, that means the original List
         * contained some duplicates.
         */
        public class WordSearchConstraint : Constraint<string, List<GridLocation>>
        {
            public List<string> Variables { get; }

            public WordSearchConstraint(List<string> variables)
            {
                Variables = variables;
            }
            public bool Satisfied(IDictionary<string, List<GridLocation>> assignment)
            {
                // combine all GridLocations into one giant List
                var allLocations = assignment.SelectMany(a => a.Value).ToArray();
                // a set will eliminate duplicates using equals()
                var allLocationsSet = new HashSet<GridLocation>(allLocations);
                // if there are any duplicate grid locations then there is an overlap
                return allLocations.Length == allLocationsSet.Count;
            }
        }
    }
}

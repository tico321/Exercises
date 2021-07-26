using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L5_1_2_GeneticAlgorithms;

namespace ClassicComputerScienceProblems
{
    // If you are compressing a list of elements the order of the elements will affect the compression ratio.
    // The order that will maximize the compression will depend in the compression algorithm used.
    public class L5_5_OptimizingListCompression
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L5_5_OptimizingListCompression(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact(Skip = "long running")]
        public void OptimizeListCompression()
        {
            var testList = new List<string>
            {
                "Michael", "Sarah", "Joshua", "Narine", "David", "Sajid", "Melanie", "Daniel",
                "Wei", "Dean", "Brian", "Murat", "Lisa"
            };
            var originalOrder = new ListCompression(testList);
            _testOutputHelper.WriteLine(originalOrder.ToString());
            var populationSize = 100;
            var generations = 10;
            var threshold = 1.0;
            var initialPopulation = Enumerable
                .Range(0, populationSize)
                .Select(_ => ListCompression.RandomInstance(testList));
            var ga = new GeneticAlgorithm<ListCompression>(
                initialPopulation,
                mutationChance: 0.2,
                crossoverChance: 0.7,
                GeneticAlgorithm<ListCompression>.SelectionType.Tournament);
            var result = ga.Run(generations, threshold, _testOutputHelper.WriteLine);
            _testOutputHelper.WriteLine(result.ToString());
        }

        public class ListCompression : Chromosome<ListCompression>
        {
            private readonly List<string> _list;
            private readonly Random _random = new Random();

            public ListCompression(List<string> list)
            {
                _list = list;
            }

            public static ListCompression RandomInstance(List<string> from)
            {
                var rnd = new Random();
                var tempList = new List<string>(from)
                    .OrderBy(_ => rnd.Next())
                    .ToList();

                return new ListCompression(tempList);
            }

            public override double Fitness()
            {
                return 1.0 / BytesCompressed();
            }

            private int BytesCompressed()
            {
                var byteArray = _list
                    .SelectMany(s => Encoding.ASCII.GetBytes(s))
                    .ToArray();
                using var outputStream = new MemoryStream();
                using var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal);
                gzipStream.Write(byteArray, 0, byteArray.Length);

                return outputStream.ToArray().Length;
            }

            public override List<ListCompression> Crossover(ListCompression other)
            {
                var child1 = new ListCompression(new List<string>(_list));
                var child2 = new ListCompression(new List<string>(_list));
                var idx1 = _random.Next(_list.Count);
                var idx2 = _random.Next(other._list.Count);
                var s1 = _list[idx1];
                var s2 = other._list[idx2];
                var idx3 = _list.IndexOf(s2);
                var idx4 = other._list.IndexOf(s1);
                Swap(child1._list, idx1, idx3);
                Swap(child2._list, idx2, idx4);
                return new List<ListCompression>{ child1, child2 };
            }

            private void Swap(List<string> letters, int a, int b)
            {
                var temp = letters[a];
                letters[a] = letters[b];
                letters[b] = temp;
            }

            public override void Mutate()
            {
                var idx1 = _random.Next(_list.Count);
                var idx2 = _random.Next(_list.Count);
                Swap(_list, idx1, idx2);
            }

            public override ListCompression Copy()
            {
                return new(new List<string>(_list));
            }

            public override string ToString() {
                return $"Order: [{string.Join(',', _list)}] Bytes: {BytesCompressed()}";
            }
        }
    }
}

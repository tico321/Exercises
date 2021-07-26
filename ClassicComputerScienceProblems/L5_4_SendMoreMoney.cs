using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L5_1_2_GeneticAlgorithms;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// SEND+MORE=MONEY
    /// </summary>
    public class L5_4_SendMoreMoney
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L5_4_SendMoreMoney(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SendMoreMoneyWithGeneticAlgorithms()
        {
            var populationSize = 1000;
            var generations = 1000;
            var threshold = 1.0;
            var initialPopulation = Enumerable
                .Range(0, populationSize)
                .Select(_ => SendMoreMoney.RandomInstance())
                .ToList();
            var ga = new GeneticAlgorithm<SendMoreMoney>(
                initialPopulation,
                mutationChance: 0.2,
                crossoverChance: 0.7,
                GeneticAlgorithm<SendMoreMoney>.SelectionType.Roulette);
            var result = ga.Run(generations, threshold, _testOutputHelper.WriteLine);

            _testOutputHelper.WriteLine(result.ToString());
        }

        public class SendMoreMoney : Chromosome<SendMoreMoney>
        {
            private List<char> _letters;
            private Random _random = new();

            public SendMoreMoney(List<char> letters)
            {
                _letters = letters;
            }

            public static SendMoreMoney RandomInstance()
            {
                var rnd = new Random();
                var letters =
                    new List<char>{ 'S', 'E', 'N', 'D', 'M', 'O', 'R', 'Y', ' ', ' '}
                    .OrderBy(item => rnd.Next()).ToList(); //shuffle
                return new SendMoreMoney(letters);
            }

            public override double Fitness()
            {
                var s = _letters.IndexOf('S');
                var e = _letters.IndexOf('E');
                var n = _letters.IndexOf('N');
                var d = _letters.IndexOf('D');
                var m = _letters.IndexOf('M');
                var o = _letters.IndexOf('O');
                var r = _letters.IndexOf('R');
                var y = _letters.IndexOf('Y');
                var send = s * 1000 + e * 100 + n * 10 + d;
                var more = m * 1000 + o * 100 + r * 10 + e;
                var money = m * 10000 + o * 1000 + n * 100 + e * 10 + y;
                var difference = Math.Abs(money - (send + more));
                // Difference is the absolute value of the difference between MONEY and SEND+MORE.
                // This way smaller values looks like higher values, since fitness maximizes the value.
                // Dividing 1 by a fitness value is a simple way to convert a minimization problem into a maximization problem.
                return 1.0 / (difference + 1.0);
            }

            public override List<SendMoreMoney> Crossover(SendMoreMoney other)
            {
                var child1 = new SendMoreMoney(new List<char>(_letters));
                var child2 = new SendMoreMoney(new List<char>(other._letters));
                var idx1 = _random.Next(_letters.Count);
                var idx2 = _random.Next(other._letters.Count);
                var l1 = _letters[idx1];
                var l2 = other._letters[idx2];
                var idx3 = _letters.IndexOf(l2);
                var idx4 = other._letters.IndexOf(l1);
                Swap(child1._letters, idx1, idx3);
                Swap(child2._letters, idx2, idx4);
                return new List<SendMoreMoney> {child1, child2};
            }

            public override void Mutate()
            {
                var idx1 = _random.Next(_letters.Count);
                var idx2 = _random.Next(_letters.Count);
                Swap(_letters, idx1, idx2);
            }

            public override SendMoreMoney Copy()
            {
                return new(new List<char>(_letters));
            }

            public override string ToString() {
                var s = _letters.IndexOf('S');
                var e = _letters.IndexOf('E');
                var n = _letters.IndexOf('N');
                var d = _letters.IndexOf('D');
                var m = _letters.IndexOf('M');
                var o = _letters.IndexOf('O');
                var r = _letters.IndexOf('R');
                var y = _letters.IndexOf('Y');
                var send = s * 1000 + e * 100 + n * 10 + d;
                var more = m * 1000 + o * 100 + r * 10 + e;
                var money = m * 10000 + o * 1000 + n * 100 + e * 10 + y;
                var difference = Math.Abs(money - (send + more));
                return (send + " + " + more + " = " + money + " Difference: " + difference);
            }

            private void Swap(List<char> letters, int a, int b)
            {
                var temp = letters[a];
                letters[a] = letters[b];
                letters[b] = temp;
            }
        }
    }
}

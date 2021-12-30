using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L5_1_2_GeneticAlgorithms;

namespace ClassicComputerScienceProblems
{
    public class L5_3_GA_SimpleEquation
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L5_3_GA_SimpleEquation(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        /// <summary>
        /// Maximize the equation 6x - x 2 + 4y - y 2.
        /// In other words, what values for x and y in that equation will yield the largest number.
        /// By using simple calculus we know the values are x = 3 and y = 2
        /// </summary>
        [Fact(Skip = "Takes too long")]
        public void MaximizeEquationSimpleTest()
        {
            var populationSize = 20;
            var maxGenerations = 100;
            var optimal = new SimpleEquation(3, 2);
            var fitnessThreshold = 13.0; // because we already know the right answer
            var initialPopulation = Enumerable
                .Range(0, populationSize)
                .Select(_ => SimpleEquation.RandomInstance());
            var ga = new GeneticAlgorithm<SimpleEquation>(
                initialPopulation,
                mutationChance: 0.5,
                crossoverChance: 0.5,
                GeneticAlgorithm<SimpleEquation>.SelectionType.Tournament);

            var result = ga.Run(maxGenerations, fitnessThreshold, _testOutputHelper.WriteLine);
            _testOutputHelper.WriteLine($"maxGenerations {maxGenerations}");
            _testOutputHelper.WriteLine($"fitnessThreshold {fitnessThreshold}");
            _testOutputHelper.WriteLine($"result {result}");
            _testOutputHelper.WriteLine($"optimal {optimal}");
        }

        /// <summary>
        /// The genes of a SimpleEquation chromosome can be thought of as x and y.
        /// The method fitness() evaluates x and y using the equation 6x - x 2 + 4y - y 2.
        /// The higher the value, the more fit the individual chromosome is.
        /// To combine one SimpleEquation with another in crossover(), the y values of the two instances are simply
        /// swapped to create the two children.
        /// mutate() randomly increments or decrements x or y.
        /// </summary>
        public class SimpleEquation : Chromosome<SimpleEquation>
        {
            //  In the case of a random instance, x and y are initially set to be random integers between 0 and 100
            private const int MaxStart = 100;
            public int X { get; private set; }
            public int Y { get; private set; }

            public SimpleEquation(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static SimpleEquation RandomInstance()
            {
                var random = new Random();
                var x = random.Next(MaxStart);
                var y = random.Next(MaxStart);
                return new SimpleEquation(x, y);
            }

            public override double Fitness()
            {
                // 6x - x 2 + 4y - y 2.
                return 6 * X - X * X + 4 * Y - Y * Y;
            }

            public override List<SimpleEquation> Crossover(SimpleEquation other)
            {
                var child1 = new SimpleEquation(X, other.Y);
                var child2 = new SimpleEquation(other.X, Y);
                return new List<SimpleEquation>{ child1, child2 };
            }

            public override void Mutate()
            {
                var random = new Random();
                if (random.NextDouble() > 0.5)// mutate x
                {
                    X = random.NextDouble() > 0.5 ? X + 1 : X - 1;
                }
                else // otherwise mutate y
                {
                    Y = random.NextDouble() > 0.5 ? Y + 1 : Y - 1;
                }
            }

            public override SimpleEquation Copy()
            {
                return new(X, Y);
            }

            public override string ToString()
            {
                return $"X: {X} Y: {Y} Fitness: {Fitness()}";
            }
        }
    }
}

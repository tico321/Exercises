using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Genetic algorithms are a simulation of natural selection to solve computational challenges.
    /// A genetic algorithm includes a population (group) of individuals known as chromosomes.
    /// The chromosomes, each composed of genes that specify their traits, compete to solve some problem.
    /// How well a chromosome solves a problem is defined by a fitness function.
    /// They depend on 3 partially or or fully stochastic (randomly determined) operations:
    /// Selection, crossover and mutation.
    ///
    /// The genetic algorithm goes through generations. In each generation, the chromosomes that are more fit are more
    /// likely to be selected to reproduce. There is also a probability in each generation that two chromosomes will
    /// have their genes merged. This is known as crossover. And finally, there is the important possibility in each
    /// generation that a gene in a chromosome may mutate (randomly change).
    ///
    /// After the fitness function of some individual in the population crosses some specified threshold,
    /// or the algorithm runs through some specified maximum number of generations,
    /// the best individual (the one that scored highest in the fitness function) is returned.
    ///
    /// Genetic algorithms may not find an optimal solution in a reasonable amount of time.
    /// For most problems, more deterministic algorithms exist with better guarantees.
    /// But there are problems for which no fast deterministic algorithm is known to exist.
    /// In these cases, genetic algorithms are a good choice.
    /// </summary>
    public class L5_1_2_GeneticAlgorithms
    {
        /*
         * Chromosome defines five essential features:
         * 1. Determine its own fitness
         * 2. Implement crossover (combine itself with another of the same type to create children)--
         *    in other words, mix itself with another chromosome
         * 3. Mutate--make a small, fairly random change in itself
         * 4. Copy itself
         * 5. Compare itself to other chromosomes of the same type
         */
        public abstract class Chromosome<T> : IComparable<Chromosome<T>>
            // T is bound to Chromosome itself, so T must be a subclass of Chromosome.
            // This is helpful for the methods crossover(), copy(), and compareTo()
            where T : Chromosome<T>
        {
            public abstract double Fitness();

            public abstract List<T> Crossover(T other);

            public abstract void Mutate();

            public abstract T Copy();

            public int CompareTo(Chromosome<T> other)
            {
                var mine = Fitness();
                var theirs = other.Fitness();
                return mine.CompareTo(theirs);
            }
        }

        /*
         * Algorithm steps
         * 1. Create an initial population of random chromosomes for the first generation of the algorithm.
         * 2. Measure the fitness of each chromosome in this generation of the population.
         *    If any exceeds the threshold, return it, and the algorithm ends.
         * 3. Select some individuals to reproduce, with a higher probability of selecting those with the highest fitness.
         * 4. Crossover (combine), with some probability, some of the selected chromosomes to create children
         *    that represent the population of the next generation.
         * 5. Mutate, usually with a low probability, some of those chromosomes.
         *    The population of the new generation is now complete, and it replaces the population of the last generation.
         * 6. Return to step 2 unless the maximum number of generations has been reached.
         *    If that is the case, return the best chromosome found so far.
         */
        public class GeneticAlgorithm<C> where C : Chromosome<C>
        {
            private List<C> _population;
            private readonly double _mutationChance;
            private readonly double _crossoverChance;
            private readonly SelectionType _selectionType;
            private readonly Random _random = new();

            // Selection method used by the algorithm.
            public enum SelectionType
            {
                // also called wheel selection, is random
                Roulette,
                // Tournament gives chromosomes a chance of being picked
                // a certain number of random chromosomes are challenged against one another,
                // and the one with the best fitness is selected.
                Tournament
            }

            /// <summary>
            /// Run() coordinates the measurement, reproduction (which includes selection), and mutation steps
            /// that bring the population from one generation to the next. It also keeps track of the best (fittest)
            /// chromosome found at any point in the search.
            /// </summary>
            /// <param name="maxGenerations">Generations threshold limit</param>
            /// <param name="fitnessThreshold">Maximum threshold that a chromosome can reach</param>
            public C Run(int maxGenerations, double fitnessThreshold, Action<string> debugger = null)
            {
                var fittest = _population.Max().Copy();
                for (var generation = 0; generation < maxGenerations; generation++) {
                    // early exit if we beat threshold
                    if (fittest.Fitness() >= fitnessThreshold) {
                        return fittest;
                    }

                    // Debug printout
                    if (debugger != null)
                    {
                        debugger($"Generation {generation} Best {fittest.Fitness()} " +
                                 $"Avg {_population.Select(c => c.Fitness()).Average()}");
                    }
                    ReproduceAndReplace();
                    Mutate();
                    var generationBest = _population.Max();
                    if (generationBest.Fitness() > fittest.Fitness()) {
                        fittest = generationBest.Copy();
                    }
                }

                return fittest;
            }

            /// <summary>
            /// Parameters that are configured at creation time
            /// </summary>
            /// <param name="initialPopulation">First generation of chromosomes</param>
            /// <param name="mutationChance">The probability of each chromosome in each generation mutating</param>
            /// <param name="crossoverChance">The probability that two parents selected to reproduce have children that
            /// are a mixture of their genes. Otherwise, the children are just duplicates of the parents.</param>
            /// <param name="selectionType">The type of selection to use.</param>
            public GeneticAlgorithm(IEnumerable<C> initialPopulation, double mutationChance, double crossoverChance, SelectionType selectionType)
            {
                _population = new List<C>(initialPopulation);
                _mutationChance = mutationChance;
                _crossoverChance = crossoverChance;
                _selectionType = selectionType;
            }

            /// <summary>
            /// Use the probability distribution wheel to pick numPicks individuals
            /// </summary>
            /// <param name="wheel">The values that represent each chromosome’s percentage of total fitness are provided
            /// in the parameter wheel. The chromosomes with the highest fitness have a better chance of being picked.
            /// </param>
            /// <param name="numPicks">Individuals to pick</param>
            private IEnumerable<C> PickRoulette(double[] wheel, int numPicks)
            {
                var picks = new List<C>();
                for (var i = 0; i < numPicks; i++)
                {
                    var pick = _random.NextDouble();
                    for (var j = 0; j < wheel.Length; j++) {
                        pick -= wheel[j];
                        if (pick <= 0) { // went “over”, leads to a pick
                            picks.Add(_population[j]);
                            break;
                        }
                    }
                }
                return picks;
            }

            /// <summary>
            /// One thing to keep in mind is that a higher number of participants in the tournament leads to less
            /// diversity in the population, because chromosomes with poor fitness are more likely to be eliminated
            /// in match-ups.
            /// More sophisticated forms of tournament selection may pick individuals that are not the best,
            /// but are second or third best, based on some kind of decreasing probability model.
            /// </summary>
            /// <param name="numParticipants">Participants of the tournament</param>
            /// <param name="numPicks">The number of chromosomes to pick</param>
            /// <returns>Picked chromosomes</returns>
            private IEnumerable<C> PickTournament(int numParticipants, int numPicks)
            {
                // Find numParticipants random participants to be in the tournament
                var tournament = new List<C>();
                while (tournament.Count < numParticipants)
                {
                    var r = _random.Next(_population.Count);
                    if (!tournament.Contains(_population[r]))
                    {
                        tournament.Add(_population[r]);
                    }
                }

                // Find the numPicks highest fitnesses in the tournament
                tournament = tournament.OrderByDescending(c => c).ToList();
                return tournament.Take(numPicks);
            }

            /// <summary>
            /// Replace the population with a new generation of individuals.
            /// 1. Two chromosomes, called parents, are selected for reproduction using one of the two selection methods.
            ///    For tournament selection, we always run the tournament among half of the total population,
            ///    but this too could be a configuration option.
            /// 2. There is crossoverChance that the two parents will be combined to produce two new chromosomes,
            ///    in which case they are added to nextPopulation.
            ///    If there are no children, the two parents are just added to nextPopulation.
            /// 3. If nextPopulation has as many chromosomes as population, it replaces it.
            ///    Otherwise, we return to step 1.
            /// </summary>
            private void ReproduceAndReplace()
            {
                var nextPopulation = new List<C>();
                // keep going until we've filled the new generation
                while (nextPopulation.Count < _population.Count) {
                    // pick the two parents
                    IEnumerable<C> parents;
                    if (_selectionType == SelectionType.Roulette) {
                        // create the probability distribution wheel
                        var totalFitness = _population.Select(c => c.Fitness()).Sum();
                        double[] wheel = _population
                            .Select(c => c.Fitness() / totalFitness)
                            .ToArray();
                        parents = PickRoulette(wheel, 2);
                    } else { // tournament
                        parents = PickTournament(_population.Count / 2, 2);
                    }

                    // potentially crossover the 2 parents
                    if (_random.NextDouble() < _crossoverChance) {
                        var parent1 = parents.First();
                        var parent2 = parents.Skip(1).First();
                        var cross = parent1.Crossover(parent2);
                        nextPopulation.AddRange(cross);
                    }
                    else
                    {   // just add the two parents
                        nextPopulation.AddRange(parents);
                    }
                }

                // if we have an odd number, we'll have 1 extra, so we remove it
                if (nextPopulation.Count > _population.Count) {
                    nextPopulation.Remove(nextPopulation.First());
                }

                // replace the reference/generation
                _population = nextPopulation;
            }

            // With mutationChance probability, mutate each individual
            private void Mutate()
            {
                foreach (var individual in _population.Where(individual => _random.NextDouble() < _mutationChance))
                {
                    individual.Mutate();
                }
            }
        }
    }
}

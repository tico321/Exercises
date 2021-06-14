using System;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /*
     * The mathematically significant number pi (π or 3.14159...) can be derived using many formulas.
     * One of the simplest is the Leibniz formula.
     * It posits that the convergence of the following infinite series is equal to pi:
     *      π = 4/1 - 4/3 + 4/5 - 4/7 + 4/9 - 4/11...
     * where the numerator remains 4 while the denominator increases by 2,
     * and the operation on the terms alternates between addition and subtraction.
     */
    public class L1_4_CalculatePi
    {
        [Theory]
        [InlineData(0, 0.0, 0.1)]
        [InlineData(1, 4.0, 0.1)]
        [InlineData(2, 2.66, 0.1)]
        [InlineData(100, 3.13, 0.1)]
        [InlineData(1000, 3.14, 0.01)]
        public void CalculatPiTest(int terms, double expected, double errorTolerance)
        {
            var actual = CalculatePi(terms);
            var diff = Math.Abs(actual - expected);

            Assert.True(diff < errorTolerance);
        }

        public static double CalculatePi(int nTerms)
        {
            var numerator = 4.0;
            var denominator = 1.0;
            var operation = 1.0;
            var pi = 0.0;
            for (var i = 0; i < nTerms; i++)
            {
                pi += operation * (numerator / denominator);
                operation *= -1;
                denominator += 2;
            }

            return pi;
        }
    }
}
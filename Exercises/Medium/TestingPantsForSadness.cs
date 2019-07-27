using Xunit;

namespace Exercises.Medium
{
    /*
        Testing Pants for Sadness
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        The average miner Vaganych took refresher courses. As soon as a miner completes the courses, he should take exams. The hardest one is a computer test called "Testing Pants for Sadness".

        The test consists of n questions; the questions are to be answered strictly in the order in which they are given, from question 1 to question n. Question i contains ai answer variants, exactly one of them is correct.

        A click is regarded as selecting any answer in any question. The goal is to select the correct answer for each of the n questions. If Vaganych selects a wrong answer for some question, then all selected answers become unselected and the test starts from the very beginning, from question 1 again. But Vaganych remembers everything. The order of answers for each question and the order of questions remain unchanged, as well as the question and answers themselves.

        Vaganych is very smart and his memory is superb, yet he is unbelievably unlucky and knows nothing whatsoever about the test's theme. How many clicks will he have to perform in the worst case?

        Input
        The first line contains a positive integer n (1 ≤ n ≤ 100). It is the number of questions in the test. The second line contains space-separated n positive integers ai (1 ≤ ai ≤ 109), the number of answer variants to question i.

        Output
        Print a single number — the minimal number of clicks needed to pass the test it the worst-case scenario.

        Please do not use the %lld specificator to read or write 64-bit integers in С++. It is preferred to use the cin, cout streams or the %I64d specificator.
     */
    public class TestingPantsForSadness
    {
        [Theory]
        [InlineData("1", "2", 2)]
        [InlineData("2", "2 2", 5)]
        [InlineData("3", "2 2 2", 9)]
        [InlineData("4", "2 2 2 2", 14)]
        [InlineData("1", "3", 3)]
        [InlineData("2", "3 3", 8)]
        [InlineData("3", "3 3 3", 15)]
        [InlineData("3", "3 2 3", 13)]
        [InlineData("3", "4 4 4", 21)]
        [InlineData("2", "1000000000 1000000000", 2999999999L)]

        public void Scenarios(string input1, string input2, long expected)
        {
            var actual = TestingPantsForSadnessSolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}
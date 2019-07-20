using Xunit;

namespace Exercises.Medium
{
    /*
        The busses in Berland are equipped with a video surveillance system. The system
        records information about changes in the number of passengers in a bus after stops.

        If 𝑥 is the number of passengers in a bus just before the current bus stop and 𝑦 is
        the number of passengers in the bus just after current bus stop, the system records
        the number 𝑦−𝑥. So the system records show how number of passengers changed.

        The test run was made for single bus and 𝑛 bus stops. Thus, the system recorded
        the sequence of integers 𝑎1,𝑎2,…,𝑎𝑛 (exactly one number for each bus stop),
        where 𝑎𝑖 is the record for the bus stop 𝑖. The bus stops are numbered from 1 to 𝑛
        in chronological order.

        Determine the number of possible ways how many people could be in the bus before
        the first bus stop, if the bus has a capacity equals to 𝑤
        (that is, at any time in the bus there should be from 0 to 𝑤 passengers inclusive).

        Input
        The first line contains two integers 𝑛 and 𝑤 (1≤𝑛≤1000,1≤𝑤≤109) —
        the number of bus stops and the capacity of the bus.

        The second line contains a sequence 𝑎1,𝑎2,…,𝑎𝑛 (−106≤𝑎𝑖≤106), where 𝑎𝑖 equals to the
        number, which has been recorded by the video system after the 𝑖-th bus stop.

        Output
        Print the number of possible ways how many people could be in the bus before the first
        bus stop, if the bus has a capacity equals to 𝑤. If the situation is contradictory
        (i.e. for any initial number of passengers there will be a contradiction), print 0.
     */
    public class BusVideoSystem
    {
        [Theory]
        [InlineData("3 5", "2 1 -3", 3)] //initially there could be 0, 1 or 2 passangers
        [InlineData("2 4", "-1 1", 4)] //initially there could be 1, 2, 3, 4 passengers
        [InlineData("4 10", "2 4 1 2", 2)]//initially there could be 0 or 1 passengers
        [InlineData("3 4", "-3 -4 4", 0)]
        public void Scenarios(string input1, string input2, int expected)
        {
            var actual = BusVideoSystemSolution.Solve(input1, input2);

            Assert.Equal(expected, actual);
        }
    }
}

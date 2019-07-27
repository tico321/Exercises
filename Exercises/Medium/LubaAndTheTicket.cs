using Xunit;

namespace Exercises.Medium
{
    /*
        Luba And The Ticket
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Luba has a ticket consisting of 6 digits. In one move she can choose digit in any position and replace it with arbitrary digit. She wants to know the minimum number of digits she needs to replace in order to make the ticket lucky.

        The ticket is considered lucky if the sum of first three digits equals to the sum of last three digits.

        Input
        You are given a string consisting of 6 characters (all characters are digits from 0 to 9) — this string denotes Luba's ticket. The ticket can start with the digit 0.

        Output
        Print one number — the minimum possible number of digits Luba needs to replace to make the ticket lucky.
        */
    public class LubaAndTheTicket
    {
        [Theory]
        [InlineData("000000", 0)]
        [InlineData("103452", 1)]
        [InlineData("123456", 2)]
        [InlineData("111000", 1)]
        [InlineData("899888", 1)]
        [InlineData("858022", 2)]
        [InlineData("223456", 2)]

        public void Scenarios(string input1, int expected)
        {
            var actual = LubaAndTheTicketSolution.Solve(input1);

            Assert.Equal(expected, actual);
        }
    }
}

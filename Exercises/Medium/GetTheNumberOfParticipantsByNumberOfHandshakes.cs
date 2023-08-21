using Xunit;

namespace Exercises.Medium
{
    /*
        Johnny is a farmer and he annually holds a beet farmers convention "Drop the beet".
        Every year he takes photos of farmers handshaking. Johnny knows that no two farmers handshake more than once. He also knows that some of the possible handshake combinations may not happen.
        However, Johnny would like to know the minimal amount of people that participated this year just by counting all the handshakes.
        Help Johnny by writing a function, that takes the amount of handshakes and returns the minimal amount of people needed to perform these handshakes (a pair of farmers handshake only once).
     */
    public sealed class GetTheNumberOfParticipantsByNumberOfHandshakes
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 3)]
        [InlineData(4, 6)]
        [InlineData(5, 7)]
        public void Test(int people, int handshakes)
        {
            Assert.Equal(people, GetParticipants(handshakes));
        }

        static int GetParticipants(int handshakes)
        {
            if (handshakes == 0) return 0;

            for(int people=1; people <= handshakes + 1; people++)
            {
                if(GetHandshakes(people) >= handshakes)
                {
                    return people;
                }
            }

            return 0;
        }

        static int GetHandshakes(int people)
        {
            int result = 0;
            for(int i=people-1; i>0; i--)
            {
                result += i;
            }
            return result;
        }
    }
}

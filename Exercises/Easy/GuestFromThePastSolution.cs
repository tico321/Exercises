namespace Exercises.Easy
{
    public class GuestFromThePastSolution
    {
        public static long Solve(string in1, string in2, string in3, string in4)
        {
            var money = long.Parse(in1);
            var plasticCost = long.Parse(in2);
            var glassCost = long.Parse(in3);
            var glassReturnVal = long.Parse(in4);

            if (plasticCost <= glassCost - glassReturnVal || glassCost > money) //we don't need glass bottles
            {
                return money / plasticCost;
            }

            var remainingMoney = money - glassCost; //we buy one
            if (remainingMoney < 0) return 0;
            var remainingGlassCost = glassCost - glassReturnVal; //we can exchange bottles the rest
            var totalBottles = remainingMoney / remainingGlassCost + 1;//we add the initial one
            var totalSpend = totalBottles * remainingGlassCost;
            if (totalSpend < money)
            {
                totalBottles += (money - totalSpend) / plasticCost;
            }

            return totalBottles;
        }
    }
}

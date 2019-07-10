using System.Collections.Generic;
using System.Linq;

namespace Exercises.Hard
{
    public class CountChangeSolution
    {
        public int Solve(int money, IEnumerable<int> coins)
        {
            if (money < 0 || coins.Count() == 0) return 0;
            if (money == 0) return 1;
            return this.Solve(money - coins.First(), coins) +
                   this.Solve(money, coins.Skip(1));
        }
    }
}

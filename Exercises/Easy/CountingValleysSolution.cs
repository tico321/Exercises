namespace Exercises.Easy
{
    public class CountingValleysSolution
    {
        public int Solve(int _, string s)
        {
            var level = 0;
            var valleyCount = 0;
            var enteredAValley = false;
            foreach (var step in s)
            {
                if (step == 'U') level++;
                if (step == 'D') level--;
                if (level >= 0 && enteredAValley) enteredAValley = false;
                if (level < 0 && !enteredAValley)
                {
                    enteredAValley = true;
                    valleyCount++;
                }
            }

            return valleyCount;
        }
    }
}

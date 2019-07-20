namespace Exercises.Medium
{
    public class NutsSolution
    {
        public static int Solve(string input)
        {
            var inputArgs = input.Split(' ');
            var maximumNumberOfSections = int.Parse(inputArgs[0]);
            var numberOfNuts = int.Parse(inputArgs[1]);
            var numberOfDivisors = int.Parse(inputArgs[2]);
            var sectionCapacity = int.Parse(inputArgs[3]);

            return GetMinimumBoxes(
                maximumNumberOfSections,
                numberOfNuts,
                numberOfDivisors,
                sectionCapacity);
        }

        private static int GetMinimumBoxes(
            int maxSectionsPerBox,
            int nuts,
            int divisors,
            int sectionCapacity)
        {
            if (nuts <= 0) return 0;

            var nextBoxSections = 1;
            if (divisors > 0)
            {
                if (maxSectionsPerBox == divisors)
                {
                    nextBoxSections = divisors;
                    divisors = 1;
                }
                else if (maxSectionsPerBox > divisors)
                {
                    nextBoxSections = divisors + 1;
                    divisors = 0;
                }
                else
                {
                    nextBoxSections = maxSectionsPerBox;
                    divisors = divisors - maxSectionsPerBox + 1;
                }
            }

            var remainingNuts = nuts - (nextBoxSections * sectionCapacity);

            var result = 1 + GetMinimumBoxes(
                maxSectionsPerBox,
                remainingNuts,
                divisors,
                sectionCapacity);
            return result;
        }
    }
}

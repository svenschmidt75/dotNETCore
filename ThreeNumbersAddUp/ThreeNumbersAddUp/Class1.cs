using System.Collections.Generic;
using System.Linq;

namespace ThreeNumbersAddUp
{
    public static class Class1
    {
        public static IEnumerable<int> ThreeNumbers(IList<int> numbers, int value)
        {
            for (int i = 0; i < numbers.Count - 2; i++)
            {
                int a1 = numbers[i];
                if (a1 > value)
                    continue;
                for (int j = i + 1; j < numbers.Count - 1; j++)
                {
                    int a2 = numbers[j];
                    if (a1 + a2 > value)
                        continue;
                    for (int k = j + 1; k < numbers.Count; k++)
                    {
                        int a3 = numbers[k];
                        if (a1 + a2 + a3 > value)
                            continue;
                        if (a1 + a2 + a3 == value)
                        {
                            return new[] {a1, a2, a3};
                        }
                    }
                }
            }
            return Enumerable.Empty<int>();
        }
    }
}
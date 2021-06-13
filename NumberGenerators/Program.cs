using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerators
{
    public class Program
    {
        static void Main()
        {
            // Run a basic test to see if the output distribution looks reasonable.
            // Majority of testing will be done in the test project.
            int[] randomNums = { -1, 0, 1, 2, 3 };
            double[] probabilities = { 0.01, 0.3, 0.58, 0.1, 0.01 };
            var gen = new ProbabilisticNumberGenerator(randomNums, probabilities);

            var results = new Dictionary<int, int>();
            Array.ForEach(randomNums, num => results.Add(num, 0));

            for (var i = 0; i < 100000; i++)
            {
                var num = gen.NextNum();
                if (results.ContainsKey(num))
                {
                    results[num]++;
                }
                else
                {
                    results.Add(num, 1);
                }
            }

            var outputString = string.Join(
                Environment.NewLine,
                results.Select(kv => $"{kv.Key} occurred {kv.Value} times"));

            Console.WriteLine($"Results:{Environment.NewLine}{outputString}");
        }
    }
}

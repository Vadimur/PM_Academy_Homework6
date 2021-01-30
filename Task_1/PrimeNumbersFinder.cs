using System;
using System.Linq;

namespace Task_1
{
    public class PrimeNumbersFinder
    {
        public int CountPrimesWithLinq(int from, int to)
        {
            if (from < 0 && to < 0)
            {
                return 0;
            }
            
            from = from < 2 ? 2 : from;
            if (to <= from)
                return 0;

            int numberOfPrimes = Enumerable.Range(from, to - from + 1)
                .Count(number => Enumerable.Range(2, (int) Math.Sqrt(number) - 1)
                                            .All(x => number % x != 0));

            return numberOfPrimes;
        }
        
        public int CountPrimesWithPLinq(int from, int to)
        {
            if (from < 0 && to < 0)
            {
                return 0;
            }
            
            from = from < 2 ? 2 : from;
            if (to <= from)
                return 0;
            
            int numberOfPrimes = Enumerable.Range(from, to - from + 1)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .AsUnordered()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Count(number => Enumerable.Range(2, (int) Math.Sqrt(number) - 1)
                                             .All(x => number % x != 0));

            return numberOfPrimes;
            
        }
    }
}
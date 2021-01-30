using System;
using System.Collections.Generic;
using System.Threading;

namespace Task_2
{
    public class PrimesFinder
    {
        public ThreadSafeList<int> Primes { get; } = new ThreadSafeList<int>();
        
        public void FindPrimes(object obj)
        {
            Settings settings = obj as Settings;
            if (settings == null) 
                return;

            if (!settings.PrimesTo.HasValue || !settings.PrimesFrom.HasValue)
            {
                return;
            }
            
            int start = settings.PrimesFrom.Value < 2 ? 2 : settings.PrimesFrom.Value;
            for (int i = start; i < settings.PrimesTo.Value; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                    Primes.AddWithoutDuplicate(i);
                
            }
        }
        
    }
}
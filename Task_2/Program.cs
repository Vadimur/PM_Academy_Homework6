using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimesFinder primesFinder = new PrimesFinder();
            JsonWorker jsonWorker = new JsonWorker();

            SettingsWrapper settingsWrapper = jsonWorker.ReadSettings();
            if (settingsWrapper.IsSuccess == false)
            {
                jsonWorker.WriteResult(new Result(false, TimeSpan.Zero.ToString(), null, settingsWrapper.Error));
                return;
            }
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Thread> threads = new List<Thread>();
            foreach (var element in settingsWrapper.Settings)
            {
                if (element == null)
                    continue;
                
                Thread workThread = new Thread(primesFinder.FindPrimes);
                workThread.Start(element);
                threads.Add(workThread);
            }
            threads.ForEach(thread => thread.Join());
            stopwatch.Stop();

            var primes = primesFinder.Primes.GetElements();
            Array.Sort(primes);
            jsonWorker.WriteResult(new Result(true, stopwatch.Elapsed.ToString(), primes));
        }
    }
}
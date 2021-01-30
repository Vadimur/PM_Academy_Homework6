using System;
using System.Diagnostics;

namespace Task_1
{
    class Program
    {
        private static string author = "Made by Mulish Vadym\n";
        private static string programDescription = "Task 1 Finding Prime Numbers with LINQ and PLINQ";
        static void Main(string[] args)
        {
            PrimeNumbersFinder primesFinder = new PrimeNumbersFinder();
            
            Console.WriteLine(programDescription);
            Console.WriteLine(author);
            
            int primesFrom = GetNumberFromUser("Search primes from: ");
            int primesTo = GetNumberFromUser("Search primes to: ");
            Console.WriteLine($"Entered range [{primesFrom}; {primesTo})");

            int numberOfPrimes;
            Console.WriteLine("Choose the method of searching for prime numbers\n" +
                              "1. LINQ\n" +
                              "2. PLINQ\n");
            int chosenMethodId = GetMethodNumberFromUser("Enter method's id: ", 1, 2);
            
            Stopwatch stopwatch = new Stopwatch();

            if (chosenMethodId == 1)
            {
                stopwatch.Start();
                numberOfPrimes = primesFinder.CountPrimesWithLinq(primesFrom, primesTo);
            }
            else if (chosenMethodId == 2)
            {
                stopwatch.Start();
                numberOfPrimes = primesFinder.CountPrimesWithPLinq(primesFrom, primesTo);
            }
            else
            {
                Console.WriteLine("Error occured");
                return;
            }
            
            stopwatch.Stop();
            
            Console.WriteLine($"Number of prime numbers in entered range is {numberOfPrimes}");
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
        }

        private static int GetNumberFromUser(string message)
        {
            string input;
            int number;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();

            } while (!int.TryParse(input, out number));

            return number;
        }
        
        private static int GetMethodNumberFromUser(string message, int minId, int maxId)
        {
            int chosenNumber;
            do
            {
                chosenNumber = GetNumberFromUser(message);
                
            } while (chosenNumber < minId || chosenNumber > maxId);

            return chosenNumber;
        }
    }
}
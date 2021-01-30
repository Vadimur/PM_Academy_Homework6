using System;
using System.Diagnostics;
using System.Threading;
using Task_3.Exceptions;

namespace Task_3
{
    class Program
    {
        private static string author = "Made by Mulish Vadym\n";
        private static string programDescription = "Task 3 Issuing unique logins service";
        static void Main(string[] args)
        {
            Console.WriteLine(programDescription);
            Console.WriteLine(author);
            
            LoginTestService loginService = new LoginTestService();
            
            try
            {
                loginService.LoadCredentials();
            }
            catch (ServiceException exception)
            {
                Console.WriteLine(exception.Message);
                return;
            }

            int threadsNumber = ReadNumberOfThreadsFromUser();
            Thread[] threads = new Thread[threadsNumber];
            
            for (int i = 0; i < threadsNumber; i++)
            {
                threads[i] = new Thread(loginService.Login);
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            
            try
            {
                loginService.SaveResult();
                Console.WriteLine("Result was saved");
            }
            catch (ServiceException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static int ReadNumberOfThreadsFromUser()
        {
            int threadsNumber;
            string input;
            do
            {
                Console.Write("Enter number of threads: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out threadsNumber));

            return threadsNumber;
        }
    }
}

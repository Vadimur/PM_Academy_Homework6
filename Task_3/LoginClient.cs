using System;
using System.Threading;

namespace Task_3
{
    public class LoginClient
    {
        public string Login(string login, string password)
        {
            Random random = new Random();
            bool isSuccess = random.Next(0,2) == 1;
            int milliseconds = (int)(random.NextDouble() * 1000);
            Thread.Sleep(milliseconds);

            if (isSuccess)
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                return new Guid().ToString();
            }
        }
    }
}
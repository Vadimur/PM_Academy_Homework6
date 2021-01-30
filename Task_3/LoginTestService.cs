using System;
using System.Collections.Concurrent;
using System.Threading;
using Task_3.Exceptions;
using Task_3.FileWorkers;

namespace Task_3
{
    public class LoginTestService
    {
        private readonly CsvFileReader _fileReader;
        private readonly FileWriter _fileWriter;
        private ConcurrentQueue<LoginCredentials> _credentials;
        private readonly LoginClient _loginClient;
        private int _successfulLogins = 0;
        private int _failedLogins = 0;


        public LoginTestService()
        {
            _fileReader = new CsvFileReader();
            _fileWriter = new FileWriter();
            _loginClient = new LoginClient();
        }
        
        public void LoadCredentials()
        {
            try
            {
                _credentials = new ConcurrentQueue<LoginCredentials>(_fileReader.ReadCredentialsFromFile());
            }
            catch (DataAccessException exception)
            {
                throw new ServiceException(exception.Message, exception);
            }
        }
        
        public void SaveResult()
        {
            try
            {
                _fileWriter.SaveResult(new Result(_successfulLogins, _failedLogins));
            }
            catch (DataAccessException exception)
            {
                throw new ServiceException(exception.Message, exception);
            }
        }

        public void Login()
        {
            while (_credentials.Count != 0)
            {
                LoginCredentials creds;
                bool isSuccess = _credentials.TryDequeue(out creds);
                if (isSuccess == false || creds == null)
                {
                    Interlocked.Increment(ref _failedLogins);
                    continue;
                }
                string token = _loginClient.Login(creds.Login, creds.Password);
            
                if (token.Equals(new Guid().ToString()))
                {
                    Interlocked.Increment(ref _failedLogins);
                }
                else
                {
                    Interlocked.Increment(ref _successfulLogins);
                }
            }
        }
    }
}
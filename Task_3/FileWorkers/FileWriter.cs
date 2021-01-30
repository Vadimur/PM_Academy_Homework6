using System;
using System.IO;
using System.Security;
using System.Text.Json;
using Task_3.Exceptions;

namespace Task_3.FileWorkers
{
    public class FileWriter
    {
        private readonly string _filePath = "result.json";
        
        public void SaveResult(Result result)
        {
            string json = JsonSerializer.Serialize(result);
            try
            {
                File.WriteAllText(_filePath, json);
            }
            catch (Exception exception)
            {
                if (exception is UnauthorizedAccessException ||
                    exception is SecurityException ||
                    exception is IOException)
                {
                    throw new DataAccessException(exception.Message, exception);
                }
                
                throw;
            }
        } 
    }
}
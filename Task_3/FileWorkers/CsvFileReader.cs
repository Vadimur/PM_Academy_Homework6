using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_3.Exceptions;

namespace Task_3.FileWorkers
{
    public class CsvFileReader
    {
        private readonly string _filePath = "logins.csv";
        public IEnumerable<LoginCredentials> ReadCredentialsFromFile()
        {
            string[] fileLines = ReadFile();
            
            List<LoginCredentials> credentials = fileLines
                .Select(line => line.Split(','))
                .Select(line => new LoginCredentials(line[0], line[1]))
                .ToList();
            
            return credentials;
        }

        private string[] ReadFile()
        {
            try
            {
                return File.ReadAllLines(_filePath);
            }
            catch (FileNotFoundException exception)
            {
                throw new DataAccessException("File not found", exception);
            }
            catch (IOException exception)
            {
                throw new DataAccessException(" I/O error occured", exception);
            }
        }
    }
}
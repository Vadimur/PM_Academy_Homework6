using System.Text.Json.Serialization;

namespace Task_3
{
    public class Result
    {
        [JsonPropertyName("successful")]
        public int Successful { get; set; }
        
        [JsonPropertyName("failed")]
        public int Failed { get; set; }
        
        public Result(int successful, int failed)
        {
            Successful = successful;
            Failed = failed;
        }
    }
}
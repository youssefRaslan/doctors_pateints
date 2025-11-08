using System.Text.Json.Serialization;

namespace doctors.Help
{
    public class Error
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}

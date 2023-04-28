using System.Text.Json.Serialization;

namespace WebApp.Domain.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("errorCode")]
        public string Code { get; set; }
        [JsonPropertyName("errorMessage")]
        public string Message { get; set; }
    }
}

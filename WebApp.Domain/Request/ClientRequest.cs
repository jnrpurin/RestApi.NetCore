using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Domain.Request
{
    public class ClientRequest
    {
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}

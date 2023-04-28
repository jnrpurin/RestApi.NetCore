using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Domain.Request
{
    public class ClientRequest
    {
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
    }
}

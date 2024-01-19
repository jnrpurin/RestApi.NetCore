using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Request
{
    public class ClientResponse
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

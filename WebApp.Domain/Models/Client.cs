using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Models
{
    public class Client
    {
        [Key] 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

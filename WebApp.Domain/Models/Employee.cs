using System.ComponentModel.DataAnnotations;
using WebApp.Domain.Enums;

namespace WebApp.Domain.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Shift Shift { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ChangedDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}

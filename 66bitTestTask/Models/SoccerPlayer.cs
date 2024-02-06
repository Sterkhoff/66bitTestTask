using System.ComponentModel.DataAnnotations;
using _66bitTestTask.Models.Enums;

namespace _66bitTestTask.Models
{
    public class SoccerPlayer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        [Required]
        public Team Team { get; set; } = null!;
        public Guid TeamId { get; set; }
        public Country Country { get; set; }
    }
}

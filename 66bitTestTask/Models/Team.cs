using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace _66bitTestTask.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

        public Team(string name)
        {
            Name = name;
        }

    }
}

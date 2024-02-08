using _66bitTestTask.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _66bitTestTask.ViewModels
{
    public class SoccerPlayerViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Длина имени должна быть меньше {1} символов")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, ErrorMessage = "Длина фамилии должна быть меньше {1} символов")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Выберите пол")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [Remote(action: "DateValidation", controller: "Home")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Выберите команду или введите новую")]
        [StringLength(50, ErrorMessage = "Длина названия должна быть меньше {1} символов")]
        public string Team { get; set; } = null!;

        [Required(ErrorMessage = "Выберите страну")]
        public Country Country { get; set; }
        public bool TeamIsNew { get; set; }
    }
}

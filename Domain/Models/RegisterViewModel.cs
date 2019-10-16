using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"[A-Za-zА-Яа-я]+", ErrorMessage = "Фамилия должна содержать либо латинские буквы, либо кирилицу")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [RegularExpression(@"[A-Za-zА-Яа-я]+", ErrorMessage = "Имя должно содержать либо латинские буквы, либо кирилицу")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Почта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}

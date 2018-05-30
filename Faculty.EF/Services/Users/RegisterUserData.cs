using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Faculty.EFCore.Services.Users
{
    public class RegisterUserData
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Логин должен содержать от 3 до 30 символов.")]
        public string UserName { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Фамилия не должна содержать более 50 знаков.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Имя не должно содержать более 50 знаков.")]
        public string Name { get; set; }

        [Required, UIHint("email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введен неправильный email адрес.")]
        public string Email { get; set; }

        [Required, UIHint("password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

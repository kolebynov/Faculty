using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Faculty.EFCore.Services.Users
{
    public class LoginData
    {
        [Required]
        public string UserName { get; set; }
        [Required, UIHint("password")]
        public string Password { get; set; }
    }
}
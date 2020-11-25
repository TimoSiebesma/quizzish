using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; }
    }
}

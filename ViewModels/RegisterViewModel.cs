using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        
    }

}

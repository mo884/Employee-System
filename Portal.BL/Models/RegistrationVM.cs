using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Portal.BL.Models
{
    public class RegistrationVM
    {

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name Required")]
        [MaxLength(50,ErrorMessage = "Min Len 50")]
        public string UserName { get; set; }

        [MinLength(6 , ErrorMessage = "Min len 6")]
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [MinLength(6, ErrorMessage = "Min len 6")]
        [Required(ErrorMessage = "Confirm Password Required")]
        [Compare("Password",ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}

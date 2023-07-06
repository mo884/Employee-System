using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Portal.BL.Models
{
    public class MailVM
    {

        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
        public string Email { get; set; }

    }
}

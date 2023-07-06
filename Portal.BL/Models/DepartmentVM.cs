using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Portal.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50,ErrorMessage = "Max Len 50")]
        [MinLength(3,ErrorMessage = "Min Len 3")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Code Required")]
        [Range(1,5000,ErrorMessage = "Range Btw 1 To 5K")]
        public string Code { get; set; }

    }
}

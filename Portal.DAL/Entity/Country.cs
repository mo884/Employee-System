using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL.Entity
{

    [Table("Country")]
    public class Country
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}

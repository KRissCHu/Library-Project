using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEntities
{
    public class Reader:BaseEntity
    {
        [Required(ErrorMessage = "FirstName is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "FamilyName is required!")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "CardNumber is required!")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "ExpCardDate is required!")]
        public DateTime ExpCardDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEntities
{
    public class User :BaseEntity
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FirstName is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "FamilyName is required!")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "Authority is required!")]
        public bool Authority { get; set; }
    }
}

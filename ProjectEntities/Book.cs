using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEntities
{
    public class Book:BaseEntity
    {
        [Required(ErrorMessage = "ISBN is required!")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Author is required!")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Publisher is required!")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "PubDate is required!")]
        public DateTime PubDate { get; set; }
        [Required(ErrorMessage = "Available is required!")]
        public int Available { get; set; }
    }
}

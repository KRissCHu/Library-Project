using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEntities
{
    public class BookStatus:BaseEntity
    {
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "ReaderId is required!")]
        public int ReaderId { get; set; }
        [Required(ErrorMessage = "BorrowDate is required!")]
        public DateTime BorrowDate { get; set; }
        [Required(ErrorMessage = "ReturnDate is required!")]
        public DateTime ReturnDate { get; set; }
        [Required(ErrorMessage = "ReturnedDate is required!")]
        public DateTime ReturnedDate { get; set; }
    }
}

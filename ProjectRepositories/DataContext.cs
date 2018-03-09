using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectRepositories
{
    public class DataContext:DbContext 

    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookStatus> BooksStatus { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext()
            : base("LibrarySystemDB")
        { }
    }
}

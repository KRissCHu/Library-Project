using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEntities;

namespace ProjectRepositories
{
    public class BookRepository
    {
        private string connectionString;

        public BookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Book> GetAll()
        {
            List<Book> result = new List<Book>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Books";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = (int)reader["Id"];
                        book.ISBN = (string)reader["ISBN"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Publisher = (string)reader["Publisher"];
                        book.PubDate = (DateTime)reader["PubDate"];
                        book.Available = (int)reader["Available"];

                        result.Add(book);
                    }
                }
            }

            return result;
        }

        public void Insert(Book book)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Books (ISBN, Title, Author, Publisher, PubDate, Available)
    VALUES (@ISBN, @Title, @Author, @Publisher, @PubDate, @Available)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ISBN";
                parameter.Value = book.ISBN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Title";
                parameter.Value = book.Title;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Author";
                parameter.Value = book.Author;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Publisher";
                parameter.Value = book.Publisher;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@PubDate";
                parameter.Value = book.PubDate;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Available";
                parameter.Value = book.Available;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public Book GetById(int Id)
        {
            Book result = new Book();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"SELECT * FROM Books WHERE Id= @Id";

                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                command.Parameters.Add(param);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = (int)reader["Id"];
                        book.ISBN = (string)reader["ISBN"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Publisher = (string)reader["Publisher"];
                        book.PubDate = (DateTime)reader["PubDate"];
                        book.Available = (int)reader["Available"];

                        result = book;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private void Update(Book book)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Books SET ISBN=@ISBN, Title=@Title, Author=@Author, Publisher=@Publisher, PubDate=@PubDate, Available=@Available  WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = book.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ISBN";
            parameter.Value = book.ISBN;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = book.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Author";
            parameter.Value = book.Author;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Publisher";
            parameter.Value = book.Publisher;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@PubDate";
            parameter.Value = book.PubDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Available";
            parameter.Value = book.Available;
            command.Parameters.Add(parameter);
            try
             {
                 connection.Open();

                 command.ExecuteNonQuery();
             }
             finally
             {
                 connection.Close();
             }
        }

        public void Save(Book book)
        {
            if (book.Id > 0)
            {
                Update(book);
            }
            else
            {
                Insert(book);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Books WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(Book book)
        {
            Delete(book.Id);
        }
    }
}

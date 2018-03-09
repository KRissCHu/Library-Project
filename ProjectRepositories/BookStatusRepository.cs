using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositories
{
    public class BookStatusRepository
    {
        private string connectionString;

        public BookStatusRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BookStatus> GetAll()
        {
            List<BookStatus> result = new List<BookStatus>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM BookStatus";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        BookStatus bookStats = new BookStatus();
                        bookStats.Id = (int)reader["Id"];
                        bookStats.Title = (string)reader["Title"];
                        bookStats.ReaderId = (int)reader["ReaderId"];
                        bookStats.BorrowDate = (DateTime)reader["BorrowDate"];
                        bookStats.ReturnDate = (DateTime)reader["ReturnDate"];
                        bookStats.ReturnedDate = (DateTime)reader["ReturnedDate"];

                        result.Add(bookStats);
                    }
                }
            }

            return result;
        }

        public void Insert(BookStatus bookStats)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO BookStatus (Title, ReaderId, BorrowDate, ReturnDate, ReturnedDate)
    VALUES (@Title, @ReaderId, @BorrowDate, @ReturnDate, @ReturnedDate)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Title";
                parameter.Value = bookStats.Title;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ReaderId";
                parameter.Value = bookStats.ReaderId;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@BorrowDate";
                parameter.Value = bookStats.BorrowDate;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ReturnDate";
                parameter.Value = bookStats.ReturnDate;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ReturnedDate";
                parameter.Value = bookStats.ReturnedDate;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public BookStatus GetById(int Id)
        {
            BookStatus result = new BookStatus();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"SELECT * FROM BookStatus WHERE Id= @Id";

                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                command.Parameters.Add(param);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        BookStatus bookStatus = new BookStatus();
                        bookStatus.Id = (int)reader["Id"];
                        bookStatus.Title = (string)reader["Title"];
                        bookStatus.ReaderId = (int)reader["ReaderId"];
                        bookStatus.BorrowDate = (DateTime)reader["BorrowDate"];
                        bookStatus.ReturnDate = (DateTime)reader["ReturnDate"];
                        bookStatus.ReturnedDate = (DateTime)reader["ReturnedDate"];
                        result = bookStatus;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private void Update(BookStatus bookStatus)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE BookStatus SET Title=@Title, ReaderId=@ReaderId, BorrowDate=@BorrowDate, ReturnDate=@ReturnDate, ReturnedDate=@ReturnedDate WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = bookStatus.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = bookStatus.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ReaderId";
            parameter.Value = bookStatus.ReaderId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@BorrowDate";
            parameter.Value = bookStatus.BorrowDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ReturnDate";
            parameter.Value = bookStatus.ReturnDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ReturnedDate";
            parameter.Value = bookStatus.ReturnedDate;
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

        public void Save(BookStatus bookStatus)
        {
            if (bookStatus.Id > 0)
            {
                Update(bookStatus);
            }
            else
            {
                Insert(bookStatus);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Bookstatus WHERE Id=@Id";

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

        public void Delete(BookStatus bookStatus)
        {
            Delete(bookStatus.Id);
        }
    }
}


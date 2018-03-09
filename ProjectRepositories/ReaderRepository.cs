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
    public class ReaderRepository
    {
        private string connectionString;

        public ReaderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reader> GetAll()
        {
            List<Reader> result = new List<Reader>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Readers";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Reader read = new Reader();
                        read.Id = (int)reader["Id"];
                        read.FirstName = (string)reader["FirstName"];
                        read.FamilyName = (string)reader["FamilyName"];
                        read.CardNumber = (string)reader["CardNumber"];
                        read.ExpCardDate = (DateTime)reader["ExpCardDate"];

                        result.Add(read);
                    }
                }
            }

            return result;
        }

        public void Insert(Reader read)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Readers (FirstName, FamilyName, CardNumber, ExpCardDate)
    VALUES (@FirstName, @FamilyName, @CardNumber, @ExpCardDate)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@FirstName";
                parameter.Value = read.FirstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FamilyName";
                parameter.Value = read.FamilyName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@CardNumber";
                parameter.Value = read.CardNumber;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@ExpCardDate";
                parameter.Value = read.ExpCardDate;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public Reader GetById(int Id)
        {
            Reader result = new Reader();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"SELECT * FROM Readers WHERE Id= @Id";

                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                command.Parameters.Add(param);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Reader read = new Reader();
                        read.Id = (int)reader["Id"];
                        read.FirstName = (string)reader["FirstName"];
                        read.FamilyName = (string)reader["FamilyName"];
                        read.CardNumber = (string)reader["CardNumber"];
                        read.ExpCardDate = (DateTime)reader["ExpCardDate"];
                        result = read;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private void Update(Reader reader)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Readers SET FirstName=@FirstName, FamilyName=@FamilyName, CardNumber=@CardNumber, ExpCardDate=@ExpCardDate WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = reader.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@FirstName";
            parameter.Value = reader.FirstName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@FamilyName";
            parameter.Value = reader.FamilyName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CardNumber";
            parameter.Value = reader.CardNumber;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ExpCardDate";
            parameter.Value = reader.ExpCardDate;
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

        public void Save(Reader reader)
        {
            if (reader.Id > 0)
            {
                Update(reader);
            }
            else
            {
                Insert(reader);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Readers WHERE Id=@Id";

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

        public void Delete(Reader reader)
        {
            Delete(reader.Id);
        }
    }
}


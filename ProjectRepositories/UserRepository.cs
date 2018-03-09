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
    public class UserRepository
    {
        private string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetAll()
        {
            List<User> result = new List<User>();
            IDbConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText =
@"SELECT * FROM Users";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.Username = (string)reader["Username"];
                        user.Password = (string)reader["Password"];
                        user.FirstName = (string)reader["FirstName"];
                        user.FamilyName = (string)reader["FamilyName"];
                        user.Authority = (bool)reader["Authority"];

                        result.Add(user);
                    }
                }
            }

            return result;
        }

        public void Insert(User user)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Users (Username, Password, FirstName, FamilyName, Authority)
    VALUES (@Username, @Password, @FirstName, @FamilyName, @Authority)
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Username";
                parameter.Value = user.Username;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Password";
                parameter.Value = user.Password;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FirstName";
                parameter.Value = user.FirstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@FamilyName";
                parameter.Value = user.FamilyName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Authority";
                parameter.Value = user.Authority;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
        }

        public User GetById(int Id)
        {
            User result = new User();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"SELECT * FROM Users WHERE Id= @Id";

                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                command.Parameters.Add(param);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.Username = (string)reader["Username"];
                        user.Password = (string)reader["Password"];
                        user.FirstName = (string)reader["FirstName"];
                        user.FamilyName = (string)reader["FamilyName"];
                        user.Authority = (bool)reader["Authority"];
                        result = user;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private void Update(User user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Users SET Username=@Username, Password=@Password, FirstName=@FirstName, FamilyName=@FamilyName, Authority=@Authority WHERE Id=@Id";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = user.Id;
            command.Parameters.Add(parameter);
            
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Username";
            parameter.Value = user.Username;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Password";
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@FirstName";
            parameter.Value = user.FirstName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@FamilyName";
            parameter.Value = user.FamilyName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Authority";
            parameter.Value = user.Authority;
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

        public void Save(User user)
        {
            if (user.Id > 0)
            {
                Update(user);
            }
            else
            {
                Insert(user);
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"DELETE FROM Users WHERE Id=@Id";

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

        public void Delete(User user)
        {
            Delete(user.Id);
        }
    }


}

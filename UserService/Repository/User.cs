using Npgsql;
using System.Data;
using UserService.Model;

namespace UserService.Repository
{
    /// <summary>
    /// This is user details repository
    /// </summary>
    /// <param name="connection"></param>
    public class User(NpgsqlConnection connection) : Iuser, IDisposable
    {
        /// <summary>
        /// this method contains all user details
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDetails>> GetAll()
        {
            var userDetails = new List<UserDetails>();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, firstname, lastname, address FROM userorderdetails.users;";
            await connection.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (reader is not null)
            {
                while (await reader.ReadAsync())
                {
                    userDetails.Add(new UserDetails()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        firstname = Convert.ToString(reader["firstname"]),
                        lastname = Convert.ToString(reader["lastname"]),
                        address = Convert.ToString(reader["address"]),
                    });
                }
            }
            await connection.CloseAsync();
            return userDetails.ToList();
        }

        /// <summary>
        /// this method is used to get details by users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDetails> GetUserById(int id)
        {
            var userDetails = new UserDetails();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, firstname, lastname, address FROM userorderdetails.users where Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (reader is not null)
            {
                while (await reader.ReadAsync())
                {

                    userDetails.Id = Convert.ToInt32(reader["id"]);
                    userDetails.firstname = Convert.ToString(reader["firstname"]);
                    userDetails.lastname = Convert.ToString(reader["lastname"]);
                    userDetails.address = Convert.ToString(reader["address"]);

                }
            }
            await connection.CloseAsync();
            return userDetails;
        }

        /// <summary>
        /// this method is used to add new user details
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(UserDetails userDetails)
        {
            const string insertQuery =
                "INSERT INTO userorderdetails.users (firstname, lastname, email, address) " +
                "VALUES (@firstname, @lastname,@email ,@address)";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = insertQuery;
            AddParameters(cmd, userDetails);
            await connection.OpenAsync();
            var rowAffected = await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            return rowAffected > 0;

        }

        /// <summary>
        /// this method is used to delete users by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(int id)
        {
            const string deleteQuery = "DELETE FROM userorderdetails.users WHERE id = @Id";
            using var cmd = connection.CreateCommand();
            cmd.CommandText = deleteQuery;
            cmd.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var rowAffected = await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            return rowAffected > 0;

        }

        /// <summary>
        /// this method is used to add parameters for query
        /// </summary>
        /// <param name="command"></param>
        /// <param name="employee"></param>
        private static void AddParameters(NpgsqlCommand command, UserDetails employee)
        {
            var parameters = command.Parameters;

            parameters.AddWithValue("@Id", employee.Id);
            parameters.AddWithValue("@firstname", employee.firstname ?? string.Empty);
            parameters.AddWithValue("@lastname", employee.lastname ?? string.Empty);
            parameters.AddWithValue("@email", employee.email ?? string.Empty);
            parameters.AddWithValue("@address", employee.address ?? string.Empty);
        }

        /// <summary>
        /// this method is used to despose object
        /// </summary>
        public void Dispose()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            GC.SuppressFinalize(this);
        }
    }
}

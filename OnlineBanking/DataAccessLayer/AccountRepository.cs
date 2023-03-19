using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using OnlineBanking.Models;
using System.Security.Claims;

namespace OnlineBanking.DataAccessLayer
{
    public class AccountRepository
    {
        private readonly string? _connectionString;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public Dictionary<string,string> Login(Login login)
        {
            Dictionary<string, string> UserDetails = new Dictionary<string, string>();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand("sp_VerifyLogin", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@userName", login.UserName);
                sqlCommand.Parameters.AddWithValue("@password", login.Password);
                var sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    int userId = (int)sqlDataReader[0];
                    string roleType = (string)sqlDataReader[1];
                    string loggerId = sqlDataReader[2].ToString();
                    UserDetails["UserId"] = userId.ToString();
                    UserDetails["UserRole"] = roleType;
                    UserDetails["LoggerId"] = loggerId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return UserDetails;
        }
    }
}

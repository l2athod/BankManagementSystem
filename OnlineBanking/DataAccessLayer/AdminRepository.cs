using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using OnlineBanking.Models;
using System.Configuration;
using System.Data;
using System.Net;

namespace OnlineBanking.DataAccessLayer
{
    public class AdminRepository
    {
        private readonly string? _connectionString;
        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public List<Customer> GetCustomers()
        {
            var list = new List<Customer>();
            try
            {
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                SqlCommand sqlCommand = new SqlCommand("sp_GetCustomerList", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Customer customer = new Customer()
                    {
                        CustomerId = (long)sqlDataReader[0],
                        FirstName = (string)sqlDataReader[1],
                        LastName = (string)sqlDataReader[2],
                        DateOfBirth = (DateTime)sqlDataReader[3],
                        Address = (string)sqlDataReader[4],
                        City = (string)sqlDataReader[5],
                        State = (string)sqlDataReader[6],
                        PinCode = (string)sqlDataReader[7],
                        Gender = (string)sqlDataReader[8],
                        PhoneNo = sqlDataReader[9].ToString(),
                        Email = (string)sqlDataReader[10],
                        AccountNumber = (string)sqlDataReader[11],
                        RoleId = (int)sqlDataReader[12]
                    };
                    list.Add(customer);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                // ignored
            }

            return list;
        }
        public bool CreateUpdateCustomer(Customer customer)
        {
            try
            {
                // One session From Login to Logout
                // httpContextAccessor.HttpContext.Session.SetInt32("CustomerId", (int)customer.CustomerId);
                // httpContextAccessor.HttpContext.Session.GetInt32("CustomerId");
                // httpContextAccessor.HttpContext.Session.Remove("CustomerId");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_CreateUpdateCustomer", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@customerId", customer.CustomerId);
                sqlCommand.Parameters.AddWithValue("@firstName", customer.FirstName);
                sqlCommand.Parameters.AddWithValue("@lastName", customer.LastName);
                sqlCommand.Parameters.AddWithValue("@dateOfBirth", customer.DateOfBirth.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlCommand.Parameters.AddWithValue("@address", customer.Address);
                sqlCommand.Parameters.AddWithValue("@city", customer.City);
                sqlCommand.Parameters.AddWithValue("@state", customer.State);
                sqlCommand.Parameters.AddWithValue("@pinCode",customer.PinCode);
                sqlCommand.Parameters.AddWithValue("@gender", customer.Gender);
                sqlCommand.Parameters.AddWithValue("@phoneNo", Convert.ToInt64(customer.PhoneNo));  
                sqlCommand.Parameters.AddWithValue("@email", customer.Email);
                sqlCommand.Parameters.AddWithValue("@accountNumber", customer.AccountNumber.ToString());
                

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }   
        public Customer GetCustomerById(long id)
        {
            try
            {
                Customer customer = new Customer();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetCustomerById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@customerId", id);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        customer.CustomerId = (long)sqlDataReader[0];
                        customer.FirstName = (string)sqlDataReader[1];
                        customer.LastName = (string)sqlDataReader[2];
                        customer.DateOfBirth = (DateTime)sqlDataReader[3];
                        customer.Address = (string)sqlDataReader[4];
                        customer.City = (string)sqlDataReader[5];
                        customer.State = (string)sqlDataReader[6];
                        customer.PinCode = (string)sqlDataReader[7];
                        customer.Gender = (string)sqlDataReader[8];
                        customer.PhoneNo = sqlDataReader[9].ToString();
                        customer.Email = (string)sqlDataReader[10];
                        customer.AccountNumber = (string)sqlDataReader[11];
                        customer.RoleId = (int)sqlDataReader[12];
                    }
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DeleteCustomer(long? id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_DeleteCustomer", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@customerId", id);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TransactionModel> GetTransactions()
        {
            try
            {
                List<TransactionModel> list = new List<TransactionModel>();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_GetTransactions", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        list.Add(new TransactionModel()
                        {
                            TransactionId = (long)sqlDataReader[0],
                            TransactionType = (string)sqlDataReader[1],
                            Description = (string)sqlDataReader[2],
                            TransferAmount = (decimal)sqlDataReader[3],
                            DateOfTransaction = (DateTime)sqlDataReader[4],
                            CustomerId = (long)sqlDataReader[5],
                            FromAccountNumber = (string)sqlDataReader[6],
                        });
                    }
                    sqlConnection.Close();
                }
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UserDetail GetAdminDetailsById(long id)
        {
            try
            {
                UserDetail user = new UserDetail();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAdminDetailsById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@adminId", id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = id;
                        user.FirstName = (string)reader[0];
                        user.LastName = (string)reader[1];
                        user.DateOfBirth = ((DateTime)reader[2]).ToString("dd-mm-yyyy");
                        user.Address = (string)reader[3];
                        user.City = (string)reader[4];
                        user.State = (string)reader[5];
                        user.PinCode = (string)reader[6];
                        user.Gender = (string)reader[7];
                        user.PhoneNo = (long)reader[8];
                        user.Email = (string)reader[9];

                        if (user.Gender == "M") { user.Gender = "Male"; }
                        else { user.Gender = "Female"; }
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

















        public List<AccountModel> GetAllAccountList()
        {
            List<AccountModel> accountModels = new List<AccountModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllAccountList";
                SqlDataAdapter SqlDA = new SqlDataAdapter(command);
                DataTable dtAccount = new DataTable();

                connection.Open();
                SqlDA.Fill(dtAccount);



                foreach (DataRow dr in dtAccount.Rows)
                {
                    accountModels.Add(new AccountModel
                    {
                        CustomerName = (string)dr["CustomerName"],
                        AccountId = Convert.ToInt64(dr["AccountId"]),
                        AccountNumber = (string)dr["AccountNumber"],
                        AccountBalance = Convert.ToDecimal(dr["AccountBalance"]),
                        DateOfCreation = (DateTime)(dr["DateofCreation"]),
                        AccountType = dr["AccountType"].ToString(),
                        Branch = (string)dr["Branch"],
                        IFSCCode = dr["IFSCCode"].ToString(),
                        Status = (bool)dr["Status"]
                    });
                }
                connection.Close();
            }

            return accountModels;
        }




        //Insert
        public bool InsertAccountModel(AccountModel accountModel)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_InsertAccount", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountNumber", accountModel.AccountNumber);
                command.Parameters.AddWithValue("@AccountBalance", accountModel.AccountBalance);
                command.Parameters.AddWithValue("@DateOfCreation", accountModel.DateOfCreation.ToString("yyyy-MM-dd hh:mm:ss"));
                command.Parameters.AddWithValue("@AccountType", accountModel.AccountType);
                command.Parameters.AddWithValue("@Branch", accountModel.Branch);
                command.Parameters.AddWithValue("@IFSCCode", accountModel.IFSCCode);

                connection.Open();
                command.ExecuteNonQuery();
                id++;
                connection.Close();
                {
                    if (id > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        //Get product 

        public List<AccountModel> AccountId(int AccountId)
        {
            List<AccountModel> accountModels = new List<AccountModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAccountById";
                command.Parameters.AddWithValue("@AccountId", AccountId);

                SqlDataAdapter SqlDA = new SqlDataAdapter(command);
                DataTable dtAccount = new DataTable();

                connection.Open();
                SqlDA.Fill(dtAccount);
                connection.Close();

                foreach (DataRow dr in dtAccount.Rows)
                {
                    accountModels.Add(new AccountModel()
                    {
                        AccountId = (int)Convert.ToInt64(dr["AccountId"]),
                        AccountNumber = ((string)dr["AccountNumber"]),
                        AccountBalance = Convert.ToDecimal(dr["AccountBalance"]),
                        DateOfCreation = (DateTime)(dr["DateofCreation"]),
                        AccountType = dr["AccountType"].ToString(),
                        Branch = (string)dr["Branch"],
                        IFSCCode = dr["IFSCCode"].ToString(),
                    });

                }
                return accountModels;
            }
        }

        //Update
        public void UpdateAccountModel(AccountModel accountModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateAccount", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountId", accountModel.AccountId);
                command.Parameters.AddWithValue("@AccountNumber", accountModel.AccountNumber);
                command.Parameters.AddWithValue("@AccountBalance", accountModel.AccountBalance);
                /*command.Parameters.AddWithValue("@DateOfCreation", accountModel.DateOfCreation.ToString("yyyy-MM-dd hh:mm:ss"));*/
                command.Parameters.AddWithValue("@AccountType", accountModel.AccountType);
                command.Parameters.AddWithValue("@Branch", accountModel.Branch);
                command.Parameters.AddWithValue("@IFSCCode", accountModel.IFSCCode);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Delete data
        public void DeleteAccount(string Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_DeleteAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccountId", Id);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                connection.Close();
            }
        }
    }
}

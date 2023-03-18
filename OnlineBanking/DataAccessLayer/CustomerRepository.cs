﻿using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using OnlineBanking.Models;

namespace OnlineBanking.DataAccessLayer
{
    public class CustomerRepository
    {
        private readonly string? _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }
        
        public List<TransactionModel> CustomerTransactionsById(long id)
        {
            try
            {
                List<TransactionModel> transactions = new List<TransactionModel>();
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_CustomerTransactionsById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@customerId", id);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        TransactionModel transaction = new TransactionModel();
                        transaction.TransactionType = (string)sqlDataReader[0];
                        transaction.Description = (string)sqlDataReader[1];
                        transaction.TransferAmount = (decimal)sqlDataReader[2];
                        transaction.DateOfTransaction = (DateTime)sqlDataReader[3];
                        transaction.FromAccountNumber = (string)sqlDataReader[4];
                        transactions.Add(transaction);
                    }
                    sqlConnection.Close();
                }
                return transactions;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool CreateTransaction(TransactionModel transaction)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_CreateTransaction", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@transactionType", transaction.TransactionType);
                    sqlCommand.Parameters.AddWithValue("@description", transaction.Description);
                    sqlCommand.Parameters.AddWithValue("@amount", transaction.TransferAmount);
                    sqlCommand.Parameters.AddWithValue("@dateOfTransaction", transaction.DateOfTransaction.ToString("yyyy-MM-dd hh:mm:ss"));
                    sqlCommand.Parameters.AddWithValue("@customerId", transaction.CustomerId);
                    sqlCommand.Parameters.AddWithValue("@transferAccountNumber", transaction.FromAccountNumber);
                    sqlCommand.Parameters.AddWithValue("@benificiaryAccountNumber", transaction.ToAccountNumber);

                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Dictionary<string,string> GetAccountNumberWithAmount(long id)
        {
            Dictionary<string, string>? accountBalance = null;
            List<SelectListItem> accounts = new List<SelectListItem>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAccountsWithAmountById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@customerId", id);  
                    var reader = sqlCommand.ExecuteReader();
                    accountBalance = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        string AccountNumber = (string)reader[0];
                        string Amount = reader[1].ToString();
                        accountBalance[AccountNumber] = Amount;
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return accountBalance;
        }

    }
}
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TransactionAPI.Interfaces;
using TransactionAPI.Models;

namespace TransactionAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int pageNumber, int pageSize)
        {
            using (var db = CreateConnection())
            {
                string sql = @"SELECT * FROM Transactions 
                       ORDER BY TransactionDateTime DESC 
                       OFFSET @Offset ROWS 
                       FETCH NEXT @PageSize ROWS ONLY";

                return await db.QueryAsync<Transaction>(sql, new
                {
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });
            }
        }

        public async Task<Transaction> GetTransactionById(string id)
        {
            using (var db = CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Transaction>("SELECT * FROM Transactions WHERE TransactionID = @id", new { id });
            }
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            using (var db = CreateConnection())
            {
                string sql = @"INSERT INTO Transactions (AccountID, TransactionAmount, TransactionCurrencyCode, TransactionDateTime) 
                               VALUES (@AccountID, @TransactionAmount, @TransactionCurrencyCode, @TransactionDateTime)";
                return await db.ExecuteAsync(sql, transaction);
            }
        }

        public async Task<int> UpdateTransaction(Transaction transaction)
        {
            using (var db = CreateConnection())
            {
                string sql = @"UPDATE Transactions 
                       SET TransactionAmount = @TransactionAmount, 
                           TransactionCurrencyCode = @TransactionCurrencyCode, 
                           TransactionScenario = @TransactionScenario 
                       WHERE TransactionID = @TransactionID";

                return await db.ExecuteAsync(sql, new { transaction.TransactionAmount, transaction.TransactionCurrencyCode, transaction.TransactionScenario, transaction.TransactionID });

            }
        }

        public async Task<int> DeleteTransaction(string id)
        {
            using (var db = CreateConnection())
            {
                return await db.ExecuteAsync("DELETE FROM Transactions WHERE TransactionID = @id", new { id });
            }
        }
    }
}
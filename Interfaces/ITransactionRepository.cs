
using TransactionAPI.Models;

namespace TransactionAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(int pageNumber, int pageSize);
        Task<Transaction> GetTransactionById(string id);
        Task<int> AddTransaction(Transaction transaction);
        Task<int> UpdateTransaction(Transaction transaction);
        Task<int> DeleteTransaction(string id);
    }
}

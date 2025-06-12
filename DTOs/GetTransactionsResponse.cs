using TransactionAPI.Models;

namespace TransactionAPI.DTOs
{
    public class GetTransactionsResponse
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
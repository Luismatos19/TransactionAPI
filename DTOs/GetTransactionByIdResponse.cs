using TransactionAPI.Models;

namespace TransactionAPI.DTOs
{
    public class GetTransactionByIdResponse
    {
        public Transaction Transaction { get; set; }
        public string Message { get; set; } = "Transação encontrada!";
    }
}
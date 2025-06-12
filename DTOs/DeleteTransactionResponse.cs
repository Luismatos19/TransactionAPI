namespace TransactionAPI.DTOs
{
    public class DeleteTransactionResponse
    {
        public required string TransactionID { get; set; }
        public string Message { get; set; } = "Transação deletada com sucesso!";
    }
}
namespace TransactionAPI.DTOs
{
    public class UpdateTransactionResponse
    {
        public string TransactionID { get; set; }
        public decimal TransactionAmount { get; set; }
        public required string TransactionCurrencyCode { get; set; }
        public required string TransactionScenario { get; set; }
        public string Message { get; set; } = "Transação atualizada com sucesso!";
    }
}
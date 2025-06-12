using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.DTOs
{
    public class DeleteTransactionRequest
    {
        [Required(ErrorMessage = "AccountID é obrigatório.")]
        public required string TransactionID { get; set; }
    }
}
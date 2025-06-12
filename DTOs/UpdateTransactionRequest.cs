using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.DTOs
{
    public class UpdateTransactionRequest
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da transação deve ser maior que zero.")]
        public decimal TransactionAmount { get; set; }

        [Required(ErrorMessage = "Código da moeda é obrigatório.")]
        [StringLength(3, ErrorMessage = "Código da moeda deve ter 3 caracteres (ex: USD, BRL).")]
        public string TransactionCurrencyCode { get; set; }

        [Required(ErrorMessage = "O cenário da transação é obrigatório.")]
        public string TransactionScenario { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.DTOs
{
    public class AddTransactionRequest
    {
        [Required(ErrorMessage = "AccountID é obrigatório.")]
        public string AccountID { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "O valor da transação deve ser positivo.")]
        public decimal TransactionAmount { get; set; }

        [Required(ErrorMessage = "Código da moeda é obrigatório.")]
        [StringLength(3, ErrorMessage = "Código da moeda deve ter 3 caracteres.")]
        public string TransactionCurrencyCode { get; set; }

        [Range(0, 23, ErrorMessage = "A hora local deve estar entre 0 e 23.")]
        public int LocalHour { get; set; }

        [Required(ErrorMessage = "O cenário da transação é obrigatório.")]
        public string TransactionScenario { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório.")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "O endereço IP é obrigatório.")]
        [RegularExpression(@"^(?:\d{1,3}\.){3}\d{1,3}$", ErrorMessage = "Formato de IP inválido.")]
        public string TransactionIPAddress { get; set; }

        [Required(ErrorMessage = "País do IP é obrigatório.")]
        public string IpCountry { get; set; }

        [Required(ErrorMessage = "País de cobrança é obrigatório.")]
        public string PaymentBillingCountryCode { get; set; }

        [Required(ErrorMessage = "País de envio é obrigatório.")]
        public string ShippingCountry { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de itens digitais deve ser positivo.")]
        public int DigitalItemCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O número de itens físicos deve ser positivo.")]
        public int PhysicalItemCount { get; set; }

        [Required(ErrorMessage = "Data da transação é obrigatória.")]
        public DateTime TransactionDateTime { get; set; }
    }
}
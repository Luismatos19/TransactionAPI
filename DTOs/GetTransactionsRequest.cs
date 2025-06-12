using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.DTOs
{
    public class GetTransactionsRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser maior que zero.")]
        public int PageNumber { get; set; }

        [Range(1, 100, ErrorMessage = "O tamanho da página deve estar entre 1 e 100.")]
        public int PageSize { get; set; }
    }
}
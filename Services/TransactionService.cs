using TransactionAPI.Interfaces;
using TransactionAPI.Models;
using TransactionAPI.DTOs;
using TransactionAPI.Exceptions;


namespace TransactionAPI.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTransactionsResponse> GetAllTransactions(GetTransactionsRequest request)
        {
            if (request.PageNumber <= 0 || request.PageSize <= 0)
                throw new ArgumentException("Os valores de página e tamanho devem ser maiores que zero.");

            var transactions = await _repository.GetAllTransactions(request.PageNumber, request.PageSize);

            return new GetTransactionsResponse
            {
                Transactions = transactions,
                TotalRecords = transactions.Count(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<GetTransactionByIdResponse> GetTransactionById(string id)
        {
            var transaction = await _repository.GetTransactionById(id);

            return transaction == null
                ? throw new TransactionNotFoundException(id)
                : new GetTransactionByIdResponse { Transaction = transaction };
        }

        public async Task<AddTransactionResponse> AddTransaction(AddTransactionRequest request)
        {
            if (request.TransactionAmount <= 0)
                throw new ArgumentException("O valor da transação deve ser positivo.");

            var transaction = new Transaction
            {
                AccountID = request.AccountID,
                TransactionID = Guid.NewGuid().ToString(),
                TransactionAmount = request.TransactionAmount,
                TransactionCurrencyCode = request.TransactionCurrencyCode,
                LocalHour = request.LocalHour,
                TransactionScenario = request.TransactionScenario,
                TransactionType = request.TransactionType,
                TransactionIPAddress = request.TransactionIPAddress,
                IpState = request.IpState,
                IpPostalCode = request.IpPostalCode,
                IpCountry = request.IpCountry,
                IsProxyIP = request.IsProxyIP,
                BrowserLanguage = request.BrowserLanguage,
                PaymentInstrumentType = request.PaymentInstrumentType,
                CardType = request.CardType,
                PaymentBillingPostalCode = request.PaymentBillingPostalCode,
                PaymentBillingState = request.PaymentBillingState,
                PaymentBillingCountryCode = request.PaymentBillingCountryCode,
                ShippingPostalCode = request.ShippingPostalCode,
                ShippingState = request.ShippingState,
                ShippingCountry = request.ShippingCountry,
                CvvVerifyResult = request.CvvVerifyResult,
                DigitalItemCount = request.DigitalItemCount,
                PhysicalItemCount = request.PhysicalItemCount,
                TransactionDateTime = request.TransactionDateTime
            };

            await _repository.AddTransaction(transaction);

            return new AddTransactionResponse
            {
                AccountID = transaction.AccountID,
                TransactionID = transaction.TransactionID,
                TransactionAmount = transaction.TransactionAmount,
                TransactionCurrencyCode = transaction.TransactionCurrencyCode,
                LocalHour = transaction.LocalHour,
                TransactionScenario = transaction.TransactionScenario,
                TransactionType = transaction.TransactionType,
                TransactionIPAddress = transaction.TransactionIPAddress,
                IpState = transaction.IpState,
                IpPostalCode = transaction.IpPostalCode,
                IpCountry = transaction.IpCountry,
                IsProxyIP = transaction.IsProxyIP,
                BrowserLanguage = transaction.BrowserLanguage,
                PaymentInstrumentType = transaction.PaymentInstrumentType,
                CardType = transaction.CardType,
                PaymentBillingPostalCode = transaction.PaymentBillingPostalCode,
                PaymentBillingState = transaction.PaymentBillingState,
                PaymentBillingCountryCode = transaction.PaymentBillingCountryCode,
                ShippingPostalCode = transaction.ShippingPostalCode,
                ShippingState = transaction.ShippingState,
                ShippingCountry = transaction.ShippingCountry,
                CvvVerifyResult = transaction.CvvVerifyResult,
                DigitalItemCount = transaction.DigitalItemCount,
                PhysicalItemCount = transaction.PhysicalItemCount,
                TransactionDateTime = transaction.TransactionDateTime
            };
        }

        public async Task<UpdateTransactionResponse> UpdateTransaction(string id, UpdateTransactionRequest request)
        {
            if (id == null)
                throw new TransactionNotFoundException(id);

            if (request.TransactionAmount <= 0)
                throw new InvalidTransactionDataException("O valor da transação deve ser positivo.");

            if (string.IsNullOrWhiteSpace(request.TransactionCurrencyCode) || request.TransactionCurrencyCode.Length != 3)
                throw new InvalidTransactionDataException("Código da moeda inválido (deve conter 3 caracteres, ex: USD, BRL).");

            if (string.IsNullOrWhiteSpace(request.TransactionScenario))
                throw new InvalidTransactionDataException("O cenário da transação não pode estar vazio.");

            var transaction = new Transaction
            {
                TransactionID = id,
                TransactionAmount = request.TransactionAmount,
                TransactionCurrencyCode = request.TransactionCurrencyCode,
                TransactionScenario = request.TransactionScenario
            };

            await _repository.UpdateTransaction(transaction);

            return new UpdateTransactionResponse
            {
                TransactionID = id,
                TransactionAmount = request.TransactionAmount,
                TransactionCurrencyCode = request.TransactionCurrencyCode,
                TransactionScenario = request.TransactionScenario,
                Message = "Transação atualizada com sucesso!"
            };
        }

        public async Task<DeleteTransactionResponse> DeleteTransaction(DeleteTransactionRequest request)
        {
            await _repository.DeleteTransaction(request.TransactionID);

            return new DeleteTransactionResponse
            {
                TransactionID = request.TransactionID
            };
        }
    }
}

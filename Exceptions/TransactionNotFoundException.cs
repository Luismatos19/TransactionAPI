using System;

namespace TransactionAPI.Exceptions
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(string transactionId)
            : base($"Transação com ID {transactionId} não encontrada.") { }
    }
}
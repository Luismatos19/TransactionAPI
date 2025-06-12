using System;

namespace TransactionAPI.Exceptions
{
    public class InvalidTransactionDataException : Exception
    {
        public InvalidTransactionDataException(string message) : base(message) { }
    }
}

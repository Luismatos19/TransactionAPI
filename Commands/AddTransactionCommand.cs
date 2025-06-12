using MediatR;
using TransactionAPI.Models;
using System.Collections.Generic;

namespace TransactionAPI.Commands
{
    public record AddTransactionCommand(Transaction Transaction) : IRequest<int>;
}
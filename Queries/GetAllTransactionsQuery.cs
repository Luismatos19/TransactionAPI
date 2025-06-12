using MediatR;
using TransactionAPI.Models;
using System.Collections.Generic;

namespace TransactionAPI.Queries
{
    public record GetAllTransactionsQuery : IRequest<IEnumerable<Transaction>>;
}

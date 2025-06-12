using Xunit;
using Moq;
using TransactionAPI.Interfaces;
using TransactionAPI.Services;
using TransactionAPI.Models;
using TransactionAPI.DTOs;
using TransactionAPI.Exceptions;

namespace TransactionAPI.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _repositoryMock;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _repositoryMock = new Mock<ITransactionRepository>();
            _service = new TransactionService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTransactions_ShouldReturnTransactions_WhenValidRequest()
        {
            var mockTransactions = new List<Transaction> { new Transaction { TransactionID = "123", TransactionAmount = 100 } };
            _repositoryMock.Setup(repo => repo.GetAllTransactions(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(mockTransactions);

            var request = new GetTransactionsRequest { PageNumber = 1, PageSize = 10 };
            var result = await _service.GetAllTransactions(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Transactions.Count);
        }

        [Fact]
        public async Task GetTransactionById_ShouldThrowException_WhenTransactionNotFound()
        {
            _repositoryMock.Setup(repo => repo.GetTransactionById(It.IsAny<string>()))
                           .ReturnsAsync((Transaction)null);

            await Assert.ThrowsAsync<TransactionNotFoundException>(() => _service.GetTransactionById("123"));
        }

        [Fact]
        public async Task AddTransaction_ShouldReturnResponse_WhenTransactionValid()
        {
            var request = new AddTransactionRequest { AccountID = "A123", TransactionAmount = 200, TransactionCurrencyCode = "USD" };
            _repositoryMock.Setup(repo => repo.AddTransaction(It.IsAny<Transaction>()))
                           .ReturnsAsync(1);

            var result = await _service.AddTransaction(request);

            Assert.NotNull(result);
            Assert.Equal("USD", result.TransactionCurrencyCode);
            Assert.True(result.TransactionAmount > 0);
        }

        [Fact]
        public async Task UpdateTransaction_ShouldThrowException_WhenInvalidData()
        {
            var request = new UpdateTransactionRequest { TransactionAmount = -50, TransactionCurrencyCode = "USD", TransactionScenario = "Online" };

            await Assert.ThrowsAsync<InvalidTransactionDataException>(() => _service.UpdateTransaction("123", request));
        }

        [Fact]
        public async Task DeleteTransaction_ShouldCallRepositoryDeleteMethod()
        {
            var request = new DeleteTransactionRequest { TransactionID = "123" };
            _repositoryMock.Setup(repo => repo.DeleteTransaction(It.IsAny<string>()))
                           .Returns(Task.CompletedTask);

            var result = await _service.DeleteTransaction(request);

            Assert.NotNull(result);
            Assert.Equal("123", result.TransactionID);
            _repositoryMock.Verify(repo => repo.DeleteTransaction("123"), Times.Once);
        }
    }
}
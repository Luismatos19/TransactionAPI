using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionAPI.DTOs;
using TransactionAPI.Models;
using TransactionAPI.Services;

namespace TransactionAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetTransactionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactions(
            [FromQuery] GetTransactionsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactions = await _service.GetAllTransactions(request);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTransactionByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransaction(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = await _service.GetTransactionById(id);
            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTransactionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.AddTransaction(request);
            return CreatedAtAction(nameof(GetTransaction), new { id = response.TransactionID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateTransactionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTransaction(string id, [FromBody] UpdateTransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.UpdateTransaction(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteTransactionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(DeleteTransactionRequest request) => Ok(await _service.DeleteTransaction(request));
    }
}

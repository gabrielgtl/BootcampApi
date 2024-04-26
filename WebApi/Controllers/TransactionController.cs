using Core.Interfaces.Services;
using Core.Requests.Account;
using Core.Requests.Transaction;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TransactionController : BaseApiController
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> FilterTransaction([FromQuery] FilterTransactionModel filter, int id)
    {
        var transaction = await _transactionService.FilterTransaction(filter);
        return Ok(transaction);
    }
}

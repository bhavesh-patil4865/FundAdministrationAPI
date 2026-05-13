using Asp.Versioning;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/transactions")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(
        ITransactionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Register transaction
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] CreateTransactionDto dto)
    {
        await _service.RegisterTransactionAsync(dto);

        return Ok(new
        {
            message = "Transaction registered"
        });
    }
}
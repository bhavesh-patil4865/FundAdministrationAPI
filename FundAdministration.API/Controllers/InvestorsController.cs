using Asp.Versioning;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/investors")]
[Authorize]
public class InvestorsController : ControllerBase
{
    private readonly IInvestorService _service;

    public InvestorsController(IInvestorService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all investors
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var investors = await _service.GetAllAsync();

        return Ok(investors);
    }

    /// <summary>
    /// Get investor by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var investor = await _service.GetByIdAsync(id);

        if (investor == null)
            return NotFound();

        return Ok(investor);
    }

    /// <summary>
    /// Create investor
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateInvestorDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    /// <summary>
    /// Delete investor
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Get investor transactions
    /// </summary>
    [HttpGet("{id}/transactions")]
    public async Task<IActionResult>
        GetTransactions(Guid id)
    {
        var result =
            await _service
                .GetInvestorTransactionsAsync(id);

        return Ok(result);
    }
}
using Asp.Versioning;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/funds")]
[Authorize]
public class FundsController : ControllerBase
{
    private readonly IFundService _service;

    public FundsController(IFundService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all funds
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var funds = await _service.GetAllAsync();

        return Ok(funds);
    }

    /// <summary>
    /// Get fund by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var fund = await _service.GetByIdAsync(id);

        if (fund == null)
            return NotFound();

        return Ok(fund);
    }

    /// <summary>
    /// Create new fund
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateFundDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    /// <summary>
    /// Update fund
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateFundDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    /// <summary>
    /// Delete fund
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Get fund summary
    /// </summary>
    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetSummary(Guid id)
    {
        var result =
            await _service.GetFundSummaryAsync(id);

        return Ok(result);
    }
}
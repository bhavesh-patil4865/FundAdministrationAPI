using Asp.Versioning;
using FundAdministration.Application.DTOs;
using FundAdministration.Domain.Entities;
using FundAdministration.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get fund investment report
    /// </summary>
    [HttpGet("funds")]
    public async Task<IActionResult> GetFundReport()
    {
        var result = await _context.Funds
            .Select(f => new FundReportDto
            {
                FundName = f.Name,

                InvestorCount = f.Investors.Count,

                NetInvestment =
                    f.Investors
                        .SelectMany(i => i.Transactions)
                        .Sum(t =>
                            t.Type ==
                            TransactionType.Subscription
                                ? t.Amount
                                : -t.Amount)
            })
            .ToListAsync();

        return Ok(result);
    }
}
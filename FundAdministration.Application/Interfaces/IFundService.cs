using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Interfaces
{
    public interface IFundService
    {
        Task<IEnumerable<FundDto>> GetAllAsync();

        Task<FundDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateFundDto dto);

        Task UpdateAsync(Guid id, UpdateFundDto dto);

        Task DeleteAsync(Guid id);

        Task<FundSummaryDto> GetFundSummaryAsync(Guid fundId);

    }
}

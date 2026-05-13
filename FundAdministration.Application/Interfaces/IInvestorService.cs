using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Interfaces
{
    public interface IInvestorService
    {
        Task<IEnumerable<InvestorDto>> GetAllAsync();

        Task<InvestorDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateInvestorDto dto);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<TransactionDto>>
            GetInvestorTransactionsAsync(Guid investorId);
    }
}

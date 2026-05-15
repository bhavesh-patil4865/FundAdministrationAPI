using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using FundAdministration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundAdministration.Infrastructure.Repositories
{
    public class InvestorRepository : GenericRepository<Investor>, IInvestorRepository
    {
        public InvestorRepository(AppDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>>
            GetInvestorTransactionsAsync(Guid investorId)
        {
            return await _context.Transactions
                .Where(x => x.InvestorId == investorId)
                .ToListAsync();
        }

        public async Task<int> GetInvestorCountByFundAsync(Guid fundId)
        {
            return await _context.Investors
                .CountAsync(i => i.FundId == fundId);
        }
    }
}

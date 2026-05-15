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
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByFundIdAsync(Guid fundId)
        {
            return await _context.Transactions
                .Include(t => t.Investor)
                .Where(t => t.Investor.FundId == fundId)
                .ToListAsync();
        }
    }
}

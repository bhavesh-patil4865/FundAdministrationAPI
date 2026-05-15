using FundAdministration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundAdministration.Domain.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByFundIdAsync(Guid fundId);
    }
}

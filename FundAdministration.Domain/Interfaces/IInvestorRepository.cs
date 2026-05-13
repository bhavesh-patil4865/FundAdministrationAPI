using FundAdministration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Domain.Interfaces
{
    public interface IInvestorRepository: IRepository<Investor>
    {
        Task<IEnumerable<Transaction>> GetInvestorTransactionsAsync(Guid investorId);
    }
}
